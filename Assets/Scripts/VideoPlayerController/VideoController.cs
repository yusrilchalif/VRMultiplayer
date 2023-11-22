using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Photon.Pun;

public class VideoController : MonoBehaviourPun
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] Button playButton;

    public Sprite playIcon, pauseIcon;

    bool isPlayVideo = false;
    private PhotonView pv;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        pv = GetComponent<PhotonView>();
        playButton.onClick.AddListener(TooglePlayPause);

        if(pv.IsMine)
        {
            videoPlayer.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [PunRPC]
    private void TooglePlayPause()
    {
        if(!pv.IsMine)
        {
            if(isPlayVideo)
            {
                videoPlayer.Pause();
                playButton.image.sprite = playIcon;
            }
            else
            {
                videoPlayer.Play();
                playButton.image.sprite = pauseIcon;
            }
        }
    }
}
