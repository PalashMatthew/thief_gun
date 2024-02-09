using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float curr_hp;
    public float max_hp;

    public bool dont_hit = false;  //Пока true не получает урон
    public float dont_hit_timer;  //Время не получения урона

    public GameObject old_mesh;

    public List<GameObject> mass_skins;
    public List<SkinnedMeshRenderer> mass_body;
    public List<MeshRenderer> mass_helmet;

    public PlayerAudio aud;

    

    public enum slow_time
    {
        AllSlow,
        PlayerSlow
    }
    public slow_time _slow_time;

    public enum rebound
    {
        EveryTimeRebound,
        OneRebound
    }
    public rebound _rebound;


    public void Awake()
    {
        CheckSettings();
    }

    public void Start()
    {
        //ChangeSkin();
        old_mesh.SetActive(false);
        

        curr_hp = max_hp;
        //GameplayController._playerIsStopped = false;        
    }

    void CheckSettings()
    {
        if (PlayerPrefs.GetString("rebound") == "all_rebound")
            _rebound = rebound.EveryTimeRebound;
        else _rebound = rebound.OneRebound;

        if (PlayerPrefs.GetString("slow") == "all_slow")
            _slow_time = slow_time.AllSlow;
        else _slow_time = slow_time.PlayerSlow;

        //_rebound = rebound.EveryTimeRebound;
    }

    void ChangeSkin()
    {
        foreach (GameObject _skin in mass_skins)
        {
            _skin.SetActive(false);
        }

        int active_skin = PlayerPrefs.GetInt("active_skin");

        mass_skins[active_skin].SetActive(true);
        body = mass_body[active_skin];
    }

    public void StartChange()
    {
        gameObject.GetComponent<PlayerDash>().enabled = true;
    }

    public void But_Dont_HIT()
    {
        dont_hit = !dont_hit;
    }

    public void Hit(float _dmg)
    {
        //if (!dont_hit && !GameplayController._playerIsStopped)
        //{
        //    if (curr_hp > 2)
        //        curr_hp -= _dmg;

        //    aud.Play_Die();
        //    GameObject.Find("GameController").GetComponent<LevelController>().Lose();
        //    GameplayController._playerIsStopped = true;
        //    GameObject.Find("Player").GetComponent<PlayerDash>().StoppedPlayer();
        //    StartCoroutine(Dont_Hit());
        //    GameObject.Find("playerMesh").GetComponent<Animator>().ResetTrigger("start");
        //    GameObject.Find("playerMesh").GetComponent<Animator>().SetTrigger("die");
        //}

        if (!dont_hit && !GameplayController._playerIsStopped)
        {
            StartCoroutine(Dont_Hit());
            PlayerHealth.playerHp -= 1;
        }
            
    }

    IEnumerator Dont_Hit()
    {
        //StartCoroutine(HitEnum());
        dont_hit = true;
        yield return new WaitForSeconds(dont_hit_timer);
        dont_hit = false;
    }

    public Material hit_mat_1, hit_mat_2;
    public Material head_mat, body_mat;
    private SkinnedMeshRenderer body;

    public IEnumerator HitEnum()
    {
        //On 1
        body.material = hit_mat_1;

        yield return new WaitForSeconds(0.05f);

        //Off
        body.material = body_mat;

        yield return new WaitForSeconds(0.05f);

        //On 2
        body.material = hit_mat_2;

        yield return new WaitForSeconds(0.05f);

        body.material = body_mat;
    }
}
