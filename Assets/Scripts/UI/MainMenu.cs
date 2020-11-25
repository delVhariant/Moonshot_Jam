using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject levelSelect;
    public GameObject story;

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
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayClicked()
    {
        mainMenu.SetActive(false);
        levelSelect.SetActive(true);
        story.SetActive(false);
    }

    public void MainMenuClicked()
    {
        mainMenu.SetActive(true);
        levelSelect.SetActive(false);
        story.SetActive(false);
    }

    public void StoryClicked()
    {
        mainMenu.SetActive(false);
        levelSelect.SetActive(false);
        story.SetActive(true);
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
