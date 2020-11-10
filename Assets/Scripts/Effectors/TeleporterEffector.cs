using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterEffector : EffectorBase
{
    public TeleporterEffector exit;
    public ObjectScaler scaler;

    public float speed;
    float timer;
    public float bigScale;
    
    GameObject teleportTarget;
    Vector3 velocity, angularVelocity;
    
    


    
    // Start is called before the first frame update
    void Start()
    {
        if(!scaler)
            gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0 && teleportTarget && exit)
        {
            timer -= speed * Time.deltaTime;
            if(timer <= 0)
            {
                Vector3 n = Vector3.Normalize(exit.transform.forward);
                Debug.Log($"Launching Speed: {Vector3.Magnitude(velocity)} at dir: {n} = {n * Vector3.Magnitude(velocity)}");
                Rigidbody rb = teleportTarget.GetComponent<Rigidbody>();
                rb.velocity = Vector3.zero;
                rb.angularVelocity = rb.velocity;
                teleportTarget.transform.position = exit.transform.position;
                teleportTarget.transform.rotation = exit.transform.rotation;
                rb.AddForce(Vector3.Magnitude(velocity) * n, ForceMode.VelocityChange);                
            }
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        scaler.Scale(bigScale, speed);
        timer=1;
        if(exit)
        {
            teleportTarget = other.gameObject;
            velocity = other.GetComponent<Rigidbody>().velocity;
            angularVelocity = other.GetComponent<Rigidbody>().angularVelocity;
        }

    }

    protected override void OnTriggerExit(Collider other)
    {
        scaler.Scale(1, speed);
        if(timer != 0)
        {
            if(teleportTarget == other.gameObject)
                teleportTarget = null;
            timer=0;
        }
        
    }
}
