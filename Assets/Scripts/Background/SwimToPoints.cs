using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimToPoints : MonoBehaviour
{
    public swimPath selectedPath;
    int currentInt = 0;
    int pathLength = 1;
    Transform currentTarget;
    public float speed = 1f;
    public float turnSpeed = 1f;
    Vector3 distance;

    // Start is called before the first frame update
    void Start()
    {
        currentTarget = selectedPath.pathPoints[currentInt];
        pathLength = selectedPath.pathPoints.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTarget)
        {
            distance = currentTarget.position - this.transform.position;
            if(distance.magnitude < 3f)
            {
                currentInt ++;
                if(currentInt >= pathLength)
                {
                    currentInt = 0;
                }
                currentTarget = selectedPath.pathPoints[currentInt];
            }

            this.transform.position = Vector3.MoveTowards(this.transform.position,currentTarget.position,speed * Time.deltaTime);

             // Determine which direction to rotate towards
        Vector3 targetDirection = currentTarget.position - transform.position;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, turnSpeed * Time.deltaTime, 0.0f);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);
        
        }
    }
}
