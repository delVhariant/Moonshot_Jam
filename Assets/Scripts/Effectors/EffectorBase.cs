using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectorBase : MonoBehaviour
{
    public bool aiming = false;
    public AudioSource source;
    public AudioClip effectSound;
     void Awake()
    {

    }
    
    // Start is called before the first frame update
    void Start()
    {
    //source = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    protected virtual void Update()
    {

    }

    void LateUpdate()
    {}

    void FixedUpdate()
    {}

    virtual protected void OnTriggerEnter(Collider other)
    {
        if(!GameState.IsStarted())
            return;
    }

    virtual protected void OnTriggerStay(Collider other)
    {
        if(!GameState.IsStarted())
            return;
    }

    virtual protected void OnTriggerExit(Collider other)
    {
        if(!GameState.IsStarted())
            return;
    }

    virtual public void PerformAiming()
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.x));
        transform.LookAt(point);
        if(Input.GetMouseButtonDown(0))
        {
            EffectorSpawner.effectorSpawner.FinishSpawn(transform);
        }
    }

    public virtual void MoveEffector()
    {
        EffectorSpawner.effectorSpawner.SpawnNew(gameObject);
    }

}
