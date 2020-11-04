using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpulseEffector : EffectorBase
{
    public float force = 35;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    protected override void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.Impulse);
    }
}
