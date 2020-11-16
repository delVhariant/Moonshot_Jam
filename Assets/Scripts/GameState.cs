using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public enum GamePhase 
{
    Planning,
    Aiming,
    Execution,
    End
}
    

public class GameState : MonoBehaviour
{

    public static GameState gameManager;
    public GamePhase gamePhase = GamePhase.Planning;

    public CinemachineVirtualCamera planningCam;
    public CinemachineVirtualCamera flyingCam;
    
    public bool switchCam = false;

    void Awake()
    {
        // Shitty singleton code, not great practice but whatevs
        if(GameState.gameManager && GameState.gameManager != this)
        {
            this.enabled = false;
        }
        else if(!GameState.gameManager)
        {
            GameState.gameManager = this;
        }
    }
    
    public static bool IsStarted()
    {
        return GameState.gameManager.gamePhase == GamePhase.Execution;
    }

    public void StartRun()
    {
        GameState.gameManager.gamePhase = GamePhase.Execution;
        if(switchCam)
            flyingCam.gameObject.SetActive(true);
    }

    public static void EndRun()
    {
        GameState.gameManager.gamePhase = GamePhase.End;
    }
    

    // Start is called before the first frame update
    void Start()
    {
        SubmarineInput.OnLaunch += StartRun;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
