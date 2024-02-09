using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneTransition : MonoBehaviour
{
    public GameObject panel;

    [Header("Image")]
    public Image img_background;
    public Image img_loading_progress_bar;
    public Image img_loading;
    public Image img_loading_fill;
    public float give_line_rotate_speed;

    [Header("Text")]
    public TMP_Text t_loading_percentage;

    private AsyncOperation loading_scene_operation;
    private static SceneTransition instance;

    private bool start_loading;

    public void Awake()
    {
        instance = this;

        panel.SetActive(true);
        start_loading = false;
        AnimFinishLoading();
    }

    public void Update()
    {
        if (start_loading)
        {
            t_loading_percentage.text = "Loading... " + Mathf.RoundToInt(loading_scene_operation.progress * 100) + "%";
            img_loading_fill.fillAmount = loading_scene_operation.progress;
            img_loading.transform.Rotate(new Vector3(0, 0, give_line_rotate_speed * Time.deltaTime));
        }
    }

    public static void SwithToScene(string scene_name)
    {
        instance.panel.SetActive(true);

        instance.AnimStartLoading();
        instance.start_loading = true;

        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        instance.loading_scene_operation = SceneManager.LoadSceneAsync(scene_name);
        instance.loading_scene_operation.allowSceneActivation = false;
    }

    void AnimStartLoading()
    {
        img_background.DOColor(new Color(0.1548149f, 0.6239301f, 0.6698113f, 0), 0);
        img_loading_progress_bar.DOColor(new Color(1, 1, 1, 0), 0);
        img_loading.DOColor(new Color(1, 1, 1, 0), 0);
        img_loading_fill.DOColor(new Color(1, 1, 1, 0), 0);
        t_loading_percentage.DOColor(new Color(1, 1, 1, 0), 0);

        img_background.DOColor(new Color(0.1548149f, 0.6239301f, 0.6698113f, 1), 0.5f);
        img_loading_progress_bar.DOColor(new Color(1, 1, 1, 1), 0.5f);
        img_loading.DOColor(new Color(1, 1, 1, 1), 0.5f);
        t_loading_percentage.DOColor(new Color(1, 1, 1, 1), 0.5f);
        img_loading_fill.DOColor(new Color(1, 1, 1, 1), 0.5f).OnComplete(GoToScene);
    }

    void AnimFinishLoading()
    {
        img_background.DOColor(new Color(0.1548149f, 0.6239301f, 0.6698113f, 1), 0);
        img_loading_progress_bar.DOColor(new Color(1, 1, 1, 1), 0);
        img_loading.DOColor(new Color(1, 1, 1, 1), 0);
        img_loading_fill.DOColor(new Color(1, 1, 1, 1), 0);
        t_loading_percentage.DOColor(new Color(1, 1, 1, 1), 0);

        img_background.DOColor(new Color(0.1548149f, 0.6239301f, 0.6698113f, 0), 0.5f);
        img_loading_progress_bar.DOColor(new Color(1, 1, 1, 0), 0.5f);
        img_loading.DOColor(new Color(1, 1, 1, 0), 0.5f);
        img_loading_fill.DOColor(new Color(1, 1, 1, 0), 0.5f);
        t_loading_percentage.DOColor(new Color(1, 1, 1, 0), 0.5f).OnComplete(EndLoading);
    }

    void GoToScene()
    {
        instance.loading_scene_operation.allowSceneActivation = true;
    }

    void EndLoading()
    {
        panel.SetActive(false);
    }
}
