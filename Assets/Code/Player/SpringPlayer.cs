using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringPlayer : MonoBehaviour
{
    public bool scale_spring = false;

    public Transform _playerTransform;
    public Transform _playerBody;

    public float _rotationKoefficient;

    Vector3 smoothedPosition;
    Vector3 desiredPosition;
    public float smoothSpeed = 0.125f;

    public void Update()
    {
        Vector3 relativePosition = _playerTransform.InverseTransformPoint(transform.position);

        _playerBody.localEulerAngles = new Vector3(relativePosition.z, 0, -relativePosition.x) * _rotationKoefficient;

        //Transform
        desiredPosition = GameObject.Find("FollowPlayer").transform.position;
        smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;

        //Rotation
        Vector3 targetDirection = GameObject.Find("FollowPlayer").transform.position - transform.position;

        float singleStep = 0.05f;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);

        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }
}
