using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    [Header("General")]
    public List<GameObject> buttons_skin;

    private int active_skin;

    public GameObject panel_shop;


    public void Awake()
    {
        ActivateSkin();
    }

    void ActivateSkin()
    {
        if (!PlayerPrefs.HasKey("active_skin"))
        {
            active_skin = 0;
            PlayerPrefs.SetInt("active_skin", 0);

            PlayerPrefs.SetString("skin_" + 0 + "_bought", "yes");

            for (int i = 1; i < 10; i++)
            {
                PlayerPrefs.SetString("skin_" + i + "_bought", "non");
            }
        }
        else
        {
            active_skin = PlayerPrefs.GetInt("active_skin");
        }

        buttons_skin[active_skin].GetComponent<ShopSkin>().type = ShopSkin.Type.active;
    }

    public void But_Skin(int _num)
    {
        if (buttons_skin[_num].GetComponent<ShopSkin>().type == ShopSkin.Type.non_bought)
        {
            if (PlayerPrefs.GetInt("player_money") >= buttons_skin[_num].GetComponent<ShopSkin>().price)
            {
                PlayerPrefs.SetInt("player_money", PlayerPrefs.GetInt("player_money") - buttons_skin[_num].GetComponent<ShopSkin>().price);

                buttons_skin[_num].GetComponent<ShopSkin>().type = ShopSkin.Type.active;

                buttons_skin[active_skin].GetComponent<ShopSkin>().type = ShopSkin.Type.bought;

                buttons_skin[active_skin].GetComponent<ShopSkin>().ChangeType();
                buttons_skin[_num].GetComponent<ShopSkin>().ChangeType();

                active_skin = _num;

                PlayerPrefs.SetString("skin_" + _num + "_bought", "yes");
                PlayerPrefs.SetInt("active_skin", active_skin);
            }
        } 
        else if (buttons_skin[_num].GetComponent<ShopSkin>().type == ShopSkin.Type.bought)
        {
            buttons_skin[_num].GetComponent<ShopSkin>().type = ShopSkin.Type.active;

            buttons_skin[active_skin].GetComponent<ShopSkin>().type = ShopSkin.Type.bought;

            buttons_skin[active_skin].GetComponent<ShopSkin>().ChangeType();
            buttons_skin[_num].GetComponent<ShopSkin>().ChangeType();

            active_skin = _num;
            PlayerPrefs.SetInt("active_skin", active_skin);
        }        
    }

    public void But_Back()
    {
        //panel_shop.SetActive(false);
    }
}
