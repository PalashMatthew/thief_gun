using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizationPopUp : MonoBehaviour
{
    public GameObject toggle_ru, toggle_en;

    public Image imgLang;
    public Sprite sprRu, sprEn;

    private void Start()
    {
        switch (PlayerPrefs.GetString("gameLanguage"))
        {
            case "en":
                toggle_en.SetActive(true);
                toggle_ru.SetActive(false);
                imgLang.sprite = sprEn;
                break;

            case "ru":
                toggle_en.SetActive(false);
                toggle_ru.SetActive(true);
                imgLang.sprite = sprRu;
                break;
        }
    }

    public void But_Change_Lang(string _local)
    {
        if (PlayerPrefs.GetString("gameLanguage") != _local)
        {
            PlayerPrefs.SetString("gameLanguage", _local);

            switch (_local)
            {
                case "en":
                    toggle_en.SetActive(true);
                    toggle_ru.SetActive(false);
                    imgLang.sprite = sprEn;
                    break;

                case "ru":
                    toggle_en.SetActive(false);
                    toggle_ru.SetActive(true);
                    imgLang.sprite = sprRu;
                    break;
            }

            GameObject.Find("LocalizationManager").GetComponent<LocalizationController>().ChangeLang(_local);
        }
    }
}
