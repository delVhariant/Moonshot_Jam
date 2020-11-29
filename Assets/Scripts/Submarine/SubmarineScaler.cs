using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectScaler))]
public class SubmarineScaler : MonoBehaviour
{
    
    
    float baseMass;
    float targetScale;
    bool performScaling;

    float scaleSpeed;

    Rigidbody rb;

    ObjectScaler scaler;

    public bool snapBack = false;
    bool hasScaled = false;
    float timeToSnap = 0f;
    float snapDelay = 5f;

    
    // Start is called before the first frame update
    void Start()
    {
        scaler = GetComponent<ObjectScaler>();
        rb = GetComponent<Rigidbody>();
        baseMass = rb.mass;
    }

    // Update is called once per frame
    void Update()
    {
        if(performScaling)
        {
            rb.mass = Mathf.Lerp(rb.mass, baseMass * targetScale, scaleSpeed * Time.deltaTime);
            if(Mathf.Abs(rb.mass - baseMass * targetScale) < 0.1)
            {
                rb.mass = baseMass * targetScale;
                performScaling = false;
                hasScaled = true;
            }

        }
        // check if should snapBack to OG size
        if (snapBack && hasScaled && targetScale != 1f)
        {
            timeToSnap -= Time.deltaTime;
            if(timeToSnap <= 0)
            {   
                Scale(1,scaleSpeed);
                hasScaled = false;
                timeToSnap = 0f;
            }
        }
    }

    public void Scale(float scale, float time)
    {
        scaleSpeed = time;
        targetScale = scale;
        performScaling = true;
        scaler.Scale(scale, time);
        // snap back to OG size delay
        if( snapBack)
        {
            hasScaled = true;
            timeToSnap = snapDelay;
        }
    }
}
