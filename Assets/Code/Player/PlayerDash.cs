using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class PlayerDash : MonoBehaviour
{
    Rigidbody rb;
    public float force = 16f;
    public float slow_force = 5;
    public float new_slow_force;

    public Joystick joy;

    public bool change_dir = false;

    private bool time_slow = true;

    public GameObject vfx_player_bump;

    public bool isStopped;

    public bool rebound;

    public PlayerAudio aud;

    public float smooth_slow;

    public ParticleSystem vfx_sparks;

    //[Header("Joystick")]
    private Image img_fake_joy_stick;
    private Image img_fake_joy_handle;
    private GameObject obj_fake_joy;
    private GameObject obj_joy;
    private Vector2 last_joy_pos;

    public bool isReboundVfx;

    public GameObject rebound_visualize;
    public ParticleSystem vfx_rebound;


    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
        joy = GameObject.Find("Floating Joystick").GetComponent<Joystick>();

        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;

        img_fake_joy_stick = GameObject.Find("fake_joy").GetComponent<Image>();
        img_fake_joy_handle = GameObject.Find("fake_joy_handle").GetComponent<Image>();
        obj_fake_joy = GameObject.Find("fake_joy");
        obj_joy = GameObject.Find("Floating Joystick");

        if (transform.GetComponent<PlayerController>()._slow_time == PlayerController.slow_time.AllSlow)
        {
            time_slow = true;
        }

        if (transform.GetComponent<PlayerController>()._slow_time == PlayerController.slow_time.PlayerSlow)
        {
            time_slow = false;
        }

        rebound = true;

        if (!isReboundVfx)
            rebound_visualize.SetActive(true);
        else
        {
            vfx_rebound.gameObject.SetActive(true);
            vfx_rebound.Play();
        }
    }

    public void Update()
    {
        //GameObject.Find("playerMesh").GetComponent<Animator>().SetBool("rebound", rebound);

        if (!isReboundVfx)
        {
            if (rebound)
            {
                rebound_visualize.SetActive(true);
            }
            else
            {
                rebound_visualize.SetActive(false);
            }
        }

        if (!GameplayController._playerIsStopped)
        {
            FakeJoySettings();
        }

        if (!GameplayController._playerIsStopped)
        {
            if (transform.GetComponent<PlayerController>()._rebound == PlayerController.rebound.EveryTimeRebound)
            {
                if (Input.GetMouseButtonDown(0) && GameObject.Find("GameController").GetComponent<LevelController>().run_access)
                {
                    Camera.main.DOFieldOfView(32, 0.3f);

                    obj_fake_joy.SetActive(false);

                    StopAllCoroutines();
                    Camera.main.GetComponent<CameraController>().StopAllCoroutines();
                    Camera.main.GetComponent<CameraController>().smoothSpeed = 10;

                    change_dir = true;

                    if (time_slow)
                    {
                        Time.timeScale = 0.1f;
                        Time.fixedDeltaTime = Time.timeScale * 0.02f;
                    }

                    img_fake_joy_stick.GetComponent<Animator>().SetTrigger("extra_quit");
                    StopCoroutine(TutorTimer());

                    //if (!vfx_sparks.isPlaying)
                    //{
                    //    vfx_sparks.Play();
                    //}
                }

                if (Input.GetMouseButton(0) && GameObject.Find("GameController").GetComponent<LevelController>().run_access)
                {                   
                    Vector3 moveDirection = new Vector3(joy.Horizontal, 0, joy.Vertical);

                    last_joy_pos = new Vector2(joy.Horizontal, joy.Vertical);

                    if (joy.Horizontal != 0 && joy.Vertical != 0)
                    {
                        transform.forward = -moveDirection;
                        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y - 45f, transform.localEulerAngles.z);
                    }                    
                }

                if (Input.GetMouseButtonUp(0) && GameObject.Find("GameController").GetComponent<LevelController>().run_access)
                {
                    obj_fake_joy.SetActive(true);

                    change_dir = false;
                    StartCoroutine(ReturnTimeScale());
                    Camera.main.GetComponent<CameraController>().BackSpeed();

                    rb.velocity = new Vector3(0, 0, 0);
                    rb.AddForce(transform.forward * force, ForceMode.Impulse);

                    if (last_joy_pos.x != 0 && last_joy_pos.y != 0)
                    {
                        rebound = false;

                        if (GameObject.Find("GameUI").GetComponent<LevelUIController>().isTutorialLevel)
                        {
                            img_fake_joy_stick.GetComponent<Animator>().SetTrigger("tutor_off");

                            img_fake_joy_stick.GetComponent<Animator>().SetTrigger("extra_quit");
                        }
                    }
                    //rebound = true;
                    Camera.main.DOFieldOfView(35, 0.3f);

                    GameObject.Find("GameController").GetComponent<LevelController>().StartRun();
                }
            }

            if (transform.GetComponent<PlayerController>()._rebound == PlayerController.rebound.OneRebound)
            {
                if (rebound)
                {
                    if (Input.GetMouseButtonDown(0) && GameObject.Find("GameController").GetComponent<LevelController>().run_access)
                    {
                        if (isReboundVfx)
                        {
                            vfx_rebound.Stop();
                            vfx_rebound.gameObject.SetActive(false);
                        }
                        //GameObject.Find("PlayerMesh").GetComponent<PlayerMeshController>().slowRotate.Pause();
                        DOTween.Kill(GameObject.Find("PlayerMesh"));

                        Camera.main.DOFieldOfView(32, 0.3f);

                        obj_fake_joy.SetActive(false);

                        StopAllCoroutines();
                        Camera.main.GetComponent<CameraController>().StopAllCoroutines();
                        Camera.main.GetComponent<CameraController>().smoothSpeed = 10;

                        change_dir = true;

                        if (time_slow)
                        {
                            Time.timeScale = 0.1f;
                            Time.fixedDeltaTime = Time.timeScale * 0.02f;
                        }

                        img_fake_joy_stick.GetComponent<Animator>().SetTrigger("extra_quit");
                        StopCoroutine(TutorTimer());

                        //if (!vfx_sparks.isPlaying)
                        //{
                        //    vfx_sparks.Play();
                        //}
                    }

                    if (Input.GetMouseButton(0) && GameObject.Find("GameController").GetComponent<LevelController>().run_access)
                    {
                        Vector3 moveDirection = new Vector3(joy.Horizontal, 0, joy.Vertical);

                        last_joy_pos = new Vector2(joy.Horizontal, joy.Vertical);

                        if (joy.Horizontal != 0 && joy.Vertical != 0)
                        {
                            if (PlayerPrefs.GetString("inversion") == "true")
                            {
                                transform.forward = -moveDirection;
                            }
                            else
                            {
                                transform.forward = moveDirection;
                            }
                            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y - 45f, transform.localEulerAngles.z);
                        }
                    }

                    if (Input.GetMouseButtonUp(0) && GameObject.Find("GameController").GetComponent<LevelController>().run_access)
                    {
                        if (rb.velocity.magnitude < force)
                            GameObject.Find("playerMesh").GetComponent<Animator>().SetTrigger("start");
                        //GameObject.Find("PlayerMesh").GetComponent<PlayerMeshController>().StartCoroutine(GameObject.Find("PlayerMesh").GetComponent<PlayerMeshController>().PauseRot());

                        obj_fake_joy.SetActive(true);

                        change_dir = false;
                        StartCoroutine(ReturnTimeScale());
                        Camera.main.GetComponent<CameraController>().BackSpeed();

                        rb.velocity = new Vector3(0, 0, 0);
                        rb.AddForce(transform.forward * force, ForceMode.Impulse);
                        Vector3 newMoveDirection = rb.velocity;

                        transform.forward = newMoveDirection;
                        //GameObject.Find("PlayerMesh").transform.rotation = transform.rotation;
                        //GameObject.Find("PlayerMesh").transform.forward = transform.forward;

                        //if (last_joy_pos.x != 0 && last_joy_pos.y != 0)
                        //{
                        //    rebound = false;
                        //}
                        //rebound = false;
                        if (GameObject.Find("GameUI").GetComponent<LevelUIController>().isTutorialLevel)
                        {
                            //img_fake_joy_stick.GetComponent<Animator>().SetTrigger("tutor_off");

                            img_fake_joy_stick.GetComponent<Animator>().SetTrigger("extra_quit");
                        }
                            

                        //rebound = true;
                        Camera.main.DOFieldOfView(35, 0.3f);

                        GameObject.Find("GameController").GetComponent<LevelController>().StartRun();
                    }
                }
            }

            if (!change_dir)
            {
                if (rb.velocity.magnitude < force)
                {
                    rb.velocity = rb.velocity.normalized * force;
                }

                if (rb.velocity.magnitude > force)
                {
                    rb.velocity = rb.velocity.normalized * force;
                }

                Vector3 newMoveDirection = rb.velocity;

                transform.forward = newMoveDirection;

                //GameObject.Find("PlayerMesh").transform.rotation = transform.rotation;
            }

            if (change_dir && !time_slow)
            {
                if (rb.velocity.magnitude < slow_force)
                {
                    rb.velocity = rb.velocity.normalized * slow_force;
                }

                if (rb.velocity.magnitude > slow_force)
                {
                    rb.velocity = rb.velocity.normalized * slow_force;
                }
                
            }

            if (change_dir)
                GameObject.Find("PlayerMesh").transform.rotation = transform.rotation;
        }
    }    

    IEnumerator ReturnTimeScale()
    {
        while (Time.timeScale < 1)
        {
            Time.timeScale += Time.unscaledDeltaTime * smooth_slow;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;

            if (Time.timeScale > 1)
            {
                Time.timeScale = 1;
                Time.fixedDeltaTime = Time.timeScale * 0.02f;
            }
            yield return null;
        }        
    }
 
    public void StoppedPlayer()
    {
        change_dir = false;
        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        rb.velocity = new Vector3(0, 0, 0);

        var vfx_main = vfx_sparks.main;
        vfx_main.loop = false;

        obj_fake_joy.SetActive(false);
        obj_joy.SetActive(false);
    }

    public void RestartPlayer()
    {
        var vfx_main = vfx_sparks.main;
        vfx_main.loop = true;

        obj_fake_joy.SetActive(true);
        rebound = true;
    }

    void FakeJoySettings()
    {
        if (rebound)
        {
            //obj_joy.GetComponent<JoySettings>().back.SetActive(true);
            img_fake_joy_stick.color = new Vector4(1, 1, 1, 1);
            img_fake_joy_handle.color = new Vector4(1, 1, 1, 1);
            
        }
        else
        {
            img_fake_joy_stick.color = new Vector4(1, 1, 1, 0.4f);
            img_fake_joy_handle.color = new Vector4(1, 1, 1, 0.4f);
            
            obj_joy.GetComponent<JoySettings>().back.SetActive(false);
        }
    }

    void CheckTap()
    {
        rebound = true;
        FakeJoySettings();
        if (isReboundVfx)
        {
            vfx_rebound.Stop();
            vfx_rebound.gameObject.SetActive(false);
        }
        obj_joy.GetComponent<JoySettings>().back.SetActive(true);
        //GameObject.Find("PlayerMesh").GetComponent<PlayerMeshController>().slowRotate.Pause();
        DOTween.Kill(GameObject.Find("PlayerMesh"));

        Camera.main.DOFieldOfView(17, 0.3f);

        obj_fake_joy.SetActive(false);

        StopAllCoroutines();
        Camera.main.GetComponent<CameraController>().StopAllCoroutines();
        Camera.main.GetComponent<CameraController>().smoothSpeed = 10;

        change_dir = true;

        if (time_slow)
        {
            Time.timeScale = 0.1f;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }
    }

    #region Collisions
    void OnCollisionEnter(Collision collision)
    {
        GameObject _vfx = Instantiate(vfx_player_bump, collision.contacts[0].point, transform.rotation);
        Destroy(_vfx, 1);

        if (!rebound)
        {
            if (GameObject.Find("GameUI").GetComponent<LevelUIController>().isTutorialLevel)
            {
                //img_fake_joy_stick.GetComponent<Animator>().ResetTrigger("tutor_off");
                //img_fake_joy_stick.GetComponent<Animator>().SetTrigger("tutor_on");

                img_fake_joy_stick.GetComponent<Animator>().ResetTrigger("extra_quit");
                img_fake_joy_stick.GetComponent<Animator>().SetTrigger("extra_tutor");
            }

            StartCoroutine(TutorTimer());

            if (isReboundVfx)
            {
                vfx_rebound.gameObject.SetActive(true);
                vfx_rebound.Play();
            }

            if (Input.GetMouseButton(0) && GameObject.Find("GameController").GetComponent<LevelController>().run_access)
                CheckTap();
        }

        rebound = true;
        
        GameObject.Find("PlayerMesh").GetComponent<PlayerMeshController>().StartCoroutine(GameObject.Find("PlayerMesh").GetComponent<PlayerMeshController>().SlowRot());
        if (collision.gameObject.tag == "wall")
        {
            if (GameplayController._playerIsStopped != true)
                aud.Play_Bump();

            //Sequence _scale = DOTween.Sequence();

            //_scale.Append(transform.DOScaleZ(0.5f, 0.15f));
            //_scale.Append(transform.DOScaleZ(0.65f, 0.15f));
        }
    }

    IEnumerator TutorTimer()
    {
        yield return new WaitForSeconds(5);
        img_fake_joy_stick.GetComponent<Animator>().ResetTrigger("extra_quit");
        img_fake_joy_stick.GetComponent<Animator>().SetTrigger("extra_tutor");

        StartCoroutine(TutorTimer());
    }

    #endregion
}
    