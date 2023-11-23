using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ToggleSyncController : MonoBehaviourPun, IPunObservable
{
    public GameObject objectToHide;
    public Toggle toggle;
    public Image activeSprite;
    public Image inactiveSprite;

    private bool isObjectActive = true;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(isObjectActive);
        }
        else
        {
            isObjectActive = (bool)stream.ReceiveNext();
            if(objectToHide != null)
            {
                objectToHide.SetActive(isObjectActive);
                UpdateImageToggle();
            }
        }
    }

    [PunRPC]
    public void OnToggleValueChanged()
    {
        if(photonView.IsMine)
        {
            isObjectActive = toggle.isOn;
            photonView.RPC("ToggleObjectActiveState", RpcTarget.All, isObjectActive);
            UpdateImageToggle();
        }
    }

    [PunRPC]
    private void ToggleObjectActiveState(bool state)
    {
        isObjectActive = state;
        if(objectToHide != null)
        {
            objectToHide.SetActive(isObjectActive);
            UpdateImageToggle();
        }
    }

    void UpdateImageToggle()
    {
        if(toggle != null)
        {
            if(objectToHide)
            {
                activeSprite.gameObject.SetActive(true);
                inactiveSprite.gameObject.SetActive(false);
            }
            else
            {
                activeSprite.gameObject.SetActive(false);
                inactiveSprite.gameObject.SetActive(true);
            }
        }
    }

}
