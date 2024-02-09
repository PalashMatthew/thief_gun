using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SawController : MonoBehaviour
{
    [Header("General")]
    public GameObject saw_obj;
    public float speed;
    public float rotate_speed;
    public float damage;

    private int curr_target;

    [Header("Navigation")]
    public Transform target_1;
    public Transform target_2;

    [Header("Restart")]
    public Vector3 start_saw_pos;
    public bool start_attack = false;

    [Header("Other")]
    public HPMinus damage_script;


    public void Start()
    {
        curr_target = 1;
        damage_script.dmg = damage;

        
    }
    public void StartAttack()
    {
        curr_target = 1;
        SawMove();
        start_attack = true;
    }

    public void EndAttack()
    {
        saw_obj.transform.DOKill();
        start_attack = false;
        saw_obj.transform.localPosition = start_saw_pos;
    }

    public void Update()
    {
        if (start_attack)
            saw_obj.transform.Rotate(Vector3.up * rotate_speed * Time.deltaTime);
    }

    void SawMove()
    {
        float _speed;
        _speed = Vector3.Distance(target_1.position, target_2.position) * speed;

        if (curr_target == 1)
        {
            saw_obj.transform.DOMove(target_2.position, _speed).SetEase(Ease.Linear).OnComplete(SawMove);
            curr_target = 2;

            return;
        }

        if (curr_target == 2)
        {
            saw_obj.transform.DOMove(target_1.position, _speed).SetEase(Ease.Linear).OnComplete(SawMove);
            curr_target = 1;

            return;
        }
    }
}
