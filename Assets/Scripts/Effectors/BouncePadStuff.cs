using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePadStuff : MonoBehaviour
{
    AudioSource source;
    public Animator animator;
    public AudioClip bumpSound;

    private void OnCollisionEnter(Collision collision)
    {
        source.PlayOneShot(bumpSound);
        animator.SetTrigger("Bounce");
        Debug.Log("Bounce");
    }
    void Start()
    {
        source = GetComponent<AudioSource>();
    }
}
