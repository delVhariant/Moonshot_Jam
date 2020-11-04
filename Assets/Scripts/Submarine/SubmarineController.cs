using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineController : MonoBehaviour
{
    public float turnSpeed = 15;
    [SerializeField]
    Vector3 turnTarget;

    [SerializeField]
    bool turning = false;

    public ConstantForce force;
    float initialSpeed;

    
    // Start is called before the first frame update
    void Start()
    {
        force = GetComponent<ConstantForce>();
    }

    // Update is called once per frame
    void Update()
    {
        if(turning)
        {
            //transform.Rotate(turnTarget * Time.deltaTime * turnSpeed, Space.World);
            
            Vector3 newDir = Vector3.Lerp(transform.eulerAngles, turnTarget, (Mathf.Deg2Rad * turnSpeed) * Time.deltaTime);
            //Debug.DrawLine(transform.position, transform.position + turnTarget, Color.red);
            transform.rotation = Quaternion.Euler(newDir);
            if(Vector3.Distance(transform.eulerAngles, turnTarget) < 0.1)
            {
                turning = false;
                //turnTarget = Vector3.zero;
            }
            //force.force = transform.forward * initialSpeed;
        }
    }

    public void Turn(Vector3 angle)
    {
        turnTarget = angle;
        turning = true;
    }

    public void SetForce(float f)
    {
        initialSpeed = f;
    }
}
