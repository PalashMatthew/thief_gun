using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private void Awake()
    {
        
        if (!PlayerPrefs.HasKey("soundsValue"))
        {
            PlayerPrefs.SetFloat("soundsValue", 0.3f);
            PlayerPrefs.SetFloat("musicValue", 0.172f);
        }
    }
}
