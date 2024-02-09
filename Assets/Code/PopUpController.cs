using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PopUpController : MonoBehaviour
{
    public GameObject popUpObj;
    public Image imgBackAlpha;

    private void Start()
    {
        OffPopUpObj();
        imgBackAlpha.DOFade(0, 0);
    }

    public void OpenPopUp()
    {
        if (!popUpObj.active)
        {
            popUpObj.SetActive(true);

            if (imgBackAlpha != null)
            {
                imgBackAlpha.gameObject.SetActive(true);
                imgBackAlpha.DOFade(0.3f, 0.15f);
            }

            Sequence _popUpOpen = DOTween.Sequence();
            _popUpOpen.Append(popUpObj.transform.DOScale(0f, 0f));
            _popUpOpen.Append(popUpObj.transform.DOScale(1.05f, 0.06f));
            _popUpOpen.Append(popUpObj.transform.DOScale(0.97f, 0.06f));
            _popUpOpen.Append(popUpObj.transform.DOScale(1f, 0.06f));
        }
    }

    public void ClosedPopUp()
    {
        if (imgBackAlpha != null)
            imgBackAlpha.DOFade(0f, 0.15f);

        Sequence _popUpOpen = DOTween.Sequence();
        _popUpOpen.Append(popUpObj.transform.DOScale(1.05f, 0.06f));
        _popUpOpen.Append(popUpObj.transform.DOScale(0f, 0.06f).OnComplete(OffPopUpObj));
    }

    private void OffPopUpObj()
    {
        popUpObj.SetActive(false);

        if (imgBackAlpha != null)
            imgBackAlpha.gameObject.SetActive(false);
    }
}
