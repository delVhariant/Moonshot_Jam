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

    public int impulseLimit;
    public PlaceEffectorButton impulseButton;
    public int bounceLimit;
    public PlaceEffectorButton bounceButton;
    public int bigLimit;
    public PlaceEffectorButton bigButton;
    public int smallLimit;
    public PlaceEffectorButton smallButton;
    public int teleLimit;
    public PlaceEffectorButton teleButton;


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
        GameState.OnReset += OnReset;
        if(GameState.gameManager.gameMode == GameMode.Challenge)
        {
            impulseButton.SetLimit(impulseLimit);
            bounceButton.SetLimit(bounceLimit);
            bigButton.SetLimit(bigLimit);
            smallButton.SetLimit(smallLimit);
            teleButton.SetLimit(teleLimit);
        }
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

    void OnReset()
    {
        CancelSpawn(false);
    }

    public void SpawnNew(GameObject e)
    {
        CameraLook(e.transform);
        spawning = e.GetComponent<EffectorBase>();
        state = SpawnState.Placing;
    }

    public void MoveSpawn(EffectorBase effector)
    {
        if(GameState.gameManager.gameMode == GameMode.Challenge)
        {
            switch(effector.type)
            {
                case EffectorType.ImpulseEffector:
                    impulseButton.Remove();
                    break;
                case EffectorType.BouncePad:
                    bounceButton.Remove();
                    break;
                case EffectorType.Embiggener:
                    bigButton.Remove();
                    break;
                case EffectorType.Shrinker:
                    smallButton.Remove();
                    break;
                case EffectorType.TeleporterExit:
                case EffectorType.TeleporterEntrance:
                    teleButton.Remove();
                    break;
                default:
                    break;
            }
        }
        
        SpawnNew(effector.gameObject);

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
        
        if(GameState.gameManager.gameMode == GameMode.Challenge)
        {
            switch(spawned.GetComponent<EffectorBase>().type)
            {
                case EffectorType.ImpulseEffector:
                    impulseButton.Spawn();
                    break;
                case EffectorType.BouncePad:
                    bounceButton.Spawn();
                    break;
                case EffectorType.Embiggener:
                    bigButton.Spawn();
                    break;
                case EffectorType.Shrinker:
                    smallButton.Spawn();
                    break;
                case EffectorType.TeleporterExit:
                    teleButton.Spawn();
                    break;
                default:
                    break;
            }
        }
        spawned.GetComponentInChildren<EffectorHighlighter>().enabled = true;
    }

    public void CancelSpawn(bool speedUp = true)
    {
        if(state != SpawnState.Idle)
            {
                //GameState.gameManager.planningCam.Follow = null;
                CameraLook(null);
                if(GameState.gameManager.controlType == ControlType.RealTime)
                {
                    GameState.gameManager.realTimeTarget.RemoveMember(spawning.transform);
                }
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
            if(spawn)
                GameState.gameManager.realTimeTarget.AddMember(spawn, 1, 1);
        }
        else if(GameState.gameManager.controlType == ControlType.Plan)
        {
            GameState.gameManager.planningCam.Follow = spawn;
        }
    }
    void OnDestroy()
    {
        GameState.OnReset -= OnReset;
    }
}
