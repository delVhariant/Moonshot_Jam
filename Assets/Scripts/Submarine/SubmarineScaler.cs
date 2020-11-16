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
            }

        }
    }

    public void Scale(float scale, float time)
    {
        scaleSpeed = time;
        targetScale = scale;
        performScaling = true;
        scaler.Scale(scale, time);
    }
}
