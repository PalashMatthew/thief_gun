using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopSkin : MonoBehaviour
{    
    public enum Type
    {
        non_bought,
        bought,
        active
    }

    [Header("General")]
    public Type type;
    public int price;
    public GameObject img_money;
    public int skin_num;

    [Header("Text")]
    public GameObject obj_t_price;
    public GameObject obj_t_active;
    public TMP_Text t_price;
    public TMP_Text t_active;

    public void OnEnable()
    {
        CheckAccess();

        t_price.text = "" + price;
    }

    private void CheckAccess()
    {
        if (PlayerPrefs.GetString("skin_" + skin_num + "_bought") == "non")
        {
            type = Type.non_bought;
        } 
        else
        {
            if (PlayerPrefs.GetInt("active_skin") == skin_num)
            {
                type = Type.active;
            } 
            else
            {
                type = Type.bought;
            }
        }

        ChangeType();
    }

    public void ChangeType()
    {
        if (type == Type.active)
        {
            obj_t_price.SetActive(false);
            obj_t_active.SetActive(true);

            img_money.SetActive(false);

            if (PlayerPrefs.GetString("gameLanguage") == "ru")
                t_active.text = "Выбран";
            else
                t_active.text = "Active";
        }

        if (type == Type.bought)
        {
            obj_t_price.SetActive(false);
            obj_t_active.SetActive(true);

            img_money.SetActive(false);

            t_active.text = "";
        }

        if (type == Type.non_bought)
        {
            obj_t_price.SetActive(true);
            obj_t_active.SetActive(false);

            img_money.SetActive(true);

            t_price.text = "_price";
        }
    }
}
