using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretController : MonoBehaviour
{
    [Header("General")]    
    public GameObject gun_obj;

    [Header("Shoting")]
    public GameObject bullet_obj;
    public Transform bullet_spawn_pos;
    public float shoot_speed;
    public float bullet_speed;
    private List<GameObject> inst_bullets_obj = new List<GameObject>();

    [Header("Rotate Settings")]
    public float single_step;
    public float max_delta;

    [Header("Restart")]
    public Vector3 start_gun_rotate;
    public bool start_attack = false;

    [Header("UI")]
    public Image img_progress_bar;
    private float curr_shot_time;

    [Header("Other")]    
    private GameObject target;

    public bool isPlayerVisible;

    public LayerMask layer;
    


    public void Start()
    {
        target = GameObject.Find("Player");
        img_progress_bar.fillAmount = 0;
    }

    public void StartAttack()
    {
        StartCoroutine(Shoting());
        start_attack = true;
        img_progress_bar.fillAmount = 0;
        curr_shot_time = 0;
    }

    public void EndAttack()
    {
        StopAllCoroutines();
        start_attack = false;
        gun_obj.transform.eulerAngles = start_gun_rotate;

        img_progress_bar.fillAmount = 0;

        foreach (GameObject gm in inst_bullets_obj)
        {
            Destroy(gm, 0);
        }

        inst_bullets_obj.Clear();
    }

    public void Update()
    {
        if (start_attack)
        {
            if (isPlayerVisible)
                Rotation();

            curr_shot_time += Time.deltaTime;
            img_progress_bar.fillAmount = curr_shot_time / shoot_speed;
        }

        CheckPlayer();
    }

    void CheckPlayer()
    {
        RaycastHit hit;

        Vector3 playerPos = new Vector3(GameObject.Find("Player").transform.position.x, GameObject.Find("Player").transform.position.y + 0.2f, GameObject.Find("Player").transform.position.z);

        Vector3 dir = playerPos - transform.position;
        //dir = new Vector3(dir.x, 1, dir.z);
        dir = dir.normalized;

        if (Physics.Raycast(transform.position, dir, out hit, Mathf.Infinity, layer))
        {
            Debug.DrawRay(transform.position, dir * hit.distance, Color.yellow);

            if (hit.collider.tag == "Player")
            {
                isPlayerVisible = true;
            }
            else
            {
                isPlayerVisible = false;
            }
        }
    }

    void Rotation()
    {
        Vector3 targetDirection = target.transform.position - gun_obj.transform.position;

        // The step size is equal to speed times frame time.
        float singleStep = single_step * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(gun_obj.transform.forward, targetDirection, singleStep, max_delta);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        gun_obj.transform.rotation = Quaternion.LookRotation(newDirection);

        gun_obj.transform.eulerAngles = new Vector3(0, gun_obj.transform.eulerAngles.y, 0);
    }

    IEnumerator Shoting()
    {
        yield return new WaitForSeconds(shoot_speed);

        if (start_attack)
        {
            if (isPlayerVisible)
                Shot();

            StartCoroutine(Shoting());
        }
    }

    void Shot()
    {
        GameObject _bullet = Instantiate(bullet_obj, bullet_spawn_pos.position, gun_obj.transform.rotation);

        inst_bullets_obj.Add(_bullet);

        _bullet.GetComponent<BulletController>().speed = bullet_speed;

        curr_shot_time = 0;
    }
}
