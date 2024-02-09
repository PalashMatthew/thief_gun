using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy2Controller : MonoBehaviour
{
    public float distanceVisible;

    public bool isPlayerVisible;
    NavMeshAgent agent;

    public bool isStartMove;

    public float moveSpeed;

    public Vector3 startCoord;

    public LayerMask layer;

    public float shoot_speed;

    public bool start_attack = false;

    public GameObject bullet_obj;

    public Transform bullet_spawn_pos;

    public float bulletSpeed;

    bool isPlayerFind = false;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        isStartMove = false;

        StartCoroutine(Shoting());

        startCoord = transform.position;
    }

    private void Update()
    {
        CheckPlayer();

        if (isPlayerFind)
        {
            agent.SetDestination(GameObject.Find("Player").transform.position);
        }
    }

    void CheckPlayer()
    {
        RaycastHit hit;

        Vector3 playerPos = new Vector3(GameObject.Find("Player").transform.position.x, GameObject.Find("Player").transform.position.y + 0.2f, GameObject.Find("Player").transform.position.z);

        Vector3 dir = playerPos - transform.position;
        //dir = new Vector3(dir.x, 1, dir.z);
        dir = dir.normalized;

        if (Physics.Raycast(transform.position, dir, out hit, distanceVisible, layer))
        {
            Debug.DrawRay(transform.position, dir * hit.distance, Color.yellow);

            if (hit.collider.tag == "Player")
            {
                isPlayerVisible = true;

                isPlayerFind = true;

            }
        }
    }

    IEnumerator StopMove()
    {
        yield return new WaitForSeconds(moveSpeed);

        if (isPlayerVisible)
        {
            StartCoroutine(StopMove());
        }
        else
        {
            isStartMove = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerHealth.playerHp -= 1;
        }
    }

    IEnumerator Shoting()
    {
        yield return new WaitForSeconds(shoot_speed);

        if (isPlayerFind)
            Shot();

        StartCoroutine(Shoting());
    }

    void Shot()
    {
        GameObject _bullet = Instantiate(bullet_obj, bullet_spawn_pos.position, transform.rotation);

        _bullet.transform.LookAt(GameObject.Find("Player").transform.position);
        _bullet.transform.eulerAngles = new Vector3(0, _bullet.transform.eulerAngles.y, 0);

        _bullet.GetComponent<BulletController>().speed = bulletSpeed;
    }
}
