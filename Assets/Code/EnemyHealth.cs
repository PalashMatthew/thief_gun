using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float hp;


    private void Update()
    {
        if (hp <= 0) Destroy(gameObject);
    }

    public void Hit()
    {
        hp -= 1;
    }
}
