using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingCapsulController : MonoBehaviour
{
    [Header("General")]
    public GameObject capsul_obj;
    public float speed;
    public float rotate_speed;
    public float damage;

    private int curr_target;

    [Header("Navigation")]
    public Transform target_1;
    public Transform target_2;

    [Header("Restart")]
    public Vector3 start_capsul_pos;
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
        CapsulMove();
        start_attack = true;
    }

    public void EndAttack()
    {
        capsul_obj.transform.DOKill();
        start_attack = false;
        capsul_obj.transform.localPosition = start_capsul_pos;
    }

    public void Update()
    {
        if (start_attack)
            capsul_obj.transform.Rotate(Vector3.up * rotate_speed * Time.deltaTime);
    }

    void CapsulMove()
    {
        float _speed;
        _speed = Vector3.Distance(target_1.position, target_2.position) * speed;

        if (curr_target == 1)
        {
            capsul_obj.transform.DOMove(target_2.position, _speed).SetEase(Ease.Linear).OnComplete(CapsulMove);
            curr_target = 2;

            return;
        }

        if (curr_target == 2)
        {
            capsul_obj.transform.DOMove(target_1.position, _speed).SetEase(Ease.Linear).OnComplete(CapsulMove);
            curr_target = 1;

            return;
        }
    }
}
