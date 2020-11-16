using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceEffectorButton : MonoBehaviour
{
    public ShowRadial radialMenu;
    public EffectorSpawner spawner;
    public GameObject effectorPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
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
                GameState.gameManager.planningCam.Follow = e.transform;
                spawner.SpawnNew(e);
                radialMenu.Hide();
            }
        }
    }
}
