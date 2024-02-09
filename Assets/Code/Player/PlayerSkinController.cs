using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkinController : MonoBehaviour
{
    public List<GameObject> mass_skins;

    public void Start()
    {
        ChangeSkin();
    }

    void ChangeSkin()
    {
        foreach (GameObject _skin in mass_skins)
        {
            _skin.SetActive(false);
        }

        int active_skin = PlayerPrefs.GetInt("active_skin");

        mass_skins[active_skin].SetActive(true);
    }
}
