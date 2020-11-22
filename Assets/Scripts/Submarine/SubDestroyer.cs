using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubDestroyer : MonoBehaviour
{
    
    Rigidbody rb;
    float baseMass;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        baseMass = rb.mass;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.layer == LayerMask.NameToLayer("Desctructible") && rb.mass > baseMass)
        {
            GameObject.Destroy(coll.gameObject);
        }
    }
}
