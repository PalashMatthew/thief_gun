using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelSelectController : MonoBehaviour
{
    [Header("General")]
    public List<GameObject> obj_level;
    public List<string> level_names;
    public List<GameObject> img_level_obj;

    public RectTransform content;

    public GameObject panel_shop;

    [Header("Settings")]
    public TMP_Text t_rebound;
    public TMP_Text t_time_scale;
    public bool isRebound;
    public bool isTimeScale;


    public void Start()
    {
        content.localPosition = new Vector3(0, 5132.213f, 0);

        //SettingsClear();

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

        if (PlayerPrefs.GetString("rebound") == "all_rebound")
        {
            t_rebound.text = "All Rebound";
            isRebound = false;
        }
        else
        {
            t_rebound.text = "One Rebound";
            isRebound = true;
        }

        if (PlayerPrefs.GetString("slow") == "all_slow")
        {
            t_time_scale.text = "All Slow";
            isTimeScale = false;
        }
        else
        {
            t_time_scale.text = "Player Slow";
            isTimeScale = true;
        }

        CameraSettings();

        if (!PlayerPrefs.HasKey("inversion"))
        {
            PlayerPrefs.SetString("inversion", "false");
        }

        if (!PlayerPrefs.HasKey("tutor_complite"))
        {
            PlayerPrefs.SetInt("tutor_complite", 0);
        }

        if (PlayerPrefs.GetInt("tutor_complite") == 0)
        {
            PlayerPrefs.SetInt("last_level", 1);
            SceneTransition.SwithToScene("Tutor_Level_1");
        }

        if (PlayerPrefs.GetString("level_" + (1) + "_unlock") != "true")
        {
            img_level_obj[0].GetComponent<Animator>().SetTrigger("puls");
        } 
        else
        {
            for (int i = 1; i < 35; i++)
            {
                if (PlayerPrefs.GetString("level_" + (i) + "_unlock") != "true")
                {
                    img_level_obj[i - 2].GetComponent<Animator>().SetTrigger("puls");
                    return;
                }
            }
        }

        

        //if (!PlayerPrefs.HasKey("last_level"))
        //    img_level_obj[0].GetComponent<Animator>().SetTrigger("puls");

        //if (PlayerPrefs.GetInt("last_level") > 1 && PlayerPrefs.GetInt("last_level") < 33)
        //    img_level_obj[PlayerPrefs.GetInt("last_level")].GetComponent<Animator>().SetTrigger("puls");
    }

    public void But_PlayLevel(int level_num)
    {
        if (PlayerPrefs.GetString("level_" + (level_num + 1) + "_unlock") == "true")
        {
            PlayerPrefs.SetInt("last_level", level_num + 1);

            SceneTransition.SwithToScene(level_names[level_num]);
        }
    }

    public void But_Shop()
    {
        panel_shop.SetActive(true);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            But_UnlockAll();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            PlayerPrefs.SetInt("player_money", 100000);
        }
    }

    public void But_UnlockAll()
    {
        for (int i = 0; i <= 30; i++)
        {
            PlayerPrefs.SetString("level_" + i + "_unlock", "true");
        }

        Application.LoadLevel(Application.loadedLevel);
    }

    public void But_ClearSave()
    {
        PlayerPrefs.DeleteAll();
        Application.LoadLevel(Application.loadedLevel);
    }

    void SettingsClear()
    {
        isRebound = false;
        t_rebound.text = "All Rebound";
        PlayerPrefs.SetString("rebound", "all_rebound");

        isTimeScale = false;
        t_time_scale.text = "All Slow";
        PlayerPrefs.SetString("slow", "all_slow");
    }

    public void But_Rebound()
    {
        isRebound = !isRebound;

        if (isRebound)
        {
            t_rebound.text = "One Rebound";
            PlayerPrefs.SetString("rebound", "one_rebound");
        } 
        else
        {
            t_rebound.text = "All Rebound";
            PlayerPrefs.SetString("rebound", "all_rebound");
        }
    }

    public void But_TimeScale()
    {
        isTimeScale = !isTimeScale;

        if (isTimeScale)
        {
            t_time_scale.text = "Player Slow";
            PlayerPrefs.SetString("slow", "player_slow");
        }
        else
        {
            t_time_scale.text = "All Slow";
            PlayerPrefs.SetString("slow", "all_slow");
        }
    }

    void CameraSettings()
    {
        int level_active = PlayerPrefs.GetInt("last_level");
        
        switch (level_active)
        {
            case 1:
                content.localPosition = new Vector3(0, 6035.168f, 0);
                break;
            case 2:
                content.localPosition = new Vector3(0, 6035.168f, 0);
                break;
            case 3:
                content.localPosition = new Vector3(0, 5689f, 0);
                break;
            case 4:
                content.localPosition = new Vector3(0, 5550f, 0);
                break;
            case 5:
                content.localPosition = new Vector3(0, 5321f, 0);
                break;
            case 6:
                content.localPosition = new Vector3(0, 5121f, 0);
                break;
            case 7:
                content.localPosition = new Vector3(0, 4934f, 0);
                break;
            case 8:
                content.localPosition = new Vector3(0, 4821f, 0);
                break;
            case 9:
                content.localPosition = new Vector3(0, 4666f, 0);
                break;
            case 10:
                content.localPosition = new Vector3(0, 4495, 0);
                break;
            case 11:
                content.localPosition = new Vector3(0, 4333f, 0);
                break;
            case 12:
                content.localPosition = new Vector3(0, 4081f, 0);
                break;
            case 13:
                content.localPosition = new Vector3(0, 3835, 0);
                break;
            case 14:
                content.localPosition = new Vector3(0, 3609, 0);
                break;
            case 15:
                content.localPosition = new Vector3(0, 3373, 0);
                break;
            case 16:
                content.localPosition = new Vector3(0, 3266, 0);
                break;
            case 17:
                content.localPosition = new Vector3(0, 3108, 0);
                break;
            case 18:
                content.localPosition = new Vector3(0, 2837, 0);
                break;
            case 19:
                content.localPosition = new Vector3(0, 2621, 0);
                break;
            case 20:
                content.localPosition = new Vector3(0, 2330, 0);
                break;
            case 21:
                content.localPosition = new Vector3(0, 2156, 0);
                break;
            case 22:
                content.localPosition = new Vector3(0, 1904, 0);
                break;
            case 23:
                content.localPosition = new Vector3(0, 1655, 0);
                break;
            case 24:
                content.localPosition = new Vector3(0, 1448, 0);
                break;
            case 25:
                content.localPosition = new Vector3(0, 1209, 0);
                break;
            case 26:
                content.localPosition = new Vector3(0, 1057, 0);
                break;
            case 27:
                content.localPosition = new Vector3(0, 957, 0);
                break;
            case 28:
                content.localPosition = new Vector3(0, 627, 0);
                break;
            case 29:
                content.localPosition = new Vector3(0, 527, 0);
                break;
            case 30:
                content.localPosition = new Vector3(0, 259, 0);
                break;
            case 31:
                content.localPosition = new Vector3(0, 0, 0);
                break;
            case 32:
                content.localPosition = new Vector3(0, 0, 0);
                break;
            case 33:
                content.localPosition = new Vector3(0, 0, 0);
                break;
        }
    }
}
