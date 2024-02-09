using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    [SerializeField] string videoFileName;

    public void Start()
    {
        
    }

    public void OnEnable()
    {
        PlayVideo();
    }

    void PlayVideo()
    {
        VideoPlayer videoPlayer = GetComponent<VideoPlayer>();

        if (videoPlayer)
        {
            string videoPatch = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);
            videoPlayer.url = videoPatch;
            videoPlayer.Play();
        }
    }
}
