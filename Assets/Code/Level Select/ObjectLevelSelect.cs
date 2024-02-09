using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectLevelSelect : MonoBehaviour
{
    [Header("General")]
    public int level_num;
    public Image img_star_1, img_star_2, img_star_3;
    public Sprite spr_star_full, spr_star_empty;
    public TMP_Text t_level_num;
    public Image img_lock;

    public void Start()
    {
        ChekingAll();

        t_level_num.text = level_num + "";
    }

    void ChekingAll()
    {
        if (!PlayerPrefs.HasKey("level_" + level_num + "_unlock"))
        {
            if (level_num != 1)
                PlayerPrefs.SetString("level_" + level_num + "_unlock", "false");
            else
                PlayerPrefs.SetString("level_" + level_num + "_unlock", "true");
        }

        if (PlayerPrefs.GetString("level_" + level_num + "_unlock") == "true")
        {
            img_lock.gameObject.SetActive(false);
            img_star_1.gameObject.SetActive(true);
            img_star_2.gameObject.SetActive(true);
            img_star_3.gameObject.SetActive(true);
            t_level_num.gameObject.SetActive(true);

            CheckingStars();
        } else
        {
            img_lock.gameObject.SetActive(true);
            img_star_1.gameObject.SetActive(false);
            img_star_2.gameObject.SetActive(false);
            img_star_3.gameObject.SetActive(false);
            t_level_num.gameObject.SetActive(false);
        }
    }

    void CheckingStars()
    {
        if (PlayerPrefs.GetString("level_" + level_num + "_success") == "true")
        {
            int stars_count = PlayerPrefs.GetInt("level_" + level_num + "_stars_count");

            switch (stars_count)
            {
                case 1:
                    img_star_1.sprite = spr_star_full;
                    img_star_2.sprite = spr_star_empty;
                    img_star_3.sprite = spr_star_empty;
                    break;
                case 2:
                    img_star_1.sprite = spr_star_full;
                    img_star_2.sprite = spr_star_full;
                    img_star_3.sprite = spr_star_empty;
                    break;
                case 3:
                    img_star_1.sprite = spr_star_full;
                    img_star_2.sprite = spr_star_full;
                    img_star_3.sprite = spr_star_full;
                    break;
            }
        }
    }
}
