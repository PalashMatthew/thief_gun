using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrajectory : MonoBehaviour
{
    public float max_distance;

    public GameObject reflection_obj;
    public Transform target_reflection;

    public LineRenderer line_1, line_2;
    public Transform line_2_pos, line_2_pos2;

    public LayerMask trajectory_layer;

    public Joystick joy;

    public void Start()
    {
        joy = GameObject.Find("Floating Joystick").GetComponent<Joystick>();
    }

    public void Update()
    {
        if (!GameplayController._playerIsStopped)
        {
            if (transform.GetComponent<PlayerController>()._rebound == PlayerController.rebound.EveryTimeRebound)
            {
                if (Input.GetMouseButton(0))
                {
                    if (joy.Horizontal != 0 && joy.Vertical != 0)
                        ReflectionView();
                    else ReflectionOff();
                }

                if (Input.GetMouseButtonUp(0))
                {
                    ReflectionOff();
                }
            }

            if (transform.GetComponent<PlayerController>()._rebound == PlayerController.rebound.OneRebound)
            {
                if (transform.GetComponent<PlayerDash>().rebound)
                {
                    if (Input.GetMouseButton(0))
                    {
                        if (joy.Horizontal != 0 || joy.Vertical != 0)
                            ReflectionView();
                        else ReflectionOff();
                    }

                    if (Input.GetMouseButtonUp(0))
                    {
                        ReflectionOff();
                    }
                }
            }
        }
        else
        {
            ReflectionOff();
        }        
    }

    void ReflectionView()
    {
        reflection_obj.SetActive(true);
        line_1.enabled = true;
        line_2.enabled = true;

        //Line 1
        RaycastHit hit;
        Vector3 newDirection = Vector3.zero;

        if (Physics.Raycast(target_reflection.position, transform.TransformDirection(Vector3.forward), out hit, max_distance, trajectory_layer))
        {
            reflection_obj.transform.position = hit.point;
            newDirection = Vector3.Reflect(transform.forward, hit.normal);

            line_1.SetPosition(0, new Vector3(transform.position.x, 0.1f, transform.position.z));
            line_1.SetPosition(1, new Vector3(hit.point.x, 0.1f, hit.point.z));
        } 
        else
        {
            reflection_obj.transform.localPosition = new Vector3(0, 0, max_distance + 1);
            newDirection = Vector3.Reflect(transform.forward, reflection_obj.transform.localPosition);

            line_1.SetPosition(0, new Vector3(transform.position.x, 0.1f, transform.position.z));
            line_1.SetPosition(1, new Vector3(reflection_obj.transform.position.x, 0.1f, reflection_obj.transform.position.z));
        }

        
        newDirection = new Vector3(
            -newDirection.x,
            0,
            -newDirection.z
        );
        reflection_obj.transform.rotation = Quaternion.LookRotation(newDirection);

        

        //Line 2
        RaycastHit hit2;
        Physics.Raycast(reflection_obj.transform.position, -reflection_obj.transform.forward, out hit2, 500, trajectory_layer);

        line_2_pos2.transform.position = hit2.point;
        if (hit2.distance <= 3)
        {
            line_2.SetPosition(0, new Vector3(hit.point.x, 0.1f, hit.point.z));
            line_2.SetPosition(1, new Vector3(line_2_pos2.position.x, 0.1f, line_2_pos2.position.z));
        } 
        else
        {
            line_2.SetPosition(0, new Vector3(hit.point.x, 0.1f, hit.point.z));
            line_2.SetPosition(1, new Vector3(line_2_pos.position.x, 0.1f, line_2_pos.position.z));
        }
    }

    void ReflectionOff()
    {
        reflection_obj.SetActive(false);
        line_1.enabled = false;
        line_2.enabled = false;
    }
}
