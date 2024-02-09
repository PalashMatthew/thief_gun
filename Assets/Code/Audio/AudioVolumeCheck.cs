using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVolumeCheck : MonoBehaviour
{
    AudioSource aud;

    public void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    void LateUpdate()
    {
        aud.volume = PlayerPrefs.GetFloat("soundsValue");
    }
}
