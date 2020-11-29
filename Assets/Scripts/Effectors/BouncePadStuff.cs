using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePadStuff : EffectorBase
{
    //AudioSource source;
    public Animator animator;
    //public AudioClip bumpSound;

    private void OnCollisionEnter(Collision collision)
    {
        if(source && effectSound)
                source.PlayOneShot(effectSound);
        animator.SetTrigger("Bounce");
        //Debug.Log("Bounce");
    }
    void Start()
    {
        source = GetComponent<AudioSource>();
    }
}
