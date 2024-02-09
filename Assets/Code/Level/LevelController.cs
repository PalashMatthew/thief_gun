using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LevelController : MonoBehaviour
{
    [Header("General")]
    public int level_num;
    public int level_money_reward;
    private GameObject player;
    public Vector3 player_pos;
    public Vector3 player_rot;
    public int lose_count;

    public bool start_run;
    public bool run_access;

    [Header("UI")]
    public Image img_lose_panel;
    public GameObject but_back;
    public GameObject but_view;
    public GameObject start_panel;
    public GameObject joy_canvas;
    public GameObject fake_joy;
    public GameObject fake_joy_handle;

    [Header("Chest")]
    public GameObject obj_chest;
    public List<GameObject> obj_keys;

    [Header("Traps")]
    public List<GameObject> traps;

    public bool level_tutor;

    public GameObject tutor_handle;


    public void Awake()
    {
        if (!PlayerPrefs.HasKey("rebound"))
        {
            PlayerPrefs.SetString("rebound", "all_rebound");
        }

        if (!PlayerPrefs.HasKey("slow"))
        {
            PlayerPrefs.SetString("slow", "all_slow");
        }
    }

    public void Start()
    {
        run_access = false;
        start_run = false;
        lose_count = 0;
        img_lose_panel.gameObject.SetActive(false);
        player = GameObject.Find("Player");
        but_back.SetActive(true);

        GameplayController._playerIsStopped = true;

        if (Application.loadedLevelName == "F_Level_2")
        {
            if (PlayerPrefs.GetString("map_tutorial") != "true")
            {
                GameObject.Find("Tutorial").GetComponent<TutorialController>().ShowTutor();
            }
        }

        //if (Application.loadedLevelName == "F_Level_9")
        //{
        //    if (PlayerPrefs.GetString("trap_tutorial") != "true")
        //    {
        //        GameObject.Find("Tutorial").GetComponent<TutorialController>().ShowTutor();
        //    }
        //}

        if (!level_tutor)
        {
            fake_joy.GetComponent<Animator>().SetTrigger("move");
        }

        fake_joy.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        fake_joy_handle.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        //tutor_handle.SetActive(true);
        //fake_joy.GetComponent<Animator>().SetTrigger("tutor_on");

        //fake_joy.transform.localPosition = new Vector2(0, -190);
    }

    public void Win()
    {
        foreach (GameObject _trap in traps)
        {
            _trap.GetComponent<TrapRestart>().RestartTrap();
        }
    }

    public void Lose()
    {
        img_lose_panel.gameObject.SetActive(true);
        img_lose_panel.color = new Vector4(0, 0, 0, 0);

        GameObject.Find("Player").GetComponent<PlayerDash>().rebound = true;

        but_view.GetComponent<Button>().interactable = true;

        StartCoroutine(LoseEnum());
    }

    IEnumerator LoseEnum()
    {
        yield return new WaitForSeconds(0);
        img_lose_panel.color = new Vector4(0, 0, 0, img_lose_panel.color.a + 0.03f);

        if (img_lose_panel.color.a < 1)
        {
            StartCoroutine(LoseEnum());
        }
        else
        {
            player.transform.position = player_pos;
            player.transform.eulerAngles = player_rot;
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            GameObject.Find("PlayerMesh").transform.position = player_pos;
            GameObject.Find("PlayerMesh").transform.eulerAngles = player_rot;
            lose_count += 1;
            GameplayController._playerIsStopped = true;

            

            obj_chest.GetComponent<ChestController>().ResetChest();
            

            foreach (GameObject _key in obj_keys)
            {
                _key.SetActive(true);
                _key.GetComponent<KeyController>().ResetKey();
            }

            foreach (GameObject _trap in traps)
            {
                _trap.GetComponent<TrapRestart>().RestartTrap();
            }

            start_run = false;

            GameObject.Find("playerMesh").GetComponent<Animator>().SetTrigger("restart");
            yield return new WaitForSeconds(0.3f);
            player.GetComponent<PlayerDash>().RestartPlayer();
            //but_view.transform.localPosition = new Vector3(831.5f, -432.8f, 0);
            //but_back.transform.localPosition = new Vector3(-864f, -417f, 0);

            but_back.GetComponent<RectTransform>().DOAnchorPos(new Vector2(96, 123), 0);
            but_back.GetComponent<RectTransform>().DOAnchorMin(new Vector2(0, 0), 0);
            but_back.GetComponent<RectTransform>().DOAnchorMax(new Vector2(0, 0), 0);

            but_view.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-128.5f, 107.2f), 0);
            but_view.GetComponent<RectTransform>().DOAnchorMin(new Vector2(1, 0), 0);
            but_view.GetComponent<RectTransform>().DOAnchorMax(new Vector2(1, 0), 0);

            

            start_panel.SetActive(true);
            run_access = false;
            img_lose_panel.gameObject.SetActive(false);
            joy_canvas.SetActive(true);
            //GameplayController._playerIsStopped = false;

            fake_joy.GetComponent<Animator>().SetTrigger("move");
            fake_joy.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            fake_joy_handle.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            //fake_joy.transform.localPosition = new Vector2(0, -190);
        }
    }

    public void StartRun()
    {
        if (!start_run)
        {
            ActivateTraps();

            //but_back.SetActive(false);

            start_run = true;

            fake_joy.GetComponent<Animator>().SetTrigger("idle");
            fake_joy.GetComponent<Image>().color = new Color(1, 1, 1, 0.4f);
            fake_joy_handle.GetComponent<Image>().color = new Color(1, 1, 1, 0.4f);
            //fake_joy.GetComponent<Animator>().SetTrigger("tutor_off");

            //fake_joy.transform.localPosition = new Vector2(544, -181);
        }
    }

    public void ActivateTraps()
    {
        if (traps != null)
        {
            foreach (GameObject _trap in traps)
            {
                _trap.GetComponent<TrapRestart>().StartTrap();
            }
        }
    }

    public void RestartTraps()
    {
        foreach (GameObject _trap in traps)
        {
            _trap.GetComponent<TrapRestart>().RestartTrap();
        }
    }
}
