using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Photon.Pun;

public class VideoController : MonoBehaviourPun, IPunObservable
{
    private VideoPlayer videoPlayer;
    public Button playVideoBtn;
    private bool isPaused = false;
    public Sprite playButton, pauseButton;

    private void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        playVideoBtn.onClick.AddListener(TogglePlayPause);

        if (photonView.IsMine)
        {
            videoPlayer.Play(); // Mulai video untuk pemain lokal
        }
    }

    [PunRPC]
    private void TogglePlayPause()
    {
        if(photonView.IsMine)
        {
            photonView.RPC("ToggleVideo", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    private void ToggleVideo()
    {
        if(videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
            playVideoBtn.image.sprite = pauseButton;
        }
        else
        {
            videoPlayer.Play();
            playVideoBtn.image.sprite = playButton;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(isPaused); // Mengirim status pause ke semua klien
        }
        else
        {
            isPaused = (bool)stream.ReceiveNext(); // Menerima status pause dari klien lain
        }
    }
}
