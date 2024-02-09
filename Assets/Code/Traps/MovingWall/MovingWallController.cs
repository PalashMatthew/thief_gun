using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingWallController : MonoBehaviour
{
    [Header("General")]
    public GameObject wall_obj;
    public float speed;

    private int curr_target, next_target;

    [Header("Navigation")]
    public List<Transform> targets;

    [Header("Restart")]
    public bool start_attack = false;

    public void StartAttack()
    {
        start_attack = true;
        curr_target = 0;
        next_target = targets.Count;

        WallMove();
    }

    public void EndAttack()
    {
        start_attack = false;

        wall_obj.transform.DOKill();
        wall_obj.transform.position = targets[0].position;
    }

    void WallMove()
    {
        if (curr_target + 1 >= targets.Count)
            curr_target = 0;
        else curr_target += 1;

        if (next_target + 1 >= targets.Count)
            next_target = 0;
        else next_target += 1;

        float _speed;
        _speed = Vector3.Distance(targets[next_target].position, targets[curr_target].position) * speed;

        wall_obj.transform.DOMove(targets[curr_target].position, _speed).SetEase(Ease.Linear).OnComplete(WallMove);
    }
}
