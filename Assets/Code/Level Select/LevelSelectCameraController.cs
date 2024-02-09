using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectCameraController : MonoBehaviour
{
    private Vector3 newPosition;

    private Vector3 dragStartPosition;
    private Vector3 dragCurrentPosition;

    protected Plane plane;

    public float zoomOutMin;
    public float zoonOutMax;
    public float zoom_speed;

    bool scroll_accept;

    public bool use_accept = false;

    public float cam_speed;
    private float curr_timer;
    private int dir;


    public void Start()
    {
        scroll_accept = true;
        newPosition = transform.position;
    }

    public void Update()
    {
        if (use_accept)
        {

            if (scroll_accept)
                HandleMouseInput();

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
            StopAllCoroutines();

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
                //newPosition = new Vector3(transform.position.x, transform.position.y, newPosition.z);
                transform.position = new Vector3(transform.position.x, transform.position.y, newPosition.z);
            }           
        }
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
