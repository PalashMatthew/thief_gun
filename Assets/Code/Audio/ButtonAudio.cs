using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAudio : MonoBehaviour
{
    AudioSource aud;
    public AudioClip clip_press;


    public void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    public void Play_Press()
    {
        aud.clip = clip_press;
        aud.Play();
    }
}
