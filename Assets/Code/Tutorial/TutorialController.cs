using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialController : MonoBehaviour
{
    public static bool isTutorialActive;
    public GameObject tutorial_canvas;
    public GameObject text_panel;
    public GameObject video_panel;

    public GameObject BackCanvas;

    public int level_id;

    public TMP_Text t_tutor;
    private int curr_text_num;

    [Header("Text")]
    public string tutor_text_1_1;
    public string tutor_text_1_2;

    public string tutor_text_2_1;
    public string tutor_text_2_2;
    public string tutor_text_2_3;
    public string tutor_text_2_4;

    public string tutor_text_3_1;

    public string tutor_text_1_1_en;
    public string tutor_text_1_2_en;

    public string tutor_text_2_1_en;
    public string tutor_text_2_2_en;
    public string tutor_text_2_3_en;
    public string tutor_text_2_4_en;

    public string tutor_text_3_1_en;

    public bool isLevel1;
    public bool isLevel3;

    public GameObject fake_joy;

    public bool lvl_1_plus;

    public void Start()
    {       
        if (!isLevel1 && !isLevel3)
        {
            isTutorialActive = true;
            tutorial_canvas.SetActive(true);
            text_panel.SetActive(true);
            video_panel.SetActive(false);


            curr_text_num = 1;

            if (level_id == 1)
            {
                if (PlayerPrefs.GetString("gameLanguage") == "ru")
                    t_tutor.text = tutor_text_1_1;
                else
                    t_tutor.text = tutor_text_1_1_en;

                fake_joy.SetActive(false);
            }

            if (level_id == 2)
            {
                if (PlayerPrefs.GetString("gameLanguage") == "ru")
                    t_tutor.text = tutor_text_2_1;
                else
                    t_tutor.text = tutor_text_2_1_en;

                fake_joy.SetActive(false);
            }
        }
    }

    public void Update()
    {
        if (isTutorialActive && !isLevel1 && !isLevel3)
        {
            BackCanvas.SetActive(false);
        }

        if (isLevel1 || isLevel3)
        {
            if (PlayerPrefs.GetString("gameLanguage") == "ru")
                t_tutor.text = tutor_text_3_1;
            else
                t_tutor.text = tutor_text_3_1_en;
        }
    }

    public void But_NextText()
    {
        curr_text_num++;

        if (level_id == 1)
        {
            if (curr_text_num == 2)
            {
                if (PlayerPrefs.GetString("gameLanguage") == "ru")
                    t_tutor.text = tutor_text_1_2;
                else
                    t_tutor.text = tutor_text_1_2_en;
            }

            if (curr_text_num == 3)
            {
                text_panel.SetActive(false);
                video_panel.SetActive(true);
            }

            if (curr_text_num == 4)
            {
                fake_joy.SetActive(true);
                fake_joy.GetComponent<Animator>().SetTrigger("tutor_on");
                video_panel.SetActive(false);
                BackCanvas.SetActive(true);
                isTutorialActive = false;
                tutorial_canvas.SetActive(false);
            }
        }

        if (level_id == 2)
        {
            if (curr_text_num == 2)
            {
                if (PlayerPrefs.GetString("gameLanguage") == "ru")
                    t_tutor.text = tutor_text_2_2;
                else
                    t_tutor.text = tutor_text_2_2_en;
            }

            if (curr_text_num == 3)
            {
                if (PlayerPrefs.GetString("gameLanguage") == "ru")
                    t_tutor.text = tutor_text_2_3;
                else
                    t_tutor.text = tutor_text_2_3_en;
            }

            if (curr_text_num == 4)
            {
                if (PlayerPrefs.GetString("gameLanguage") == "ru")
                    t_tutor.text = tutor_text_2_4;
                else
                    t_tutor.text = tutor_text_2_4_en;

                //text_panel.SetActive(false);
                //video_panel.SetActive(true);
            }

            if (curr_text_num == 5)
            {
                text_panel.SetActive(false);
                video_panel.SetActive(true);
                //fake_joy.SetActive(true);
                //fake_joy.GetComponent<Animator>().SetTrigger("tutor_on");

                //video_panel.SetActive(false);
                //BackCanvas.SetActive(true);
                //isTutorialActive = false;
                //tutorial_canvas.SetActive(false);
            }

            if (curr_text_num == 6)
            {
                fake_joy.SetActive(true);
                fake_joy.GetComponent<Animator>().SetTrigger("tutor_2_on");

                video_panel.SetActive(false);
                BackCanvas.SetActive(true);
                isTutorialActive = false;
                tutorial_canvas.SetActive(false);
            }
        }
    }

    public void ButGoToTutorLevel2()
    {
        SceneTransition.SwithToScene("Tutor_Level_2");
    }

    public void TutorComplite()
    {
        PlayerPrefs.SetInt("tutor_complite", 1);
    }

    public void CloseTutor()
    {
        video_panel.SetActive(false);
        BackCanvas.SetActive(true);
        isTutorialActive = false;
        tutorial_canvas.SetActive(false);

        PlayerPrefs.SetString("map_tutorial", "true");

        fake_joy.GetComponent<Animator>().SetTrigger("move");
    }

    public void CloseTrapTutor()
    {
        video_panel.SetActive(false);
        BackCanvas.SetActive(true);
        isTutorialActive = false;
        tutorial_canvas.SetActive(false);

        PlayerPrefs.SetString("trap_tutorial", "true");

        fake_joy.GetComponent<Animator>().SetTrigger("move");
    }

    public void ShowTutor()
    {
        isTutorialActive = true;
        tutorial_canvas.SetActive(true);
        text_panel.SetActive(true);
        video_panel.SetActive(false);

        fake_joy.GetComponent<Animator>().SetTrigger("idle");

        curr_text_num = 1;
    }
}
