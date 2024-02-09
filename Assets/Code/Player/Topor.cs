using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Topor : MonoBehaviour
{
    public float rotateSpeed;

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotateSpeed * Time.deltaTime));        
    }

    private void FixedUpdate()
    {
        Vector3 pos = GameObject.Find("Player").transform.position;
        transform.position = new Vector3(pos.x, 0.1547f, pos.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy")
        {
            other.GetComponent<EnemyHealth>().Hit();
        }
    }
}
