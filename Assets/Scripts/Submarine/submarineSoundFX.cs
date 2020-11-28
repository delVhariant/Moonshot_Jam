using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class submarineSoundFX : MonoBehaviour
{
    AudioSource source;
    public AudioClip bumpSound;

    private void OnCollisionEnter(Collision collision)
    {
        source.PlayOneShot(bumpSound);
    }
    void Start()
    {
        source = GetComponent<AudioSource>();
    }
}
