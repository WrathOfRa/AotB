using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    LogicFromMusic logic;

    public bool[,] simpleTriggers; // current triggers and last triggers
    public float[,] highestValues;  // current values and last values

    public Sprite laserSprite;
    public string spriteLayer;
    public int spriteSortingOrder;

    GameObject[] lasers;

    public Vector3 targetLocation;
    public bool targetAcquired;

    public LayerMask laserIgnore;

    // Start is called before the first frame update
    void Start()
    {
        this.tag = "LaserSpawner";
        simpleTriggers = new bool[7, 2];
        highestValues = new float[7, 2];
        lasers = new GameObject[7];

        targetLocation = new Vector3(0, 0, 0);
        targetAcquired = false;

        laserIgnore = 1 << 8 | 1 << 13 | 1 << 14;
        laserIgnore = ~laserIgnore;

        for (int i = 0; i < 7; i++) SpawnLaser(i);
    }

    void FixedUpdate()
    {
        if (logic == null) SetLogic();

        StorePriorData();
        SimplifyData();

        GetTargetLocation();

        NewAdjustLaser();

    }

    void SpawnLaser(int pos)
    {
        GameObject newLaser = new GameObject("Laser_" + pos);
        SpriteRenderer renderer = newLaser.AddComponent<SpriteRenderer>();
        newLaser.AddComponent<Laser>();
        renderer.sprite = laserSprite;

        float unit = laserSprite.rect.width / laserSprite.bounds.size.x;

        // Laser Properties, to be set by color type

        float maxLength, minLength, lengthDecay, maxWidth, minWidth, widthDecay, decayDelay;

        renderer.sortingLayerName = spriteLayer;
        renderer.sortingOrder = spriteSortingOrder + pos;

        switch (pos)
        {
            case 0:
                renderer.color = new Color(255, 0, 0);
                maxLength = 2000; minLength = 200; lengthDecay = 5;
                maxWidth = 50; minWidth = 5; widthDecay = 1;
                decayDelay = 10 - pos;
                break;
            case 1:
                renderer.color = new Color(255, 0, 255);
                maxLength = 1700; minLength = 200; lengthDecay = 5;
                maxWidth = 36; minWidth = 5; widthDecay = 1;
                decayDelay = 10 - pos;
                break;
            case 2:
                renderer.color = new Color(0, 0, 255);
                maxLength = 1500; minLength = 200; lengthDecay = 5;
                maxWidth = 28; minWidth = 5; widthDecay = 1;
                decayDelay = 10 - pos;
                break;
            case 3:
                renderer.color = new Color(0, 255, 255);
                maxLength = 1200; minLength = 200; lengthDecay = 5;
                maxWidth = 22; minWidth = 5; widthDecay = 1;
                decayDelay = 10 - pos;
                break;
            case 4:
                renderer.color = new Color(0, 255, 0);
                maxLength = 800; minLength = 200; lengthDecay = 5;
                maxWidth = 18; minWidth = 5; widthDecay = 1;
                decayDelay = 10 - pos;
                break;
            case 5:
                renderer.color = new Color(255, 255, 0);
                maxLength = 600; minLength = 200; lengthDecay = 5;
                maxWidth = 14; minWidth = 5; widthDecay = 1;
                decayDelay = 10 - pos;
                break;
            case 6:
                renderer.color = new Color(255, 100, 0);
                maxLength = 400; minLength = 200; lengthDecay = 5;
                maxWidth = 10; minWidth = 5; widthDecay = 1;
                decayDelay = 10 - pos;
                break;
            default:
                renderer.color = Color.black;
                maxLength = 1000; minLength = 500; lengthDecay = 5;
                maxWidth = 10; minWidth = 5; widthDecay = 1;
                decayDelay = 10 - pos;
                break;
        }

        renderer.transform.localScale = new Vector3(0, 0);
        renderer.transform.eulerAngles = new Vector3(0, 0, 0);

        newLaser.transform.parent = this.transform;
        newLaser.transform.position = new Vector3(this.transform.position.x/* + (1f * pos)*/, this.transform.position.y, this.transform.position.z);

        // 0: rotation, 1: length, 2: maxLength, 3: minLength, 4: lengthDecay, 5: width, 6: maxWidth, 7: minWidth, 8: widthDecay, 9: decayDelay
        float[] prop = new float[11] { 0, 0, maxLength, minLength, lengthDecay, 0, maxWidth, minWidth, widthDecay, decayDelay, unit };

        newLaser.GetComponent<Laser>().SetProperties(prop);

        newLaser.AddComponent<PolygonCollider2D>();

        lasers[pos] = newLaser;
    }

    void NewAdjustLaser()
    {
        float unit = laserSprite.rect.width / laserSprite.bounds.size.x;
        Vector3 temp = targetLocation - this.transform.position;
        float newRotation = Mathf.Rad2Deg * Mathf.Atan2(temp.y, temp.x);
        float collisionDistance = -1;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(Mathf.Cos(newRotation * Mathf.Deg2Rad), Mathf.Sin(newRotation * Mathf.Deg2Rad)), Mathf.Infinity, laserIgnore);

        if (hit.collider != null)
        {
            if (hit.collider.tag == "Player") collisionDistance = hit.distance * unit + 10;
            else if (hit.collider.tag == "Wall") collisionDistance = hit.distance * unit - 10;
        }

        for (int i = 0; i < 7; i++)
        {
            lasers[i].GetComponent<Laser>().FireLaser(simpleTriggers[i, 0], simpleTriggers[i, 1], highestValues[i, 0], highestValues[i, 1], newRotation, collisionDistance);
        }
    }

    void GetTargetLocation()
    {
        targetLocation = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    /*void AdjustLasersMusic()
    {
        /*for (int i = 0; i < 7; i++)
        {
            if (simpleTriggers[i])
            {
                Laser temp = lasers[i].GetComponent<Laser>();
                if (temp.laserOff) temp.StartLaser();
                else temp.ChangeProperties(-1, -1, -1, false);
            }
        }*//*

        for (int i = 0; i < 7; i++)
        {
            if (simpleTriggers[i])
            {
                lasers[i].GetComponent<Laser>().FireLaser();
            }
        }
    }*/

    void StorePriorData()
    {
        for (int i = 0; i < 7; i++)
        {
            simpleTriggers[i, 1] = simpleTriggers[i, 0];
            simpleTriggers[i, 0] = false;

            highestValues[i, 1] = highestValues[i, 0];
            highestValues[i, 0] = 0.0f;
        }
    }

    void SetNewData(int simplePos, int pos)
    {
        if (logic.triggered[pos]) simpleTriggers[simplePos, 0] = true;
        if (logic.musicData[pos, 0] > highestValues[simplePos, 0]) highestValues[simplePos, 0] = logic.musicData[pos, 0];
    }

    void SimplifyData()
    {
        for (int i = 0; i < logic.triggered.Length; i++)
        {
            if (i < 3)       SetNewData(0, i);
            else if (i < 7)  SetNewData(1, i);
            else if (i < 15) SetNewData(2, i);
            else if (i < 23) SetNewData(3, i);
            else if (i < 33) SetNewData(4, i);
            else if (i < 47) SetNewData(5, i);
            else             SetNewData(6, i);
        }
    }

    void SetLogic()
    {
        logic = GameObject.FindGameObjectWithTag("MusicLogic").GetComponent<LogicFromMusic>();
    }

    
}
