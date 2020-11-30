using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlaceEffectorButton : MonoBehaviour
{
    public ShowRadial radialMenu;
    public EffectorSpawner spawner;
    public GameObject effectorPrefab;
    public Text limitText;
    Button button;

    public float threshold = 0.05f;

    public int limit=-1;
    bool unlimited = true;
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().alphaHitTestMinimumThreshold = threshold;
        button = GetComponent<Button>();
        
        if(GameState.gameManager.gameMode != GameMode.MetroidVania && effectorPrefab)
        {
            if(unlimited || limit > 0 || GameState.gameManager.gameMode != GameMode.Challenge)
            {
                //Debug.Log($"re-enabling button {transform.name}");
                button.interactable = true;
            }
            
            
        }
        if(GameState.gameManager.gameMode != GameMode.Challenge)
        {
            limitText.gameObject.SetActive(false);
        }
        if(!effectorPrefab)
        {
            this.enabled = false;
        }

    }

    public void SetLimit(int l)
    {
        limit = l;
        Debug.Log($"Setting: {transform.name} limit to: {limit}");
        if(!limitText)
        {
            limitText = GetComponentInChildren<Text>(true);
        }
        if(!button)
        {
            button = GetComponent<Button>();
        }
        limitText.gameObject.SetActive(true);
        if(limit >=0)
        {
            unlimited = false;
            limitText.text = limit.ToString();
            if(limit == 0)
            {
                button.interactable = false;
            }
        }
    }

    public void Spawn()
    {
        if(!unlimited)
        {
            limit--;
            limitText.text = limit.ToString();
            if(limit<=0)
            {
                button.interactable = false;
            }
        }
    }

    public void Remove()
    {
        if(!unlimited)
        {
            limit++;
            limitText.text = limit.ToString();
            if(limit > 0)
            {
               button.interactable = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonClicked()
    {
        if(radialMenu.visible)
        {
            if(effectorPrefab)
            {
                GameObject e = Instantiate(effectorPrefab, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.x)), effectorPrefab.transform.rotation);
                spawner.SpawnNew(e);
                radialMenu.Hide();
            }
        }
    }
}
