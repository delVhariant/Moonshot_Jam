using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectorPickup : MonoBehaviour
{
    public Button effectorButton;
    public string message;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    virtual protected void OnTriggerEnter(Collider other)
    {
        if(!GameState.IsStarted())
            return;

        effectorButton.interactable = true;
        GameState.gameManager.ShowPopup(message);
        GameObject.Destroy(gameObject);
    }

}
