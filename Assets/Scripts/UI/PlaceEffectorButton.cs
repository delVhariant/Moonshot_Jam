using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceEffectorButton : MonoBehaviour
{
    public ShowRadial radialMenu;
    public EffectorSpawner spawner;
    public GameObject effectorPrefab;

    // Start is called before the first frame update
    void Start()
    {
        if(GameState.gameManager.gameMode == GameMode.Normal && effectorPrefab)
        {
            GetComponent<Button>().enabled = true;
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
