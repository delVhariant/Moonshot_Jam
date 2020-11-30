using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShowRadial : MonoBehaviour
{
    public static ShowRadial Instance;
    public RectTransform panel;
    public EffectorSpawner spawner;
    public bool visible = false;

    public Canvas canvas;
    public CanvasScaler canvasScaler;

    void Awake()
    {
        // Singleton logic:
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            this.enabled = false;
            return;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameState.gameManager.gamePhase == GamePhase.Planning || GameState.gameManager.controlType == ControlType.RealTime)
        {
            if(!visible && spawner.state == SpawnState.Idle && Input.GetMouseButtonDown(1))
            {
                Vector2 scale = (panel.rect.size)/2;
                Vector2 clicked = Input.mousePosition;
                clicked /= canvas.scaleFactor;
                Vector2 scaledScreen = new Vector2(Screen.width, Screen.height) / canvas.scaleFactor;
                Vector2 screenLoc = new Vector2(Mathf.Clamp(clicked.x, scale.x, scaledScreen.x-scale.x), Mathf.Clamp(clicked.y, scale.y, scaledScreen.y-scale.y));
                
                // Debug.Log($"Clicked: {clicked} Canvas: {canvas.pixelRect.size}. Panel: {panel.rect.size}, size on screen: {panel.rect.size * canvas.scaleFactor}. ScreenLoc: {screenLoc} Clamp: {Screen.width-scale.x}x{Screen.height-scale.y}");
                
                // scale.x *= GetScale(panel.rect.size.x, panel.rect.size.y).x;
                // scale.y *= GetScale(panel.rect.size.x, panel.rect.size.y).y;
                panel.anchoredPosition = screenLoc;
                Show();          
            }
            else if(visible && Input.GetMouseButtonDown(1))
            {
                Hide();
            }
        }
    }

    public void Show()
    {
        if(GameState.gameManager.controlType == ControlType.RealTime)
        {
            if(!GameState.IsStarted())
                return;

            GameState.gameManager.SlowTime();
        }

        panel.gameObject.SetActive(true);
        visible = true;
    }

    public void Hide()
    {
        panel.gameObject.SetActive(false);
        visible = false;
        if(GameState.gameManager.controlType == ControlType.RealTime && spawner.state == SpawnState.Idle)
        {
            GameState.gameManager.ResetTimeScale();
        }
    }
}
