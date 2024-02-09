using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    [Header("General")]
    public int key_need;

    [Header("Other")]   
    public ParticleSystem vfx_money;
    public Animator anim;

    private bool isOpen = false;

    [HideInInspector]
    public int curr_key_count;


    public void Start()
    {
        if (key_need == 0)
        {
            anim.SetTrigger("jumping");
        }
    }

    public void ResetChest()
    {
        anim.SetTrigger("reset");
        //anim.ResetTrigger("open");
        curr_key_count = 0;

        Debug.Log("RESET CHEST");
    }

    public void KeyPickUp()
    {
        curr_key_count++;
        anim.ResetTrigger("reset");
        if (curr_key_count >= key_need)
        {
            
            anim.SetTrigger("jumping");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && curr_key_count == key_need && !isOpen && !GameplayController._playerIsStopped)
        {
            isOpen = true;
            OpenChest();
        }
    }

    void OpenChest()
    {
        GameObject.Find("Player").GetComponent<PlayerController>().aud.Play_Win();
        GameObject.Find("playerMesh").GetComponent<Animator>().SetTrigger("win");

        anim.SetTrigger("open");
        vfx_money.Play();

        GameplayController._playerIsStopped = true;

        if (GameObject.Find("Floating Joystick") != null)
            GameObject.Find("Floating Joystick").SetActive(false);

        GameObject.Find("Player").GetComponent<PlayerDash>().StoppedPlayer();
        Camera.main.GetComponent<CameraController>().EndLevel();

        GameObject.Find("GameUI").GetComponent<LevelUIController>().Win();
        GameObject.Find("GameController").GetComponent<LevelController>().Win();
    }
}
