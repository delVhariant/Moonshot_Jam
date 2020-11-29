using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScaler : MonoBehaviour
{
    bool performScaling;

    // public bool snapBack = false;
    // bool hasScaled = false;
    // float timeToSnap = 0f;
    // float snapDelay = 5f;
    float targetScale;
    float scaleSpeed;
    Vector3 baseScale;
    
    
    // Start is called before the first frame update
    void Start()
    {
        baseScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {

        if(performScaling)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, baseScale * targetScale, scaleSpeed * Time.deltaTime);
            if(Vector3.Distance(transform.localScale, baseScale * targetScale) < 0.1)
            {
                transform.localScale = baseScale * targetScale;
                performScaling = false;
            }
        }
        // check if should snapBack to OG size
        // if (snapBack && hasScaled)
        // {
        //     timeToSnap -= Time.deltaTime;
        //     if(timeToSnap <= 0)
        //     {   
        //         Scale(1,scaleSpeed);
        //         hasScaled = false;
        //         timeToSnap = 0f;
        //     }
        // }
    }

    public void Scale(float scale, float time)
    {
        scaleSpeed = time;
        targetScale = scale;
        performScaling = true;

        // snap back to OG size delay
        /*if( snapBack)
        {
        hasScaled = true;
        timeToSnap = snapDelay;
        }*/
    }
}
