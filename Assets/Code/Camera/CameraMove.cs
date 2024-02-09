using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float min_x, max_x;
    public float min_z, max_z;

    private Vector3 newPosition;

    private Vector3 dragStartPosition;
    private Vector3 dragCurrentPosition;

    protected Plane plane;

    public float zoomOutMin;
    public float zoonOutMax;
    public float zoom_speed;

    bool scroll_accept;

    public bool use_accept = false;


    public void Start()
    {
        scroll_accept = true;
        newPosition = transform.position;
    }

    public void Update()
    {
        if (use_accept)
        {
            //if (Input.touchCount == 1 && scroll_accept)
                //HandleMouseInput();

            if (scroll_accept)
                HandleMouseInput();

            //Zoom();
            Bounds();

            if (Input.GetMouseButtonUp(0))
            {
                scroll_accept = true;
            }
        }
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if (plane.Raycast(ray, out entry))
            {
                dragStartPosition = ray.GetPoint(entry);
            }
        }

        if (Input.GetMouseButton(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if (plane.Raycast(ray, out entry))
            {
                dragCurrentPosition = ray.GetPoint(entry);

                newPosition = transform.position + dragStartPosition - dragCurrentPosition;
                transform.position = newPosition;
            }
        }
    }

    void Zoom()
    {
        if (Input.touchCount >= 2)
        {
            scroll_accept = false;

            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView - (difference * zoom_speed), zoomOutMin, zoonOutMax);
        }
    }

    void Bounds()
    {
        if (Camera.main.transform.position.x < min_x)
            Camera.main.transform.position = new Vector3(min_x, Camera.main.transform.position.y, Camera.main.transform.position.z);

        if (Camera.main.transform.position.x > max_x)
            Camera.main.transform.position = new Vector3(max_x, Camera.main.transform.position.y, Camera.main.transform.position.z);

        if (Camera.main.transform.position.z < min_z)
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, min_z);

        if (Camera.main.transform.position.z > max_z)
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, max_z);
    }

    protected Vector3 PlanePosition(Vector2 screenPos)
    {
        var rayNow = Camera.main.ScreenPointToRay(screenPos);
        if (plane.Raycast(rayNow, out var enterNow))
            return rayNow.GetPoint(enterNow);

        return Vector3.zero;
    }

    protected Vector3 PlanePositionDelta(Touch touch)
    {
        if (touch.phase != TouchPhase.Moved)
            return Vector3.zero;

        var rayBefore = Camera.main.ScreenPointToRay(touch.position - touch.deltaPosition);
        var rayNow = Camera.main.ScreenPointToRay(touch.position);
        if (plane.Raycast(rayBefore, out var enterBefore) && plane.Raycast(rayNow, out var enterNow))
            return rayBefore.GetPoint(enterBefore) - rayNow.GetPoint(enterNow);

        return Vector3.zero;
    }
}
