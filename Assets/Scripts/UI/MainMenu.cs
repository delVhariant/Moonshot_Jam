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
    public GameObject story;

    public CinemachineVirtualCamera mainCam;
    public CinemachineVirtualCamera levelCam;
    public CinemachineVirtualCamera storyCam;

    public TMP_Dropdown levelTypeDropdown;

    public TMP_Dropdown controlTypeDropdown;
    
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

        MainMenuClicked();
        StopAllCoroutines();
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayClicked()
    {
        levelCam.enabled = true;
        mainCam.enabled = false;        
        storyCam.enabled = false;
        mainMenu.SetActive(false);
        story.SetActive(false);
        StartCoroutine(ChangeMenu(levelSelect));
    }

    public void MainMenuClicked()
    {
        mainCam.enabled = true;
        levelCam.enabled = false;
        storyCam.enabled = false;
        levelSelect.SetActive(false);
        story.SetActive(false);
        StartCoroutine(ChangeMenu(mainMenu));
    }

    public void StoryClicked()
    {
        storyCam.enabled = true;
        mainCam.enabled = false;
        levelCam.enabled = false;        
        mainMenu.SetActive(false);
        levelSelect.SetActive(false);
        //story.SetActive(true);
        StartCoroutine(ChangeMenu(story));
    }

    public IEnumerator ChangeMenu(GameObject menuSection)
    {
        yield return new WaitForSeconds(2);
        menuSection.SetActive(true);
    }

    public void LevelTypeChanged(TMP_Dropdown change)
    {
        Debug.Log($"Clicked: {change.value}");
        if(change.value == 0)
        {
            controlTypeDropdown.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            controlTypeDropdown.transform.parent.gameObject.SetActive(false);
        }
    }

    //Ouput the new value of the Dropdown into Text
    public void ControlTypeChanged(TMP_Dropdown change)
    {
        Debug.Log($"Clicked: {change.value}");
    }
}
