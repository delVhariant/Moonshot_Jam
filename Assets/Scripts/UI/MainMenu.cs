using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject levelSelect;
    public GameObject howToPlay;
    public GameObject story;

    public CinemachineVirtualCamera mainCam;
    public CinemachineVirtualCamera levelCam;
    public CinemachineVirtualCamera htpCam;
    public CinemachineVirtualCamera storyCam;

    public PlayStory storyScript;

    public TMP_Dropdown levelTypeDropdown;

    public TMP_Dropdown controlTypeDropdown;

    public GameObject normalLevels;
    public GameObject challengeLevels;
    public GameObject metroidLevels;
    
    // Start is called before the first frame update
    void Start()
    {

        //Add listener for when the value of the Dropdown changes, to take action
        levelTypeDropdown.onValueChanged.AddListener(delegate {
            LevelTypeChanged(levelTypeDropdown);
        });

        controlTypeDropdown.onValueChanged.AddListener(delegate {
            ControlTypeChanged(controlTypeDropdown);
        });

        // Make sire the main menu is visible.
        MainMenuClicked();
        StopAllCoroutines();
        mainMenu.SetActive(true);
    }

    
    // Update is called once per frame
    void Update()
    {
        switch(controlTypeDropdown.value)
        {
            case 0:
                PlayerPrefs.SetInt("controls", (int)ControlType.Plan);
                break;
            case 1:
                PlayerPrefs.SetInt("controls", (int)ControlType.RealTime);
                break;

        }

        // Normal = 0;
        // Challege = 1;
        // MetroidVania == 2;
        switch(levelTypeDropdown.value)
        {
            case 0:
                PlayerPrefs.SetInt("gameMode", (int)GameMode.Normal);
                break;
            case 1:
                PlayerPrefs.SetInt("gameMode", (int)GameMode.Challenge);
                break;
            case 2:
                PlayerPrefs.SetInt("gameMode", (int)GameMode.MetroidVania);
                break;

        }
    }

    public void PlayClicked()
    {
        levelCam.enabled = true;
        mainCam.enabled = false;        
        htpCam.enabled = false;
        storyCam.enabled = false;
        mainMenu.SetActive(false);
        howToPlay.SetActive(false);
        story.SetActive(false);
        StartCoroutine(ChangeMenu(levelSelect));
    }

    public void MainMenuClicked()
    {
        mainCam.enabled = true;
        levelCam.enabled = false;
        htpCam.enabled = false;
        storyCam.enabled = false;
        levelSelect.SetActive(false);
        howToPlay.SetActive(false);
        story.SetActive(false);
        StartCoroutine(ChangeMenu(mainMenu));
    }

    public void HowToPlayClicked()
    {
        htpCam.enabled = true;
        mainCam.enabled = false;
        levelCam.enabled = false; 
        storyCam.enabled = false;       
        mainMenu.SetActive(false);
        levelSelect.SetActive(false);
        story.SetActive(false);
        StartCoroutine(ChangeMenu(howToPlay));
    }

    public void StoryClicked()
    {
        htpCam.enabled = false;
        mainCam.enabled = false;
        levelCam.enabled = false; 
        storyCam.enabled = true;       
        mainMenu.SetActive(false);
        levelSelect.SetActive(false);
        howToPlay.SetActive(false);
        StartCoroutine(ChangeMenu(story));
        StartCoroutine(PlayStory());
    }

    public IEnumerator PlayStory()
    {
        yield return new WaitForSeconds(3);
        storyScript.PlayDialog();
    }

    public IEnumerator ChangeMenu(GameObject menuSection)
    {
        yield return new WaitForSeconds(2);
        menuSection.SetActive(true);
    }

    public void LevelTypeChanged(TMP_Dropdown change)
    {
        switch(change.value)
        {
            case 0:
                controlTypeDropdown.enabled = true;
                normalLevels.SetActive(true);
                challengeLevels.SetActive(false);
                metroidLevels.SetActive(false);
                
                break;
            case 1:
                controlTypeDropdown.SetValueWithoutNotify(0);
                controlTypeDropdown.enabled = false;
                normalLevels.SetActive(false);
                challengeLevels.SetActive(true);
                metroidLevels.SetActive(false);
                break;
            case 2:
                controlTypeDropdown.SetValueWithoutNotify(1);
                controlTypeDropdown.enabled = false;
                normalLevels.SetActive(false);
                challengeLevels.SetActive(false);
                metroidLevels.SetActive(true);
                break;

        }
    }

    //Ouput the new value of the Dropdown into Text
    public void ControlTypeChanged(TMP_Dropdown change)
    {
        // Debug.Log($"Clicked: {change.value}");
    }
}
