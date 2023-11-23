using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ToggleSyncController : MonoBehaviourPun, IPunObservable
{
    public GameObject objectToControl;
    public Toggle toggle;
    public Image activeImage; // Image untuk status aktif
    public Image inactiveImage; // Image untuk status tidak aktif
    private bool isObjectActive = true; // Simpan status objek lokal

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(isObjectActive);
        }
        else
        {
            isObjectActive = (bool)stream.ReceiveNext();
            if (objectToControl != null)
            {
                objectToControl.SetActive(isObjectActive);
                UpdateToggleImage();
            }
        }
    }

    public void OnToggleValueChanged()
    {
        Debug.Log("Toggle Value Changed!");
        if (photonView.IsMine)
        {
            isObjectActive = toggle.isOn;
            photonView.RPC("ToggleObjectActiveState", RpcTarget.All, isObjectActive);
            UpdateToggleImage();
        }
    }

    [PunRPC]
    void ToggleObjectActiveState(bool state)
    {
        Debug.Log("Toggle Object Active State RPC received!");
        isObjectActive = state;
        if (objectToControl != null)
        {
            objectToControl.SetActive(isObjectActive);
            UpdateToggleImage();
        }
    }

    void UpdateToggleImage()
    {
        if (toggle != null && activeImage != null && inactiveImage != null)
        {
            if (isObjectActive)
            {
                activeImage.gameObject.SetActive(true);
                inactiveImage.gameObject.SetActive(false);
            }
            else
            {
                activeImage.gameObject.SetActive(false);
                inactiveImage.gameObject.SetActive(true);
            }
        }
        else
        {
            Debug.LogError("Toggle, Active Image, or Inactive Image is not assigned!");
        }
    }
}
