using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectorBase : MonoBehaviour
{
    public bool aiming = false;
    void Awake()
    {

    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        /*if(aiming && EffectorSpawner.effectorSpawner.spawning == this && EffectorSpawner.effectorSpawner.state == SpawnState.Aiming)
        {
            PerformAiming();
        }*/
    }

    void LateUpdate()
    {}

    void FixedUpdate()
    {}

    virtual protected void OnTriggerEnter(Collider other)
    {

    }

    virtual protected void OnTriggerStay(Collider other)
    {
    
    }

    virtual protected void OnTriggerExit(Collider other)
    {

    }

    virtual public void PerformAiming()
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.x));
        transform.LookAt(point);
        if(Input.GetMouseButtonDown(0))
        {
            EffectorSpawner.effectorSpawner.state = SpawnState.Idle;
            EffectorSpawner.effectorSpawner.spawning = null;            
        }
    }

}
