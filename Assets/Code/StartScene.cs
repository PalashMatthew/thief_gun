using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    public GameObject ui;

    public void Start()
    {
        if (!PlayerPrefs.HasKey("last_level"))
        {
            PlayerPrefs.SetInt("last_level", 1);
        }

        if (!PlayerPrefs.HasKey("rebound"))
        {
            PlayerPrefs.SetString("rebound", "one_rebound");
        }

        if (!PlayerPrefs.HasKey("slow"))
        {
            PlayerPrefs.SetString("slow", "all_slow");
        }

        if (!PlayerPrefs.HasKey("inversion"))
        {
            PlayerPrefs.SetString("inversion", "false");
        }

        if (!PlayerPrefs.HasKey("tutor_complite"))
        {
            PlayerPrefs.SetInt("tutor_complite", 0);
        }

        if (!PlayerPrefs.HasKey("first_start"))
        {
            PlayerPrefs.SetInt("first_start", 0);
        }

        if (PlayerPrefs.GetInt("first_start") == 0)
        {
            ui.SetActive(true);
            StartCoroutine(Pause(0));
        } else
        {
            StartCoroutine(Pause(0));
        }
    }

    IEnumerator Pause(int sec)
    {
        yield return new WaitForSeconds(sec);
        PlayerPrefs.SetInt("first_start", 1);

        if (PlayerPrefs.GetInt("tutor_complite") == 0)
        {
            PlayerPrefs.SetInt("last_level", 1);
            SceneTransition.SwithToScene("Tutor_Level_1");
        }
        else
        {
            SceneTransition.SwithToScene("LevelSelect");
        }
    }
}
