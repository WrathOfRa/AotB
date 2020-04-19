using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    SpriteRenderer rend;
    public float rotation;
    public float length, maxLength, minLength, lengthDecay;
    public float width, maxWidth, minWidth, widthDecay;

    public float collisionLength;

    public float unit;

    public Vector3 location;
    public Vector3 targetLocation;


    public int decayCount;
    public int decayDelay;

    public bool decayFlag;
    public bool laserOff;

    public float dist;

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        decayFlag = true;
        laserOff = true;

        decayCount = 0;
        decayDelay = 10;
        dist = 0;

        this.tag = "Laser";
        gameObject.layer = 14;

        collisionLength = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (decayCount == 0) Decay();
        else decayCount--;

        //rotation++;
        //if (rotation > 359) rotation = 0;

        CheckLaserStatus();
        UpdateProperties();
    }

    void Decay()
    {
        width -= widthDecay;
        length -= lengthDecay;
    }

    void UpdateLocationCollision()
    {
        this.transform.position = new Vector3(location.x + (collisionLength * (Mathf.Cos(rotation * Mathf.Deg2Rad)) / (2 * unit)), location.y + ((collisionLength * (Mathf.Sin(rotation * Mathf.Deg2Rad))) / (2 * unit)), location.z);
    }

    void UpdateLocation()
    {
        this.transform.position = new Vector3(location.x + (length * (Mathf.Cos(rotation * Mathf.Deg2Rad)) / (2 * unit)), location.y + ((length * (Mathf.Sin(rotation * Mathf.Deg2Rad))) / (2 * unit)), location.z);
    }
    
    public void SetProperties(float[] newVal) // 0: rotation, 1: length, 2: maxLength, 3: minLength, 4: lengthDecay, 5: width, 6: maxWidth, 7: minWidth, 8: widthDecay, 9: decayDelay, 10: unit
    {
        rotation = newVal[0];
        length = newVal[1]; maxLength = newVal[2]; minLength = newVal[3]; lengthDecay = newVal[4];
        width = newVal[5]; maxWidth = newVal[6]; minWidth = newVal[7]; widthDecay = newVal[8];
        decayDelay = (int) newVal[9];

        unit = newVal[10];

        if (width >= minLength) decayCount = decayDelay;

        location = this.transform.position;
    }

    public void FireLaser(bool currTrigger, bool lastTrigger, float currHigh, float lastHigh, float newRotation, float collisionDistance)
    {
        rotation = newRotation;
        if (collisionDistance != -1) collisionLength = collisionDistance;
        //print("Set cLen: " + collisionLength);

        if (laserOff) // laser was off in last frame
        {
            if (currTrigger)
            {
                decayCount = decayDelay;
                StartLaser();
            }
        }
        else  // laser was on in last frame
        {
            decayCount = decayDelay;

            if (currHigh > lastHigh)
            {
                width += width * ( (currHigh - lastHigh) / lastHigh );
                length += length * ((currHigh - lastHigh) / lastHigh);
                if (width > maxWidth) width = maxWidth;
                if (length > maxLength) length = maxLength;
            }
            else if (lastHigh > currHigh)
            {
                width -= width * ( (lastHigh - currHigh) / currHigh );
                length -= length * ((lastHigh - currHigh) / currHigh);
            }
        }
    }

    void CheckLaserStatus()
    {
        if (length < minLength) length = minLength;
        if (width < minWidth)
        {
            width = 0;
            length = 0;
            laserOff = true;
        }
        else
            laserOff = false;
    }


    public void FireLaser()
    {
        decayCount = decayDelay;
        if (laserOff) { StartLaser(); }
        else
        {
            length += lengthDecay * 5;
            width += widthDecay * 5;

            if (length > maxLength) length = maxLength;
            if (width > maxWidth) width = maxWidth;

            laserOff = false;
        }
    }

    public void StartLaser()
    {
        length = minLength;
        width = minWidth;

        laserOff = false;
    }

    public void UpdateProperties()
    {
        if (collisionLength > length || collisionLength == -1)
        {
            rend.transform.localScale = new Vector3(width, length);
            rend.transform.eulerAngles = new Vector3(0, 0, rotation + 90);

            UpdateLocation();

            Destroy(transform.gameObject.GetComponent<PolygonCollider2D>());
            transform.gameObject.AddComponent<PolygonCollider2D>();
        }
        else
        {
            rend.transform.localScale = new Vector3(width, collisionLength);
            rend.transform.eulerAngles = new Vector3(0, 0, rotation + 90);

            UpdateLocationCollision();

            Destroy(transform.gameObject.GetComponent<PolygonCollider2D>());
            transform.gameObject.AddComponent<PolygonCollider2D>();
        }

    }

}
