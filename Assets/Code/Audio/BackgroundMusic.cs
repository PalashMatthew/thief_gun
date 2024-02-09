using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    AudioSource aud;
    public AudioClip clip_1, clip_2, clip_3, clip_4;

    int curr_clip;

    public void Start()
    {
        aud = GetComponent<AudioSource>();

        DontDestroyOnLoad(gameObject);

        curr_clip = 1;

        StartMusic();

        StartCoroutine(CheckMusic());
    }

    IEnumerator CheckMusic()
    {
        yield return new WaitForSeconds(1);
        if (!aud.isPlaying)
        {
            curr_clip++;

            if (curr_clip > 4)
                curr_clip = 1;

            StartMusic();
        }

        StartCoroutine(CheckMusic());
    }

    void StartMusic()
    {
        switch (curr_clip)
        {
            case 1:
                aud.clip = clip_1;
                break;
            case 2:
                aud.clip = clip_2;
                break;
            case 3:
                aud.clip = clip_3;
                break;
            case 4:
                aud.clip = clip_4;
                break;
        }

        aud.Play();
    }

    void LateUpdate()
    {
        aud.volume = PlayerPrefs.GetFloat("musicValue");
    }
}
