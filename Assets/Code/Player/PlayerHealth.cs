using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static float playerHp;
    public float playerMaxHp;
    public TMP_Text tHp;


    private void Start()
    {
        playerHp = playerMaxHp;
    }

    private void Update()
    {
        if (playerHp <= 0)
        {
            Application.LoadLevel(Application.loadedLevel);
        }

        tHp.text = "HP = " + playerHp + "/" + playerMaxHp;
    }
}
