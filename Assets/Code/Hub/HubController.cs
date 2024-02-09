using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HubController : MonoBehaviour
{
    [Header("Give")]
    public Image img_line_give;
    public Image img_give;
    public float give_line_rotate_speed;

    [Header("All UI")]
    public GameObject player_info;
    public GameObject other_info;
    public GameObject but_battle, but_shop, but_building;


    void Start()
    {
        StartCoroutine(GiveAnim());
    }

    public void Update()
    {
        #region Give Anim
        img_line_give.transform.Rotate(new Vector3(0, 0, -(give_line_rotate_speed * Time.deltaTime)));
        #endregion
    }

    IEnumerator GiveAnim()
    {
        img_give.rectTransform.DOScale(1.1f, 1);
        img_line_give.rectTransform.DOScale(1.1f, 1);

        yield return new WaitForSeconds(1);

        img_give.rectTransform.DOScale(1f, 1);
        img_line_give.rectTransform.DOScale(1f, 1);

        yield return new WaitForSeconds(1);

        StartCoroutine(GiveAnim());
    }

    public void But_Building()
    {
        StartBuildingAnim();
    }

    public void But_Battle()
    {
        SceneTransition.SwithToScene("LevelSelect");
    }

    public void But_Shop()
    {

    }

    #region Building
    void StartBuildingAnim()
    {
        but_battle.GetComponent<RectTransform>().DOMoveY(-142, 0.5f);
        but_shop.GetComponent<RectTransform>().DOMoveY(-142, 0.5f);
        but_building.GetComponent<RectTransform>().DOMoveY(-142, 0.5f);

        player_info.GetComponent<RectTransform>().DOMoveY(166, 0.5f);
        other_info.GetComponent<RectTransform>().DOMoveY(166, 0.5f);

        img_give.rectTransform.DOMoveX(140, 0.5f);
        img_line_give.rectTransform.DOLocalMoveX(177, 0.5f);
    }
    #endregion
}
