﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlterSizeEffector : EffectorBase
{
    public float scale;
    public float scaleTime;
    
    protected override void OnTriggerEnter(Collider other)
    {
        if(!GameState.IsStarted())
            return;
        
        if(other.TryGetComponent<SubmarineScaler>(out var controller))
        {
            controller.Scale(scale, scaleTime);
        }
    }

    override public void PerformAiming()
    {
        aiming = true;
    }

    void LateUpdate()
    {
        if(aiming)
        {
            aiming = false;
            EffectorSpawner.effectorSpawner.state = SpawnState.Idle;
            EffectorSpawner.effectorSpawner.spawning = null;            
        }
    }
}
