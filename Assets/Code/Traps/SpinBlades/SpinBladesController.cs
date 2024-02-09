using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinBladesController : MonoBehaviour
{
    [Header("General")]
    public Transform mesh;
    public float move_speed;

    [Header("Restart")]
    public bool start_attack = false;
    public Vector3 start_rotate;

    public void Start()
    {
        mesh.transform.eulerAngles = start_rotate;
    }

    public void StartAttack()
    {
        start_attack = true;
    }

    public void EndAttack()
    {
        start_attack = false;
        mesh.transform.eulerAngles = start_rotate;
    }

    public void FixedUpdate()
    {
        if (start_attack)
            mesh.Rotate(0, move_speed * Time.deltaTime, 0);
    }
}
