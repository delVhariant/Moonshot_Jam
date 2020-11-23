using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowRadial : MonoBehaviour
{
    public RectTransform panel;
    public EffectorSpawner spawner;
    public bool visible = false;
    
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
                panel.anchoredPosition = new Vector2(Mathf.Clamp(Input.mousePosition.x, panel.sizeDelta.x/2, Screen.width-panel.sizeDelta.x/2), Mathf.Clamp(Input.mousePosition.y, panel.sizeDelta.y/2, Screen.height-panel.sizeDelta.y/2));
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
