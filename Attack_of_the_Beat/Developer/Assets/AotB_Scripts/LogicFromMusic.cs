using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicFromMusic : MonoBehaviour
{
    AudioSource audioSource;
    AudioVisualizer musicAnalyzer;

    public float[,] musicData;

    public float[] defaultTriggers;
    public float lowestTrigger;
    public bool[] triggered;
    public bool[] priorTriggered;

    public float decayRate;

    public int currHistory; // counter
    public int maxHistory;

    public int[] triggerReport;

    /*
     * 
     *     Bar 0      Bar 1      Bar 2
     *       |          |          |
     *    [Bar 0],   [Bar 1],   [Bar 2]...
     * 0: [New FreqValue], ...
     * 1: [Last Value],
     * 2: [Current Trigger Value],
     * 3: [Default Trigger Value],
     * 4: [Decrease Rate],
     * 5: [currHistory for Bar]
     *    
     *    
     */


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        musicAnalyzer = GameObject.FindGameObjectWithTag("MusicAnalyzer").GetComponent<AudioVisualizer>();

        musicData = new float[musicAnalyzer.numBars, 6];
        triggered = new bool[musicAnalyzer.numBars];
        priorTriggered = new bool[musicAnalyzer.numBars];

        triggerReport = new int[7] {0,0,0,0,0,0,0};

        for (int i = 0; i < musicData.GetUpperBound(0) + 1; i++)
        {
            musicData[i, 0] = musicData[i, 1] = 0.0f;


            if (i < 10)
            {
                musicData[i, 2] = musicData[i, 3] = defaultTriggers[0];
            }
            else if (i < 20)
            {
                musicData[i, 2] = musicData[i, 3] = defaultTriggers[1];
            }
            else if (i < 30)
            {
                musicData[i, 2] = musicData[i, 3] = defaultTriggers[2];
            }
            else if (i < 45)
            {
                musicData[i, 2] = musicData[i, 3] = defaultTriggers[3];
            }
            else
            {
                musicData[i, 2] = musicData[i, 3] = defaultTriggers[4];
            }

            musicData[i, 4] = decayRate;
            triggered[i] = false;
            priorTriggered[i] = false;
            musicData[i, 5] = 0;
            currHistory = 0;
        }
    }

    void FixedUpdate()
    {
        ResetTriggered();
        UpdateMusicData(); 
    }

    void ResetTriggered()
    {
        for (int i = 0; i < triggered.Length; i++)
        {
            priorTriggered[i] = triggered[i];
            triggered[i] = false;
        }
    }

    void UpdateMusicData()
    {
        currHistory++;
        for (int i = 0; i < musicData.GetUpperBound(0) + 1; i++)
        {
            UpdateCurrLastData(i);
            UpdateTriggers(i);
            if (currHistory == maxHistory)
            {
                UpdateFromHistory(i);
                musicData[i, 5] = 0;
            }
        }

        if (currHistory >= maxHistory) {

            //print(triggerReport[0] + ", " + triggerReport[1] + ", " + triggerReport[2] + ", " + triggerReport[3] + ", " + triggerReport[4] + ", " + triggerReport[5] + ", " + triggerReport[6]);
            triggerReport = new int[7] { 0, 0, 0, 0, 0, 0, 0 };
            currHistory = 0;
        }
    }

    void UpdateFromHistory(int pos)
    {
        float triggerRate = musicData[pos, 5] / maxHistory;
        float modifier;

        if (triggerRate > 0.30f)
        {
            modifier = 1.25f;
            triggerReport[0]++;
        }
        else if (triggerRate > 0.20f)
        {
            modifier = 1.15f;
            triggerReport[1]++;
        }
        else if (triggerRate > 0.10f)
        {
            modifier = 1.10f;
            triggerReport[2]++;
        }
        else if (triggerRate > 0.05f)
        {
            modifier = 1.05f;
            triggerReport[3]++;
        }
        else if (triggerRate > 0.03f)
        {
            modifier = 1.0f;
            triggerReport[4]++;
        }
        else if (triggerRate > 0.00f )
        {
            modifier = 0.8f;
            triggerReport[5]++;
        }
        else
        {
            modifier = 0.5f;
            triggerReport[6]++;
        }

        musicData[pos, 3] = musicData[pos, 3] * modifier;

        if (musicData[pos, 3] < lowestTrigger) musicData[pos, 3] = lowestTrigger;

        if (musicData[pos, 2] < musicData[pos, 3])
            musicData[pos, 2] = musicData[pos, 3];
    }


    void UpdateTriggers(int pos)
    {
        if (musicData[pos, 0] < musicData[pos, 1])  // is current less than prior, then lower current trigger
        {
            if (musicData[pos, 2] - musicData[pos, 4] < musicData[pos, 3])
            {
                musicData[pos, 2] = musicData[pos, 3];
            }
            else
            {
                musicData[pos, 2] -= musicData[pos, 4];
            }
        }
        else
        {
            if (musicData[pos, 0] > musicData[pos, 2])
            {
                triggered[pos] = true;
                musicData[pos, 2] = musicData[pos, 0] * 0.7f;
                musicData[pos, 5]++;
            }
            
            musicData[pos, 4] = decayRate;
        }
    }

    void UpdateCurrLastData(int pos)
    {
        musicData[pos, 1] = musicData[pos, 0];  // put now last into position for last data
        musicData[pos, 0] = musicAnalyzer.bandBuffer[pos]; // update current with new data
    }
}
