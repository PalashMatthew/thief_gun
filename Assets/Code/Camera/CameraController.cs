using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    public float min_z;
    Vector3 smoothedPosition;
    Vector3 desiredPosition;

    public bool player_active;

    public bool follow;

    public float speed_back;


    void Awake()
    {
        target = GameObject.Find("Player").transform;

        desiredPosition = target.position + offset;

        transform.position = desiredPosition;

        smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;

        player_active = true;
        follow = true;
    }

    public void FixedUpdate()
    {
        if (follow)
        {
            desiredPosition = target.position + offset;
            //smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.fixedDeltaTime);
            smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }

    public void BackSpeed()
    {
        StartCoroutine(ReturnTimeScale());
    }

    IEnumerator ReturnTimeScale()
    {
        while (smoothSpeed < 18)
        {
            smoothSpeed += Time.unscaledDeltaTime * speed_back;

            if (smoothSpeed > 18)
            {
                smoothSpeed = 18;
            }
            yield return null;
        }
    }

    public void FastFollow()
    {
        desiredPosition = target.position + offset;

        transform.position = desiredPosition;

        smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;
        follow = true;
    }

    public void EndLevel()
    {
        transform.GetComponent<Camera>().DOFieldOfView(10, 0.75f).SetEase(Ease.OutBack);
        offset = new Vector3(offset.x, offset.y + 0.7f, offset.z);
    }
}
