using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubDestroyer : MonoBehaviour
{
    public LayerMask destroy;
    public Rigidbody rb;
    public float baseMass;

    Vector3 lastVel;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        baseMass = rb.mass;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        lastVel = rb.velocity.normalized;
    }

    void OnTriggerEnter(Collider coll)
    {
            //((layerMask & 1 << layer) != 0)
        if((destroy & 1 << coll.gameObject.layer) !=0 && rb.mass > baseMass)
        {
            //float force = coll.impulse.magnitude * 0.8f;
            // Vector3 norm = coll.impulse.normalized;
            
            //Debug.DrawRay(transform.position,lastVel * force, Color.blue, 10f);
            //Debug.DrawRay(transform.position, force * -norm, Color.blue, 10f);
            //Debug.Log(rb.velocity.normalized);
            //GameObject.Destroy(coll.gameObject);
            if(!coll.TryGetComponent<Resetter>(out Resetter r))
            {
                coll.gameObject.AddComponent<Resetter>();
            }
            coll.gameObject.SetActive(false);
            //rb.AddForce(lastVel * force, ForceMode.Impulse);
        }
    }
}
