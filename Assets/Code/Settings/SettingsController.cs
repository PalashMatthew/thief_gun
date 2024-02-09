using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SettingsController : MonoBehaviour
{
    public GameObject canvas;
    public Image panel;
    public Image img_background;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            OpenPopUp();
        }
    }

    public void OpenPopUp()
    {
        canvas.SetActive(true);
        panel.rectTransform.DOLocalMoveY(-1058, 0);
        img_background.DOColor(new Color(0, 0, 0, 0), 0);        

        panel.rectTransform.DOLocalMoveY(98, 0.5f).SetEase(Ease.OutBack);
        img_background.DOColor(new Color(0, 0, 0, 0.5f), 1);
    }

    public void ClosedPopUp()
    {
        panel.rectTransform.DOLocalMoveY(-1058, 0.35f).SetEase(Ease.InBack);
        img_background.DOColor(new Color(0, 0, 0, 0), 0.35f).OnComplete(OffCanvas);
    }

    void OffCanvas()
    {
        canvas.SetActive(false);
    }

    #region Buttons
    public void But_Language()
    {

    }

    public void But_About()
    {

    }

    public void But_Support()
    {

    }

    public void But_Rate()
    {

    }
    #endregion
}
