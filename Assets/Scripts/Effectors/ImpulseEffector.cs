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


    protected override void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.Impulse);
    }
}
