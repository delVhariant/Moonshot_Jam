using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScaler : MonoBehaviour
{
    bool performScaling;

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
    }

    public void Scale(float scale, float time)
    {
        scaleSpeed = time;
        targetScale = scale;
        performScaling = true;
    }
}
