using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnEffector : EffectorBase
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<SubmarineController>(out var controller))
        {
            controller.Turn(transform.rotation.eulerAngles);
        }
    }
}
