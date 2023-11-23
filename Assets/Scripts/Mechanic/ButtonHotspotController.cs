using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Photon.Pun;
using DG.Tweening;

public class ButtonHotspotController : MonoBehaviourPunCallbacks, IPunObservable, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject panelInformation;

    private Quaternion initialQuaternion;

    [SerializeField] Transform targetObject;
    [SerializeField] float scaleDuration = 1.5f;
    [SerializeField] Vector3 minScale = new Vector3(0.5f, 0.5f, 1.0f);
    [SerializeField] Vector3 maxScale = new Vector3(1.5f, 1.5f, 1.0f);

    private bool isPanelActive = false;

    void Start()
    {
        if (photonView.IsMine)
        {
            panelInformation.SetActive(false);
        }
        initialQuaternion = transform.rotation;

        if (targetObject != null)
        {
            PlayScaleAnimation();
        }
    }

    void Update()
    {
        // Update code here if necessary
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (photonView.IsMine)
        {
            transform.DOScale(new Vector3(1.2f, 1.2f, 1.22f), 0.3f);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (photonView.IsMine)
        {
            transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (photonView.IsMine)
        {
            TogglePopUp();
            photonView.RPC("TogglePopUpRPC", RpcTarget.Others);
        }
    }

    [PunRPC]
    void TogglePopUpRPC()
    {
        TogglePopUp();
    }

    void TogglePopUp()
    {
        isPanelActive = !isPanelActive;

        if (isPanelActive)
        {
            panelInformation.SetActive(true);
        }
        else
        {
            panelInformation.SetActive(false);
        }
    }

    void PlayScaleAnimation()
    {
        if (photonView.IsMine)
        {
            Sequence scaleSequence = DOTween.Sequence();

            scaleSequence.Append(targetObject.DOScale(maxScale, scaleDuration));
            scaleSequence.AppendInterval(1.0f);
            scaleSequence.Append(targetObject.DOScale(minScale, scaleDuration));
            scaleSequence.SetLoops(-1);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(isPanelActive);
        }
        else
        {
            isPanelActive = (bool)stream.ReceiveNext();
            panelInformation.SetActive(isPanelActive);
        }
    }
}
