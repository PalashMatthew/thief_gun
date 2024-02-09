using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy1Controller : MonoBehaviour
{
    public float distanceVisible;

    public bool isPlayerVisible;
    NavMeshAgent agent;

    public bool isStartMove;

    public float moveSpeed;

    public Vector3 startCoord;

    public LayerMask layer;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        isStartMove = false;

        startCoord = transform.position;
    }

    private void Update()
    {
        CheckPlayer();

        if (isStartMove)
        {
            agent.SetDestination(GameObject.Find("Player").transform.position);
        }
        else if (transform.position != startCoord)
        {
            agent.SetDestination(startCoord);
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

                if (!isStartMove)
                {
                    isStartMove=true;
                    StartCoroutine(StopMove());
                }
            }
            else
            {
                isPlayerVisible = false;
            }
        }
        else
        {
            isPlayerVisible = false;
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
}
