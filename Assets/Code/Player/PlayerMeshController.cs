using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMeshController : MonoBehaviour
{
    [Header("Rotate Settings")]
    public float speed;
    public float single_step;
    public float max_delta;

    GameObject player;

    public bool pauseRotate;

    public void Start()
    {
        player = GameObject.Find("Player");
    }

    public void Update()
    {
        transform.position = player.transform.position;

        if (!player.GetComponent<PlayerDash>().change_dir)
        {
            Quaternion nextRotation = Quaternion.Lerp(transform.rotation, player.transform.rotation, Time.deltaTime * 13f);
            transform.rotation = nextRotation;
        }
    }

    public Sequence slowRotate;

    public IEnumerator SlowRot()
    {
        //slowRotate = DOTween.Sequence();
        yield return new WaitForSeconds(0.02f);
        //slowRotate.Append(transform.DORotateQuaternion(player.transform.rotation, 0.1f).SetEase(Ease.OutSine));
    }

    public void SlowRotate(Quaternion rot)
    {
        //transform.DOLocalRotate(rot, 0.2f);
        //transform.DORotate(rot, 0.2f);
        //transform.DORotate(rot, 0.2f);

        Sequence slowRotate = DOTween.Sequence();
        slowRotate.Append(transform.DORotateQuaternion(rot, 0.1f));
    }

    public void FastRotate(Quaternion rot)
    {
        //transform.DOLocalRotate(rot, 0.2f);
        //transform.DORotate(rot, 0.2f);
        //transform.DORotate(rot, 0.2f);
        Sequence fastRotate = DOTween.Sequence();
        fastRotate.Append(transform.DORotateQuaternion(rot, 0f));
    }
}
