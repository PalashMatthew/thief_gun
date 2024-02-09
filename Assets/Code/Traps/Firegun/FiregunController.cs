using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FiregunController : MonoBehaviour
{
    [Header("General")]
    public GameObject firegun;
    public float damage;
    public float pause_speed;
    public float attack_speed;

    [Header("Object")]
    public ParticleSystem vfx_fire;
    public List<ParticleSystem> all_vfx;

    [Header("Restart")]
    public bool start_attack = false;

    [Header("UI")]
    public Image img_progress_bar;
    private float curr_shot_time;
    private float ui_time;

    [Header("Other")]
    public HPMinus damage_script;


    public void Start()
    {
        damage_script.dmg = damage;
        var vfx_main = vfx_fire.main;
        vfx_main.duration = attack_speed;

        foreach (ParticleSystem part in all_vfx)
        {
            var vfx_main1 = part.main;
            vfx_main1.duration = attack_speed;
        }

        img_progress_bar.fillAmount = 0;
    }

    public void StartAttack()
    {
        img_progress_bar.fillAmount = 0;

        StartCoroutine(AttackEnum());
    }

    public void EndAttack()
    {
        StopAllCoroutines();
        start_attack = false;
        vfx_fire.Stop();
        img_progress_bar.fillAmount = 0;
    }

    public void Update()
    {
        if (start_attack)
        {
            curr_shot_time += Time.deltaTime;
            img_progress_bar.fillAmount = curr_shot_time / ui_time;
        }
    }

    IEnumerator AttackEnum()
    {
        start_attack = true;
        curr_shot_time = 0;
        ui_time = pause_speed;
        yield return new WaitForSeconds(pause_speed);

        vfx_fire.Play();
        curr_shot_time = 0;
        ui_time = attack_speed;
        yield return new WaitForSeconds(attack_speed);

        StartCoroutine(AttackEnum());
    }
}
