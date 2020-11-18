using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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

    public delegate void GameReset();
    public static event GameReset OnReset;
    
    public static GameState gameManager;
    public GamePhase gamePhase = GamePhase.Planning;

    public CinemachineVirtualCamera planningCam;
    public CinemachineVirtualCamera flyingCam;
    public CinemachineVirtualCamera aimingCam;

    public Text phaseText;
    public Button planButton;
    public GameObject finishText;
    public bool switchCam = false;

    public Transform sub;
    [SerializeField]
    Vector3 subPos;
    public float timeSinceMove = 0;
    public float minChangeNumber = 0.1f;
    public float timeLimit = 3;

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
        phaseText.text = GamePhase.Execution.ToString();
        if(switchCam)
            flyingCam.gameObject.SetActive(true);
    }

    public static void EndRun()
    {
        GameState.gameManager.gamePhase = GamePhase.End;
        GameState.gameManager.phaseText.text = GamePhase.Execution.ToString();
    }
    

    // Start is called before the first frame update
    void Start()
    {
        SubmarineInput.OnLaunch += StartRun;

        planButton = phaseText.GetComponentInChildren<Button>();
    }

    public void FullReset()
    {

    }

    public void StartAiming()
    {
        aimingCam.gameObject.SetActive(true);
        planningCam.gameObject.SetActive(false);
        GameState.gameManager.gamePhase = GamePhase.Aiming;
        phaseText.text = GamePhase.Aiming.ToString();
        planButton.gameObject.SetActive(false);
    }

    public void StartPlanning()
    {
        aimingCam.gameObject.SetActive(false);
        planningCam.gameObject.SetActive(true);
        planningCam.Follow = sub;
        GameState.gameManager.gamePhase = GamePhase.Planning;
        phaseText.gameObject.SetActive(true);
        phaseText.text = GamePhase.Planning.ToString();
        planButton.gameObject.SetActive(true);
    }


    void LateUpdate()
    {
        if(gamePhase == GamePhase.Execution && sub)
        {
            if(Vector3.Distance(subPos,sub.position) < minChangeNumber)
            {
                timeSinceMove += Time.deltaTime;
                if(timeSinceMove > timeLimit)
                {
                    StartCoroutine(Reload());
                }
            }
            else
            {
                timeSinceMove = 0f;
            }
            subPos = sub.position;
        }
    }

    IEnumerator Reload()
    {
        finishText.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        finishText.SetActive(false);
        if(OnReset != null)
            OnReset();
        StartPlanning();
    }
}
