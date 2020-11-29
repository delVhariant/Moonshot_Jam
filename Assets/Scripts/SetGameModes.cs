using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetGameModes : MonoBehaviour
{
    
    
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += SceneChanged;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SceneChanged(Scene scene, LoadSceneMode mode)
    {
        if(GameState.gameManager.gameMode != GameMode.Menu)
        {
            GameState.gameManager.controlType = (ControlType)PlayerPrefs.GetInt("controls");
            GameState.gameManager.gameMode = (GameMode)PlayerPrefs.GetInt("gameMode");
        }
    }
}
