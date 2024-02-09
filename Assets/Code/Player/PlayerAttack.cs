using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public List<GameObject> enemy;

    public float shotSpeed;
    public GameObject bulletObj;
    public float bulletSpeed;

    private void Start()
    {
        StartCoroutine(Shot());
    }


    IEnumerator Shot()
    {
        yield return new WaitForSeconds(shotSpeed);

        foreach (GameObject obj in enemy)
        {
            if (obj != null)
            {
                GameObject inst = Instantiate(bulletObj, transform.position, transform.rotation);
                inst.transform.LookAt(obj.transform.position);
                inst.transform.eulerAngles = new Vector3(0, inst.transform.eulerAngles.y, 0);
                inst.transform.position = new Vector3(inst.transform.position.x, 0.201f, inst.transform.position.z);

                StartCoroutine(Shot());
                yield break;
            }
        }

        StartCoroutine(Shot());
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy")
        {
            Debug.Log("ENEMY");

            enemy.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "enemy")
        {
            if (enemy.Contains(other.gameObject))
            {
                enemy.Remove(other.gameObject);
            }
        }
    }
}
