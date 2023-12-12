using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class ToggleController : MonoBehaviourPunCallbacks, IPunObservable
{
    public Toggle toggle;
    public string tagObject;

    private List<GameObject> savePrefab;

    private void Start()
    {
        // Menambahkan listener pada toggle jika pemain lokal
        if (photonView.IsMine)
        {
            toggle.onValueChanged.AddListener(OnToggleValueChanged);
        }

        savePrefab = new List<GameObject>();


    }

    private void OnToggleValueChanged(bool newValue)
    {
        photonView.RPC("TogglePrefab", RpcTarget.AllBuffered, newValue, tagObject);
    }

    [PunRPC]
    private void TogglePrefab(bool newValue, string tagName)
    {

        GameObject[] prefabs = GameObject.FindGameObjectsWithTag(tagName);

        if (savePrefab == null)
        {
            savePrefab = new List<GameObject>();
        }

        foreach (GameObject prefab in prefabs)
        {
            // Sembunyikan atau tampilkan prefab berdasarkan nilai newValue
            prefab.SetActive(newValue);

            // Jika newValue adalah true (visible), tambahkan prefab ke savePrefab
            if (newValue)
            {
                // Pastikan prefab belum ada di dalam savePrefab sebelum menambahkannya
                if (!savePrefab.Contains(prefab))
                {
                    savePrefab.Add(prefab);
                }
            }
            else // Jika newValue adalah false (hidden)
            {
                // Hapus prefab dari savePrefab jika sudah ada di dalamnya
                if (savePrefab.Contains(prefab))
                {
                    savePrefab.Remove(prefab);
                }
            }

            // Jika newValue adalah true (visible) dan prefab tidak ada di dalam savePrefab
            if (newValue && !savePrefab.Contains(prefab))
            {
                prefab.SetActive(true);
            }
        }

        Debug.Log("Toggle state changed to: " + newValue + " for tag: " + tagName);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // Your existing OnPhotonSerializeView() code remains unchanged...
    }
}