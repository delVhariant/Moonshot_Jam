using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnState
{
    Placing,
    Aiming,
    Idle
}

public class EffectorSpawner : MonoBehaviour
{
    public SpawnState state = SpawnState.Idle;

    public GameObject spawning;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(spawning)
        {
            if(state == SpawnState.Placing)
            {
                spawning.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.x));
                if(Input.GetMouseButtonDown(0))
                {
                    GameState.gameManager.planningCam.Follow = null;
                    state = SpawnState.Aiming;
                }
            }
            else if(state == SpawnState.Aiming)
            {
                Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.x));
                spawning.transform.LookAt(point);
                if(Input.GetMouseButtonDown(0))
                {
                    state = SpawnState.Idle;
                    spawning = null;
                    
                }
            }
        }
    }

    public void SpawnNew(GameObject e)
    {
        spawning = e;
        state = SpawnState.Placing;
    }
}
