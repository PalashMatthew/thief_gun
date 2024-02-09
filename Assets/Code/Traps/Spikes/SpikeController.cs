using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SpikeController : MonoBehaviour
{
    [Header("General")]
    public float attack_time;
    public float pause_time;
    public Animator anim;
    public GameObject collider;

    [Header("UI")]
    public bool start_attack = false;


    public void Start()
    {
        collider.SetActive(false);
    }

    public void StartAttack()
    {
        StartCoroutine(AttackEnum());
    }

    public void EndAttack()
    {
        StopAllCoroutines();
        start_attack = false;
        collider.SetActive(false);
        anim.SetTrigger("reset");
    }

    IEnumerator AttackEnum()
    {
        start_attack = true;

        yield return new WaitForSeconds(pause_time);

        anim.SetTrigger("start");

        yield return new WaitForSeconds(1.5f);

        collider.SetActive(true);

        yield return new WaitForSeconds(attack_time);

        anim.SetTrigger("end");
        collider.SetActive(false);
        yield return new WaitForSeconds(2f);        

        StartCoroutine(AttackEnum());
    }
}
