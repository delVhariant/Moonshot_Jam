using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resetter : MonoBehaviour
{
    Rigidbody rb;

    bool startGravity;
    Vector3 startLoc;
    Quaternion startRot;
    Vector3 startScale;
    
    // Start is called before the first frame update
    void Awake()
    {
        GameState.OnReset += ResetObject;
        rb = gameObject.GetComponent<Rigidbody>();
        if(rb)
            startGravity = rb.useGravity;

        startLoc = transform.position;
        startRot = transform.rotation;
        startScale = transform.localScale;
        
    }

    public void ResetObject()
    {
        if(!gameObject.activeSelf)
            gameObject.SetActive(true);
        if(rb)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.useGravity = startGravity;
        }
        transform.position = startLoc;
        transform.rotation = startRot;
        transform.localScale = startScale;
    }

    void OnDestroy()
    {
        GameState.OnReset -= ResetObject;
    }
}
