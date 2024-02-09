using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InversionController : MonoBehaviour
{
    public GameObject panel;
    public bool isTutorial;

    public Button but_inversion, but_no_inversion;

    public Slider sliderSounds, sliderMusic;
    public Image imgSounds, imgMusic;
    public Sprite sprSoundsOn, sprSoundsOff;
    public Sprite sprMusicOn, sprMusicOff;

    bool popUpOpen;

    public void OpenInversionPanel()
    {
        panel.SetActive(true);
        sliderSounds.value = PlayerPrefs.GetFloat("soundsValue");
        sliderMusic.value = PlayerPrefs.GetFloat("musicValue");

        if (!isTutorial)
        {
            if (PlayerPrefs.GetString("inversion") == "true")
            {
                but_inversion.interactable = false;
                but_no_inversion.interactable = true;
            }
            else
            {
                but_inversion.interactable = true;
                but_no_inversion.interactable = false;
            }
        }

        popUpOpen = true;
    }

    private void Update()
    {
        if (popUpOpen)
        {
            PlayerPrefs.SetFloat("soundsValue", sliderSounds.value);
            PlayerPrefs.SetFloat("musicValue", sliderMusic.value);

            if (sliderSounds.value > 0)
                imgSounds.sprite = sprSoundsOn;
            else
                imgSounds.sprite = sprSoundsOff;

            if (sliderMusic.value > 0)
                imgMusic.sprite = sprMusicOn;
            else
                imgMusic.sprite = sprMusicOff;
        }
    }

    public void But_Inversion()
    {
        PlayerPrefs.SetString("inversion", "true");

        if (!isTutorial)
        {
            but_inversion.interactable = false;
            but_no_inversion.interactable = true;
        }

        if (Application.loadedLevelName != "LevelSelect")
            SceneTransition.SwithToScene("LevelSelect");
    }

    public void But_NoInversion()
    {
        PlayerPrefs.SetString("inversion", "false");

        if (!isTutorial)
        {
            but_inversion.interactable = true;
            but_no_inversion.interactable = false;
        }

        if (Application.loadedLevelName != "LevelSelect")
            SceneTransition.SwithToScene("LevelSelect");
    }

    public void But_Closed()
    {
        //panel.SetActive(false);
        popUpOpen = false;
    }

    public void PrivacyPolicy()
    {
        Application.OpenURL("https://docs.google.com/document/d/1F1OrmW170yvqKWSih9DRLf-YXKne6VcUuz5mmBJ6-t8/edit");
    }
}
