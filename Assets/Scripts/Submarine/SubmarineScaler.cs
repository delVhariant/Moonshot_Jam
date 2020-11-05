using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineScaler : MonoBehaviour
{
    
    Vector3 baseScale;
    float baseMass;
    bool performScaling;

    float targetScale;
    float scaleSpeed;

    Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        baseScale = transform.localScale;
        rb = GetComponent<Rigidbody>();
        baseMass = rb.mass;
    }

    // Update is called once per frame
    void Update()
    {
        if(performScaling)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, baseScale * targetScale, scaleSpeed * Time.deltaTime);
            rb.mass = Mathf.Lerp(rb.mass, baseMass * targetScale, scaleSpeed * Time.deltaTime);
            if(Vector3.Distance(transform.localScale, baseScale * targetScale) < 0.1)
            {
                transform.localScale = baseScale * targetScale;
                performScaling = false;
            }

        }
    }

    public void Scale(float scale, float time)
    {
        scaleSpeed = time;
        targetScale = scale;
        performScaling = true;
    }
}
