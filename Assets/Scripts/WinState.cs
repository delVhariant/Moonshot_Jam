﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.UI;

public class WinState : MonoBehaviour
{
    public static WinState Instance;
    public VisualEffect explosion;
    public AudioClip explosionSound;

    

    void Awake()
    {
        // Singleton logic:
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            this.enabled = false;
            return;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter()
    {
        GameState.gameManager.Win();
    }

    public void TriggerExplosion()
    {
        if(explosion)
        {
            Camera.main.GetComponent<AudioSource>().loop = false;
            Camera.main.GetComponent<AudioSource>().Stop();
            Camera.main.GetComponent<AudioSource>().PlayOneShot(explosionSound);
            explosion.SendEvent("explode");
        }
    }

}
