using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineBobOrWiggle : MonoBehaviour
{
    Vector3 startPos;
    Vector3 newPos;
    Vector3 startRot;
    Vector3 newRot;

    public bool posX = false;
    public bool posY = false;
    public bool posZ = false;
    public bool rotX = false;
    public bool rotY = false;
    public bool rotZ = false;

    public float speed = 1f;
    public float amount = 1f;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.localPosition;
        newPos = startPos;
        startRot = transform.localEulerAngles;
        newRot = startRot;
    }

    // Update is called once per frame
    void Update()
    {
        if(posX)
        {
            newPos = new Vector3(startPos.x + Mathf.Sin(Time.time * speed) * amount,newPos.y,newPos.z);
        }
        if(posY)
        {
            newPos = new Vector3(newPos.x,startPos.y + Mathf.Sin(Time.time * speed) * amount, newPos.z);
        }
        if(posZ)
        {
            newPos = new Vector3(newPos.x,newPos.y, startPos.z + Mathf.Sin(Time.time * speed) * amount);
        }
        transform.localPosition = newPos;

        if(rotX)
        {
            newRot = new Vector3(startRot.x + Mathf.Sin(Time.time * speed) * amount,newRot.y,newRot.z);
        }
        if(rotY)
        {
            newRot = new Vector3(newRot.x,startRot.y + Mathf.Sin(Time.time * speed) * amount, newRot.z);
        }
        if(rotZ)
        {
            newRot = new Vector3(newRot.x,newRot.y, startRot.z + Mathf.Sin(Time.time * speed) * amount);
        }
        transform.localEulerAngles = newRot;

    }
}
