using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using Cinemachine;

public enum GamePhase 
{
    Planning,
    Aiming,
    Execution,
    End,
    Popup
}

public enum ControlType
{
    RealTime,
    Plan
}

public enum GameMode
{
    MetroidVania,
    Normal,
    Menu
}

public class GameState : MonoBehaviour
{

    public delegate void GameReset();
    public static event GameReset OnReset;

    public GameMode gameMode = GameMode.Normal;
    
    public static GameState gameManager;
    public GamePhase gamePhase = GamePhase.Planning;

    public CinemachineVirtualCamera planningCam;
    public CinemachineVirtualCamera realTimeCam;
    public CinemachineVirtualCamera aimingCam;

    public CinemachineTargetGroup realTimeTarget;

    public Text phaseText;
    public Button planButton;
    public GameObject finishText;

    public Transform sub;
    [SerializeField]
    Vector3 subPos;
    public float timeSinceMove = 0;
    public float minChangeNumber = 0.1f;
    public float timeLimit = 3;

    public float timeScale = 0.3f;

    public ControlType controlType = ControlType.Plan;

    public TMP_Text popup;

    public GameObject escapeMenu;
    public ShowRadial radial;

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

        if(gameMode != GameMode.Menu)
            planButton = phaseText.GetComponentInChildren<Button>();
    }
    
    public void ResetTimeScale()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }

    public void SlowTime()
    {
        Time.timeScale = timeScale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        //Debug.Log($"Now operating at: {Time.timeScale}. Physics every: {Time.fixedDeltaTime}");        
    }

    public static bool IsStarted()
    {
        return GameState.gameManager.gamePhase == GamePhase.Execution;
    }

    public void StartRun()
    {
        GameState.gameManager.gamePhase = GamePhase.Execution;
        phaseText.text = GamePhase.Execution.ToString();
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
        if(gameMode != GameMode.Menu)
            StartPlanning();

    }

    public void Retry()
    {
        ResetTimeScale();
        escapeMenu.SetActive(false);
        if(OnReset != null)
            OnReset();
        if(gameMode != GameMode.Menu)        
            StartPlanning();
    }

    public void FullReset()
    {
        ResetTimeScale();
        if(LoadingScreen.Instance)
        {
            LoadingScreen.Instance.Show(SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name, LoadSceneMode.Single));
        }
        else
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        ResetTimeScale();
        if(LoadingScreen.Instance)
        {
            LoadingScreen.Instance.Show(SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single));
        }
        else
        {
            SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
        }
    }

    public void ShowPopup(string msg)
    {
        if(popup)
        {
            popup.text = msg;
            popup.gameObject.SetActive(true);
            EffectorSpawner.effectorSpawner.CancelSpawn(false); // Cancel whatever spawn we're doing.
            SlowTime();
            gamePhase = GamePhase.Popup;
        }
    }

    public void HidePopup()
    {
        popup.gameObject.SetActive(false);
        ResetTimeScale();
        gamePhase = GamePhase.Execution;
    }

    public void StartAiming()
    {
        if(radial)
        {
            radial.Hide();
        }
        if(controlType != ControlType.RealTime)
        {
            aimingCam.gameObject.SetActive(true);
            planningCam.gameObject.SetActive(false);
            realTimeCam.gameObject.SetActive(false);
            planningCam.LookAt = null;
        }
        else
        {
            aimingCam.gameObject.SetActive(false);
            planningCam.gameObject.SetActive(false);
            realTimeCam.gameObject.SetActive(true);
        }
        
        GameState.gameManager.gamePhase = GamePhase.Aiming;
        phaseText.text = GamePhase.Aiming.ToString();
        phaseText.gameObject.SetActive(true);
        planButton.gameObject.SetActive(false);
    }

    public void StartPlanning()
    {        
        if(controlType != ControlType.RealTime)
        {
            realTimeCam.gameObject.SetActive(false);
            aimingCam.gameObject.SetActive(false);
            planningCam.gameObject.SetActive(true);
            planningCam.Follow = sub;
            
            GameState.gameManager.gamePhase = GamePhase.Planning;
            phaseText.gameObject.SetActive(true);
            phaseText.text = GamePhase.Planning.ToString();
            planButton.gameObject.SetActive(true);
        }
        else
        {
            StartAiming();
        }
        
        
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && escapeMenu && gameMode != GameMode.Menu)
        {
            if(!escapeMenu.activeSelf)
            {
                EffectorSpawner.effectorSpawner.CancelSpawn();
                if(radial)
                {
                    radial.Hide();
                }
                SlowTime();
                escapeMenu.SetActive(true);
            }
            else
            {
                ResetTimeScale();
                escapeMenu.SetActive(false);
            }
        }
        
    }

    void LateUpdate()
    {
        if(gamePhase == GamePhase.Execution && sub && (gameMode == GameMode.Normal || gameMode == GameMode.Menu))
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
        if(gameMode != GameMode.Menu)
            finishText.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if(gameMode != GameMode.Menu)
            finishText.SetActive(false);

        if(OnReset != null)
            OnReset();
        if(gameMode != GameMode.Menu)        
            StartPlanning();
    }

    void OnDestroy()
    {
        SubmarineInput.OnLaunch -= StartRun;
    }


}
