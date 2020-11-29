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

    private Vector2 GetScale(float width, float height)
    {
        var scalerReferenceResolution = canvasScaler.referenceResolution;
        var widthScale = width / scalerReferenceResolution.x;
        var heightScale = height / scalerReferenceResolution.y;
        return new Vector2(widthScale, heightScale);
        
        /*switch (canvasScaler.screenMatchMode)
        {
            case CanvasScaler.ScreenMatchMode.MatchWidthOrHeight:
                var matchWidthOrHeight = canvasScaler.matchWidthOrHeight;
        
                return Mathf.Pow(widthScale, 1f - matchWidthOrHeight)*
                    Mathf.Pow(heightScale, matchWidthOrHeight);
            
            case CanvasScaler.ScreenMatchMode.Expand:
                return Mathf.Min(widthScale, heightScale);
            
            case CanvasScaler.ScreenMatchMode.Shrink:
                return Mathf.Max(widthScale, heightScale);
            
            default:
                Debug.Log("Error detecting screen match mode.");
                return 0;
        }*/
    
    }

    // Update is called once per frame
    void Update()
    {
        if(GameState.gameManager.gamePhase == GamePhase.Planning || GameState.gameManager.controlType == ControlType.RealTime)
        {
            if(!visible && spawner.state == SpawnState.Idle && Input.GetMouseButtonDown(1))
            {
                Vector2 scale = (panel.rect.size * canvas.scaleFactor)/2;
                Vector2 screenLoc = new Vector2(Mathf.Clamp(Input.mousePosition.x, scale.x, Screen.width-scale.x), Mathf.Clamp(Input.mousePosition.y, scale.y, Screen.height-scale.y));
                
                //Debug.Log($"Clicked: {Input.mousePosition} Width: {Screen.width} x Height: {Screen.height} on Canvas: {canvas.pixelRect.size}. Panel: {panel.rect.size}, size on screen: {panel.rect.size * canvas.scaleFactor}. ScreenLoc: {screenLoc}");
                
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
