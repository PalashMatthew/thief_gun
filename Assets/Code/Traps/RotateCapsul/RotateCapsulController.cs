using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCapsulController : MonoBehaviour
{
    [Header("General")]
    public GameObject capsul_obj;
    public float rotate_speed;

    [Header("Restart")]
    public bool start_attack = false;


    public void StartAttack()
    {
        start_attack = true;
    }

    public void EndAttack()
    {
        start_attack = false;
    }

    public void Update()
    {
        if (start_attack)
            capsul_obj.transform.Rotate(Vector3.up * rotate_speed * Time.deltaTime);
    }    
}
