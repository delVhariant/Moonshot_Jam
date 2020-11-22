using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnState
{
    Placing,
    Aiming,
    Idle
}

public class EffectorSpawner : MonoBehaviour
{
    public SpawnState state = SpawnState.Idle;
    public static EffectorSpawner effectorSpawner;

    public EffectorBase spawning;

    void Awake()
    {
        // Shitty singleton code, not great practice but whatevs
        if(EffectorSpawner.effectorSpawner && EffectorSpawner.effectorSpawner != this)
        {
            this.enabled = false;
        }
        else if(!EffectorSpawner.effectorSpawner)
        {
            EffectorSpawner.effectorSpawner = this;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(spawning)
        {
            if(Input.GetMouseButtonDown(1))
            {
                CancelSpawn();
            }
            
            if(state == SpawnState.Placing)
            {
                spawning.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.x));                
                if(Input.GetMouseButtonDown(0))
                {
                    //GameState.gameManager.planningCam.Follow = null;
                    CameraLook(null);
                    state = SpawnState.Aiming;
                    spawning.aiming = true;
                }
            }
            else if(state == SpawnState.Aiming)
            {
                spawning.PerformAiming();
            }
        }
    }

    public void SpawnNew(GameObject e)
    {
        CameraLook(e.transform);
        spawning = e.GetComponent<EffectorBase>();
        state = SpawnState.Placing;
    }

    public void FinishSpawn(Transform spawned)
    {
        spawning = null;
        state = SpawnState.Idle;
        if(GameState.gameManager.controlType == ControlType.RealTime)
        {
            GameState.gameManager.realTimeTarget.RemoveMember(spawned);
            GameState.gameManager.ResetTimeScale();
        }
        spawned.GetComponentInChildren<EffectorHighlighter>().enabled = true;
    }

    public void CancelSpawn(bool speedUp = true)
    {
        if(state != SpawnState.Idle)
            {
                //GameState.gameManager.planningCam.Follow = null;
                CameraLook(null);
                GameObject.Destroy(spawning.gameObject);
                spawning = null;
                state = SpawnState.Idle;
                if(GameState.gameManager.controlType == ControlType.RealTime && speedUp)
                {
                    GameState.gameManager.ResetTimeScale();
                }
            }
    }

    public void CameraLook(Transform spawn)
    {
        if(GameState.gameManager.controlType == ControlType.RealTime)
        {
            GameState.gameManager.realTimeTarget.AddMember(spawn, 1, 1);
        }
        else if(GameState.gameManager.controlType == ControlType.Plan)
        {
            GameState.gameManager.planningCam.Follow = spawn;
        }
    }
}
