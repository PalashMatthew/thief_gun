using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class LevelUIController : MonoBehaviour
{
    [Header("HP Bar")]
    public Image hp_bar_fill;
    public TMP_Text t_hp;
    private float curr_hp;
    private float max_hp;
    public Image img_star_2, img_star_3;

    [Header("Key Bar")]
    public TMP_Text t_key;
    private int curr_key;
    private int max_key;

    [Header("Start UI")]
    public GameObject panel_start_ui;
    public GameObject but_back;
    public Button but_view;
    public int but_view_id;
    public Sprite spr_but_view_1, spr_but_view_2;
    public GameObject backCanvas;

    [Header("Win UI")]
    public GameObject win_canvas;
    public Image img_win_star_1, img_win_star_2, img_win_star_3;
    public TMP_Text t_win_money;
    private int stars_count;

    [Header("View UI")]
    public GameObject joy_canvas;
    public GameObject fake_joy;
    private Vector3 camera_curr_pos, camera_curr_rot;
    public Vector3 camera_new_pos, camera_new_rot;
    private bool but_view_access = true;

    [Header("Particles")]
    public GameObject vfx_win;

    public bool isTutorialLevel;


    public void Start()
    {
        backCanvas.SetActive(true);

        but_view_id = 1;
        but_view.image.sprite = spr_but_view_1;

        but_view.interactable = true;

        max_hp = GameObject.Find("Player").GetComponent<PlayerController>().max_hp;

        win_canvas.SetActive(false);
        panel_start_ui.SetActive(true);

        camera_curr_pos = Camera.main.transform.position;
        camera_curr_rot = Camera.main.transform.eulerAngles;
    }

    public void Update()
    {
        ////HP Bar
        //curr_hp = GameObject.Find("Player").GetComponent<PlayerController>().curr_hp;
        //hp_bar_fill.fillAmount = curr_hp / max_hp;

        ////Key Bar
        //curr_key = GameObject.Find("Chest").GetComponent<ChestController>().curr_key_count;
        //max_key = GameObject.Find("Chest").GetComponent<ChestController>().key_need;
        //t_key.text = curr_key + "/" + max_key;

        //StarsView();
    }

    void StarsView()
    {
        if (curr_hp > 9)
        {
            img_star_2.color = new Vector4(1, 1, 1, 1);
            img_star_3.color = new Vector4(1, 1, 1, 1);
        }

        if (curr_hp > 5 && curr_hp <= 9)
        {
            img_star_2.color = new Vector4(1, 1, 1, 1);
            img_star_3.color = new Vector4(1, 1, 1, 0.17f);
        }

        if (curr_hp <= 5)
        {
            img_star_2.color = new Vector4(1, 1, 1, 0.17f);
            img_star_3.color = new Vector4(1, 1, 1, 0.17f);
        }
    }    

    public void Win()
    {
        win_canvas.SetActive(true);

        if (curr_hp > 9)
        {
            img_win_star_2.color = new Vector4(1, 1, 1, 1);
            img_win_star_3.color = new Vector4(1, 1, 1, 1);
            stars_count = 3;
        }

        if (curr_hp > 5 && curr_hp <= 9)
        {
            img_win_star_2.color = new Vector4(1, 1, 1, 1);
            img_win_star_3.color = new Vector4(1, 1, 1, 0.17f);
            stars_count = 2;
        }

        if (curr_hp <= 5)
        {
            img_win_star_2.color = new Vector4(1, 1, 1, 0.17f);
            img_win_star_3.color = new Vector4(1, 1, 1, 0.17f);
            stars_count = 1;
        }

        if (isTutorialLevel)
        {
            GameObject.Find("GameController").GetComponent<LevelController>().level_money_reward = 0;
        } 
        else
        {
            if (PlayerPrefs.GetString("level_" + GameObject.Find("GameController").GetComponent<LevelController>().level_num + "_success") != "true")
            {
                GameObject.Find("GameController").GetComponent<LevelController>().level_money_reward = stars_count * 100;
            }
            else if (stars_count > PlayerPrefs.GetInt("level_" + GameObject.Find("GameController").GetComponent<LevelController>().level_num + "_stars_count"))
            {
                GameObject.Find("GameController").GetComponent<LevelController>().level_money_reward = (stars_count - PlayerPrefs.GetInt("level_" + GameObject.Find("GameController").GetComponent<LevelController>().level_num + "_stars_count")) * 100;
            }
            else if (stars_count == PlayerPrefs.GetInt("level_" + GameObject.Find("GameController").GetComponent<LevelController>().level_num + "_stars_count"))
            {
                GameObject.Find("GameController").GetComponent<LevelController>().level_money_reward = 0;
            }
        }

        t_win_money.text = "" + GameObject.Find("GameController").GetComponent<LevelController>().level_money_reward;

        if (!PlayerPrefs.HasKey("level_" + GameObject.Find("GameController").GetComponent<LevelController>().level_num + "_stars_count"))
        {
            PlayerPrefs.SetInt("level_" + GameObject.Find("GameController").GetComponent<LevelController>().level_num + "_stars_count", stars_count);
        }
        else if (stars_count > PlayerPrefs.GetInt("level_" + GameObject.Find("GameController").GetComponent<LevelController>().level_num + "_stars_count"))
        {
            PlayerPrefs.SetInt("level_" + GameObject.Find("GameController").GetComponent<LevelController>().level_num + "_stars_count", stars_count);
        }        

        StartCoroutine(VFX_Win());
    }

    IEnumerator VFX_Win()
    {
        GameObject _vfx = Instantiate(vfx_win, new Vector3(Random.Range(-800, 800), Random.Range(-116, 431), -142), transform.rotation);
        _vfx.transform.parent = win_canvas.transform;
        _vfx.transform.localPosition = new Vector3(Random.Range(-800, 800), Random.Range(-116, 431), -142);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(VFX_Win());
    }

    public void But_Claim()
    {
        PlayerPrefs.SetInt("player_money", PlayerPrefs.GetInt("player_money") + GameObject.Find("GameController").GetComponent<LevelController>().level_money_reward);

        PlayerPrefs.SetString("level_" + GameObject.Find("GameController").GetComponent<LevelController>().level_num + "_success", "true");
        PlayerPrefs.SetString("level_" + (GameObject.Find("GameController").GetComponent<LevelController>().level_num + 1) + "_unlock", "true");

        SceneTransition.SwithToScene("LevelSelect");
    }

    public void But_ClaimTutorial()
    {
        PlayerPrefs.SetInt("level_" + GameObject.Find("GameController").GetComponent<LevelController>().level_num + "_stars_count", stars_count);
        PlayerPrefs.SetInt("player_money", PlayerPrefs.GetInt("player_money") + GameObject.Find("GameController").GetComponent<LevelController>().level_money_reward);
    }

    public void But_Back()
    {
        
        SceneTransition.SwithToScene("LevelSelect");
    }

    public void StartGame()
    {
        if (!TutorialController.isTutorialActive)
        {
            GameplayController._playerIsStopped = false;
            panel_start_ui.SetActive(false);
            StartCoroutine(StartRunAccess());
            //but_back.SetActive(false);
            //but_view.gameObject.SetActive(false);
            GameObject.Find("GameController").GetComponent<LevelController>().run_access = true;

            //but_view.GetComponent<RectTransform>().DOLocalMoveX(1201f, 5 * Time.unscaledDeltaTime).SetEase(Ease.InBack);
            //but_back.GetComponent<RectTransform>().DOLocalMoveX(-1199f, 5 * Time.unscaledDeltaTime).SetEase(Ease.InBack);

            but_view.interactable = false;

            but_back.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-485, 123), 5 * Time.unscaledDeltaTime).SetEase(Ease.InBack);
            but_back.GetComponent<RectTransform>().DOAnchorMin(new Vector2(0, 0), 5 * Time.unscaledDeltaTime).SetEase(Ease.InBack);
            but_back.GetComponent<RectTransform>().DOAnchorMax(new Vector2(0, 0), 5 * Time.unscaledDeltaTime).SetEase(Ease.InBack);

            but_view.GetComponent<RectTransform>().DOAnchorPos(new Vector2(528f, 107.2f), 5 * Time.unscaledDeltaTime).SetEase(Ease.InBack);
            but_view.GetComponent<RectTransform>().DOAnchorMin(new Vector2(1, 0), 5 * Time.unscaledDeltaTime).SetEase(Ease.InBack);
            but_view.GetComponent<RectTransform>().DOAnchorMax(new Vector2(1, 0), 5 * Time.unscaledDeltaTime).SetEase(Ease.InBack);
        }
    }

    IEnumerator StartRunAccess()
    {
        yield return new WaitForSeconds(0.05f);
        GameObject.Find("GameController").GetComponent<LevelController>().run_access = true;
    }

    public void But_View()
    {
        if (but_view_access)
        {
            if (but_view_id == 2)
            {
                but_view.image.sprite = spr_but_view_1;
                but_view_id = 1;
                EndView();
                StartCoroutine(EndUseAcceptMainCamera());
                Camera.main.GetComponent<CameraMove>().use_accept = false;
                fake_joy.SetActive(true);

                GameObject.Find("GameController").GetComponent<LevelController>().RestartTraps();
                GameObject.Find("Player").GetComponent<PlayerController>().dont_hit = false;

                GameObject.Find("Player").GetComponent<Rigidbody>().isKinematic = false;
                but_view_access = false;
                return;
            }

            if (but_view_id == 1)
            {
                but_view.image.sprite = spr_but_view_2;
                but_view_id = 2;
                StartView();
                StartCoroutine(StartUseAcceptMainCamera());
                fake_joy.SetActive(false);

                GameObject.Find("GameController").GetComponent<LevelController>().ActivateTraps();
                GameObject.Find("Player").GetComponent<PlayerController>().dont_hit = true;

                GameObject.Find("Player").GetComponent<Rigidbody>().isKinematic = true;
                but_view_access = false;
                return;
            }
        }
    }

    public void StartView()
    {
        Camera.main.GetComponent<CameraController>().follow = false;
        panel_start_ui.SetActive(false);
        but_back.SetActive(false);
        joy_canvas.SetActive(false);
        Camera.main.transform.DOMove(camera_new_pos, 0.5f);
        Camera.main.transform.DORotate(camera_new_rot, 0.5f);
    }

    IEnumerator StartUseAcceptMainCamera()
    {
        yield return new WaitForSeconds(0.5f);
        Camera.main.GetComponent<CameraMove>().use_accept = true;
        but_view_access = true;
    }

    IEnumerator EndUseAcceptMainCamera()
    {
        yield return new WaitForSeconds(0.5f);
        but_view_access = true;
        fake_joy.GetComponent<Animator>().SetTrigger("move");
    }

    public void EndView()
    {
        Camera.main.GetComponent<CameraController>().follow = true;
        panel_start_ui.SetActive(true);
        but_back.SetActive(true);
        joy_canvas.SetActive(true);
        Camera.main.transform.DOMove(camera_curr_pos, 0.5f);
        Camera.main.transform.DORotate(camera_curr_rot, 0.5f);

        fake_joy.GetComponent<Animator>().SetTrigger("move");
    }
}
