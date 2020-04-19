using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVisualizer : MonoBehaviour
{

    GameMusic sampleSource;
    public Camera mainCam;
    public Vector3 HUD_Offset;

    public bool latchToCamera;

    public float[] freqBand;
    public float[] bandBuffer;
    public float[] bufferDecrease;

    public float[] freqBandHighest;
    public float[] audioBand;

    public int numBars;
    public float maxBarHeight;
    public float barScale;
    public float minBarHeight;
    public float maxBarWidth;
    public Vector3 location;

    public Sprite barSprite;
    public string spriteLayer;
    public float unit;

    public List<GameObject> bars;
    public List<GameObject> aBars;
    public float aBarsOffset;

    LogicFromMusic logic;
    public List<GameObject> lLines;

    // Start is called before the first frame update
    void Start()
    {
        freqBand = new float[64];
        freqBandHighest = new float[64];
        audioBand = new float[64];
        bandBuffer = new float[64];
        bufferDecrease = new float[64];

        sampleSource = GameObject.FindGameObjectWithTag("GameMusic").GetComponent<GameMusic>();

        location = GetComponentInParent<Transform>().position;
        unit = barSprite.rect.width / barSprite.bounds.size.x;
        spriteLayer = "HUD";

        bars.Clear();

        for (int i = 0; i < numBars; i++)
        {
            AddBar(i);
        }
    }


    void LateUpdate()
    {
        if (logic == null) SetLogic();
        
        location = GetComponentInParent<Transform>().position;

        if (latchToCamera)
        {
            location = mainCam.transform.position;
            location.z = 10;
        }

        MakeFrequencyBandsMax();
        BandBuffer();
        DisplayBars();
    }

    public void DisplayBars()
    {
        for (int i = 0; i < bars.Count; i++)
        {
            UpdateBarsFromBandBuffer();
        }
    }

    void SetLogic()
    {
        logic = GameObject.FindGameObjectWithTag("MusicLogic").GetComponent<LogicFromMusic>();
    }


    void UpdateAllLayers()
    {
        foreach (GameObject bar in bars)
        {
            bar.GetComponent<SpriteRenderer>().sortingLayerName = spriteLayer;
        }
    }

    void UpdateBarHeight(int pos, float height)
    {
        bars[pos].transform.localScale = new Vector3(maxBarWidth, height, 0);
        bars[pos].transform.position = new Vector3(location.x + (pos * maxBarWidth / unit), location.y + (height / (2 * unit)), location.z);

        aBars[pos].transform.localScale = new Vector3(maxBarWidth - aBarsOffset, height - (aBarsOffset / 2), 0);
        if (((height - aBarsOffset) / (2 * unit)) + (aBarsOffset / (2 * unit)) < 0) aBars[pos].transform.position = new Vector3(location.x + (pos * maxBarWidth / unit), location.y + 0, location.z);
        else aBars[pos].transform.position = new Vector3(location.x + (pos * maxBarWidth / unit), location.y + ((height - aBarsOffset) / (2 * unit)) + (aBarsOffset / (2 * unit)), location.z);


        lLines[pos].transform.localScale = new Vector3(maxBarWidth - aBarsOffset, 2, 0);
        if (logic == null) lLines[pos].transform.position = new Vector3(location.x + (pos * maxBarWidth / unit), location.y + 0, location.z);
        else lLines[pos].transform.position = new Vector3(location.x + (pos * maxBarWidth / unit), location.y + (logic.musicData[pos, 2] / unit), location.z);

        if (latchToCamera)
        {
            bars[pos].transform.position = bars[pos].transform.position + HUD_Offset;
            aBars[pos].transform.position = aBars[pos].transform.position + HUD_Offset;
            lLines[pos].transform.position = lLines[pos].transform.position + HUD_Offset;
        }
    }

    void AddBar(int listPos)
    {
        GameObject newBar = new GameObject("Bar " + listPos.ToString());
        SpriteRenderer renderer = newBar.AddComponent<SpriteRenderer>();
        renderer.sprite = barSprite;
        renderer.color = Color.black;

        newBar.transform.parent = this.transform;
        renderer.sortingLayerName = spriteLayer;
        renderer.sortingOrder = 0;
        bars.Add(newBar);

        GameObject newABar = new GameObject("A_Bar " + listPos.ToString());
        SpriteRenderer rendererA = newABar.AddComponent<SpriteRenderer>();
        rendererA.sprite = barSprite;

        if (listPos < 3) rendererA.color = new Color(255, 0, 0);
        else if (listPos < 7) rendererA.color = new Color(255, 0, 255);
        else if (listPos < 15) rendererA.color = new Color(0, 0, 255);
        else if (listPos < 23) rendererA.color = new Color(0, 255, 255);
        else if (listPos < 33) rendererA.color = new Color(0, 255, 0);
        else if (listPos < 47) rendererA.color = new Color(255, 255, 0);
        else rendererA.color = new Color(255, 100, 0);



        newABar.transform.parent = this.transform;
        rendererA.sortingLayerName = spriteLayer;
        rendererA.sortingOrder = 1;
        aBars.Add(newABar);




        GameObject newLine = new GameObject("L_Line " + listPos.ToString());
        SpriteRenderer rendererL = newLine.AddComponent<SpriteRenderer>();
        rendererL.sprite = barSprite;
        rendererL.color = Color.red;

        newLine.transform.parent = this.transform;
        rendererL.sortingLayerName = spriteLayer;
        rendererL.sortingOrder = 2;
        lLines.Add(newLine);





        UpdateBarHeight(listPos, minBarHeight);
    }

    void UpdateBarsFromBandBuffer()
    {
        for (int i = 0; i < numBars; i++)
        {
            UpdateBarHeight(i, bandBuffer[i] + minBarHeight);
        }
    }

    void BandBuffer()
    {
        for (int i = 0; i < numBars; i++)
        {
            if (freqBand[i] > bandBuffer[i])
            {
                bandBuffer[i] = freqBand[i];
                bufferDecrease[i] = 0.005f;
            }
            if (freqBand[i] < bandBuffer[i])
            {
                bufferDecrease[i] = (bandBuffer[i] - freqBand[i]) / 8;
                bandBuffer[i] -= bufferDecrease[i];
            }

            if (bandBuffer[i] < 0) bandBuffer[i] = 0;
        }
    }

    void MakeFrequencyBandsMax()
    {
        int count = 0;
        int sampleCount = 1;
        int power = 0;

        for (int i = 0; i < 64; i++)
        {
            float currMax = 0;

            if (i == 16 || i == 32 || i == 40 || i == 48 || i == 56)
            {
                power++;
                sampleCount = (int)Mathf.Pow(2, power);
                if (power == 3) sampleCount -= 2;
            }

            for (int j = 0; j < sampleCount; j++)
            {
                if (sampleSource.mainSamples[count] > currMax) currMax = sampleSource.mainSamples[count];
                count++;
            }
            freqBand[i] = currMax * barScale;
        }

    }







}
