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

    private GameObject instantiatedPrefab; // Prefab yang telah di-instantiate di scene

    private void Start()
    {
        // Mengecek apakah prefabToToggle sudah di-instantiate di scene
        instantiatedPrefab = GameObject.Find(prefabToToggle.name);

        // Menambahkan listener pada toggle jika pemain lokal
        if (photonView.IsMine)
        {
            toggle.onValueChanged.AddListener(OnToggleValueChanged);
        }

        // Mengatur toggle agar sesuai dengan status awal instantiatedPrefab (jika sudah ada di scene)
        if (instantiatedPrefab != null)
        {
            toggle.isOn = instantiatedPrefab.activeSelf;
        }
    }

    private void OnToggleValueChanged(bool newValue)
    {
        // Jika prefab belum di-instantiate, instantiate prefabToToggle di scene
        if (instantiatedPrefab == null)
        {
            if (PhotonNetwork.IsConnected)
            {
                instantiatedPrefab = PhotonNetwork.Instantiate(prefabToToggle.name, Vector3.zero, Quaternion.identity);
                instantiatedPrefab.name = prefabToToggle.name; // Memberikan nama yang sama dengan prefab
            }
            else
            {
                instantiatedPrefab = Instantiate(prefabToToggle, Vector3.zero, Quaternion.identity);
                instantiatedPrefab.name = prefabToToggle.name;
            }
        }

        // Mengubah status active atau inactive sesuai dengan nilai toggle secara lokal
        instantiatedPrefab.SetActive(newValue);

        // Mengirim pesan ke semua pemain untuk mengubah status prefab
        photonView.RPC("TogglePrefab", RpcTarget.AllBuffered, newValue);
    }

    [PunRPC]
    private void TogglePrefab(bool newValue)
    {
        // Memperbarui status prefab berdasarkan data yang diterima dari pemain lain
        if (instantiatedPrefab != null)
        {
            instantiatedPrefab.SetActive(newValue);
        }
        else
        {
            Debug.LogWarning("InstantiatedPrefab is null. Unable to toggle prefab status.");
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // Tidak perlu menggunakan OnPhotonSerializeView untuk broadcast hide game object
        // karena informasi yang dikirim hanya berupa status hide atau unhide saja
    }
}
