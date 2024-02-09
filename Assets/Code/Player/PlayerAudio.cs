using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    AudioSource aud;
    public AudioClip clip_die;
    public AudioClip clip_bump;
    public AudioClip clip_key;
    public AudioClip clip_win;


    public void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    public void Play_Die()
    {
        aud.clip = clip_die;
        aud.Play();
    }

    public void Play_Bump()
    {
        aud.clip = clip_bump;
        aud.Play();
    }

    public void Play_Win()
    {
        aud.clip = clip_win;
        aud.Play();
    }

    public void Play_Key()
    {
        aud.clip = clip_key;
        aud.Play();
    }
}
