using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    [Header("General")]
    public GameObject vfx_boom;

    public float speed;

    public void FixedUpdate()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            collision.gameObject.GetComponent<EnemyHealth>().Hit();

            GameObject _vfx = Instantiate(vfx_boom, collision.contacts[0].point, transform.rotation);
            Destroy(_vfx, 2);
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "wall")
        {
            GameObject _vfx = Instantiate(vfx_boom, collision.contacts[0].point, transform.rotation);
            Destroy(_vfx, 2);
            Destroy(gameObject);
        }
        
    }
}
