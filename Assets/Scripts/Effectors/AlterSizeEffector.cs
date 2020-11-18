using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlterSizeEffector : EffectorBase
{
    public float scale;
    public float scaleTime;
    
    protected override void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<SubmarineScaler>(out var controller))
        {
            controller.Scale(scale, scaleTime);
        }
    }

    override public void PerformAiming()
    {
        EffectorSpawner.effectorSpawner.state = SpawnState.Idle;
        EffectorSpawner.effectorSpawner.spawning = null;   
    }
}
