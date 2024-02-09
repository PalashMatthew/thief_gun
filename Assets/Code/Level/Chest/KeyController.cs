using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class KeyController : MonoBehaviour
{
    private bool isPickUp;
    public ParticleSystem vfx;


    public void Start()
    {
        transform.position = new Vector3(transform.position.x, 0.07f, transform.position.z);
        vfx.Play();
    }

    public void ResetKey()
    {
        isPickUp = false;
        //StopAllCoroutines();
        vfx.Stop();
        vfx.Clear();
        vfx.Play();
        //transform.position = new Vector3(transform.position.x, 0.1f, transform.position.z);     \
        transform.DOMoveY(0.1f, 0.5f);
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player" && !isPickUp)
        {
            isPickUp = true;
            PickUp();
        }
    }

    void PickUp()
    {
        GameObject.Find("Chest").GetComponent<ChestController>().KeyPickUp();

        Sequence dead_anim = DOTween.Sequence();

        dead_anim.Append(transform.DOMoveY(0.3f, 0.1f));
        dead_anim.Append(transform.DOMoveY(-2, 0.6f));

        GameObject.Find("Player").GetComponent<PlayerController>().aud.Play_Key();

        //StartCoroutine(OffObject());
    }

    IEnumerator OffObject()
    {
        yield return new WaitForSeconds(0.6f);
        if (isPickUp)
        {
            //gameObject.SetActive(false);
            vfx.Stop();
            vfx.Clear();
        }            
    }
}
