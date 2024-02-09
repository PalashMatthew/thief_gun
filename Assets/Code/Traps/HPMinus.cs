using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPMinus : MonoBehaviour
{
    [HideInInspector]
    public float dmg;

    public bool isCollider;
    public bool isParticle;

    public void Start()
    {
        dmg = 1;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isCollider)
        {
            if (other.tag == "Player")
            {
                other.gameObject.GetComponent<PlayerController>().Hit(dmg);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isCollider)
        {
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<PlayerController>().Hit(dmg);
            }
        }
    }

    public void OnParticleCollision(GameObject other)
    {
        if (isParticle)
        {
            if (other.tag == "Player")
            {
                other.gameObject.GetComponent<PlayerController>().Hit(dmg);
            }
        }
    }
}
