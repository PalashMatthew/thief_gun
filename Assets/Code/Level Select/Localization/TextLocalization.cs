using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextLocalization : MonoBehaviour
{
    public string ru, en;

    TMP_Text text;


    private void Start()
    {
        text = GetComponent<TMP_Text>();

        LocalizationController.OnChangeLanguage += ChangeText;

        ChangeText();
    }

    void ChangeText()
    {
        if (PlayerPrefs.GetString("gameLanguage") == "ru")
        {
            text.text = ru;
        }

        if (PlayerPrefs.GetString("gameLanguage") == "en")
        {
            text.text = en;
        }
    }
}
