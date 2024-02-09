using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [Header("General")]
    public GameObject vfx_boom;

    [HideInInspector]
    public float speed;



    public void FixedUpdate()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "wall")
        {
            GameObject _vfx = Instantiate(vfx_boom, collision.contacts[0].point, transform.rotation);
            Destroy(_vfx, 2);
            Destroy(gameObject);
        }
        
    }
}
