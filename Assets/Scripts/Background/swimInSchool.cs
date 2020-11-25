using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swimInSchool : MonoBehaviour
{
    Vector3 randomOffset;
    public float randomRange = 3f;
    public Transform currentTarget;
    Vector3 offsetTarget;
    public float speed = 12f;
    public float slowSpeed = 1f;
    float actualSpeed = 1f;

    public float turnSpeed = 1f;
    Vector3 distance;

    // Start is called before the first frame update
    void Start()
    {
       randomOffset = Random.insideUnitSphere * randomRange;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTarget)
        {
            distance = (currentTarget.position + randomOffset) - this.transform.position;
            if(distance.magnitude < 1f)
            {
                actualSpeed = slowSpeed;
            }
            else
            {
                actualSpeed = speed;
            }

            this.transform.position = Vector3.MoveTowards(this.transform.position,(currentTarget.position + randomOffset),speed * Time.deltaTime);

             // Determine which direction to rotate towards
        Vector3 targetDirection = (currentTarget.position + randomOffset) - transform.position;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, turnSpeed * Time.deltaTime, 0.0f);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);
        
        }
    }
  
}
