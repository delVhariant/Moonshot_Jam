using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubDestroyer : MonoBehaviour
{
    public LayerMask destroy;
    public Rigidbody rb;
    public float baseMass;
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
            //((layerMask & 1 << layer) != 0)
        if((destroy & 1 << coll.gameObject.layer) !=0 && rb.mass > baseMass)
        {
            GameObject.Destroy(coll.gameObject);
        }
    }
}
