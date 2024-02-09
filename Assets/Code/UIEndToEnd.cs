using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIEndToEnd : MonoBehaviour
{
    [Header("Player Info")]
    public Image progress_bar_back;
    public TMP_Text t_level_num;
    public TMP_Text t_experience;

    [Header("Other Info")]
    public TMP_Text t_energy_count;
    public TMP_Text t_money_count;
    public TMP_Text t_hard_count;

    public void Start()
    {
        if (!PlayerPrefs.HasKey("player_money"))
            PlayerPrefs.SetInt("player_money", 0);

        UpdateStats();
    }

    public void Update()
    {
        UpdateStats();
    }

    public void UpdateStats()
    {
        //Player Info
        t_level_num.text = GameController.curr_level_num + "";
        t_experience.text = GameController.curr_experience + "";
        progress_bar_back.fillAmount = GameController.curr_experience / 100;

        //Other Info
        t_energy_count.text = GameController.curr_energy_count + "";
        t_money_count.text = PlayerPrefs.GetInt("player_money") + "";
        t_hard_count.text = GameController.curr_hard_count + "";
    }

    public void But_OpenShop(int shop_panel_id)
    {
        switch (shop_panel_id)
        {
            case 1:
                //Открыть магазин 1
                break;
            case 2:
                //Открыть магазин 2
                break;
            case 3:
                //Открыть магазин 3
                break;
        }
    }

    public void But_Settings()
    {
        GameObject.Find("UI Settings").GetComponent<SettingsController>().OpenPopUp();
    }
}
