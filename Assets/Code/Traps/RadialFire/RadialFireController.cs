using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialFireController : MonoBehaviour
{
    [Header("General")]
    public Transform mesh;
    public float move_speed;
    public ParticleSystem vfx;
    public GameObject plane;
    public float shoot_distance = 1.6f;

    [Header("Restart")]
    public bool start_attack = false;
    public Vector3 start_fire_rotate;


    public void Start()
    {
        plane.transform.localPosition = new Vector3(0, 0.09f, shoot_distance);
    }

    public void StartAttack()
    {
        start_attack = true;
        vfx.Play();
    }

    public void EndAttack()
    {
        start_attack = false;
        vfx.Stop();
        mesh.transform.eulerAngles = start_fire_rotate;
    }

    public void FixedUpdate()
    {
        if (start_attack)
            mesh.Rotate(0, move_speed * Time.deltaTime, 0);
    }
}
