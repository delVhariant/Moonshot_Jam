﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterEffector : EffectorBase
{

    public GameObject exitPrefab;
    public TeleporterEffector pair;
    public ObjectScaler scaler;

    public bool isExit;

    public float speed;
    float timer;
    public float bigScale;

    public float maxDistance = 30;
    
    GameObject teleportTarget;
    Vector3 velocity, angularVelocity;
    
    


    
    // Start is called before the first frame update
    void Start()
    {
        if(!scaler)
            gameObject.SetActive(false);

    }

    // Update is called once per frame
    protected override void Update()
    {
        
        if(EffectorSpawner.effectorSpawner.spawning == this && isExit && pair)
        {
            if(Vector3.Distance(transform.position, pair.transform.position) > maxDistance)
            {
                transform.position = pair.transform.position + Vector3.ClampMagnitude(transform.position - pair.transform.position,maxDistance);
                //Debug.DrawRay(pair.transform.position,Vector3.ClampMagnitude(transform.position - pair.transform.position, maxDistance), Color.red);
            }
        }
        base.Update();


        if(timer > 0 && teleportTarget && pair)
        {
            timer -= speed * Time.deltaTime;
            if(timer <= 0)
            {
                Vector3 n = Vector3.Normalize(pair.transform.forward);
                Debug.Log($"Launching Speed: {Vector3.Magnitude(velocity)} at dir: {n} = {n * Vector3.Magnitude(velocity)}");
                Rigidbody rb = teleportTarget.GetComponent<Rigidbody>();
                rb.velocity = Vector3.zero;
                rb.angularVelocity = rb.velocity;
                teleportTarget.transform.position = pair.transform.position;
                teleportTarget.transform.rotation = pair.transform.rotation;
                rb.AddForce(Vector3.Magnitude(velocity) * n, ForceMode.VelocityChange);                
            }
        }
    }

    public void SetPair(TeleporterEffector other)
    {
        other.pair = this;
        pair = other;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        scaler.Scale(bigScale, speed);
        timer=1;
        if(pair && !isExit)
        {
            teleportTarget = other.gameObject;
            velocity = other.GetComponent<Rigidbody>().velocity;
            angularVelocity = other.GetComponent<Rigidbody>().angularVelocity;
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        scaler.Scale(1, speed);
        if(timer != 0)
        {
            if(teleportTarget == other.gameObject)
                teleportTarget = null;
            timer=0;
        }
        
    }

    override protected void PerformAiming()
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.x));
        transform.LookAt(point);
        if(Input.GetMouseButtonDown(0))
        {
            if(!isExit)
            {
                if(exitPrefab)
                {
                    GameObject e = Instantiate(exitPrefab, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.x)), exitPrefab.transform.rotation);
                    SetPair(e.GetComponent<TeleporterEffector>());
                    GameState.gameManager.planningCam.Follow = e.transform;
                    EffectorSpawner.effectorSpawner.SpawnNew(e);
                }
            }
            else
            {
                EffectorSpawner.effectorSpawner.state = SpawnState.Idle;
                EffectorSpawner.effectorSpawner.spawning = null;
            }
        }
    }
}
