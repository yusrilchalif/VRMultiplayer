using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class ToggleController : MonoBehaviourPunCallbacks, IPunObservable
{
    public GameObject prefabToToggle;
    public Toggle toggle;
    public string tagToToogle;

    private void Start()
    {
        // Menambahkan listener pada toggle jika pemain lokal
        if (photonView.IsMine)
        {
            toggle.onValueChanged.AddListener(OnToggleValueChanged);
        }
    }

    private void OnToggleValueChanged(bool newValue)
    {
        photonView.RPC("ToggleVisibility", RpcTarget.All, newValue);
    }

    [PunRPC]
    private void ToggleVisibility(bool newValue, PhotonMessageInfo info)
    {
        GameObject[] prefabs = GameObject.FindGameObjectsWithTag(tagToToogle);

        foreach (GameObject prefab in prefabs)
        {
            prefab.SetActive(newValue);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // Your existing OnPhotonSerializeView() code remains unchanged...
    }
}