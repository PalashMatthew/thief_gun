using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    Vector3 smoothedPosition;
    Vector3 desiredPosition;
    public float smoothSpeed = 0.125f;

    public void Update()
    {
        transform.position = GameObject.Find("Player").transform.position;

        //desiredPosition = GameObject.Find("Player").transform.position;
        //smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        //transform.position = smoothedPosition;

        transform.rotation = GameObject.Find("Player").transform.rotation;
    }
}
