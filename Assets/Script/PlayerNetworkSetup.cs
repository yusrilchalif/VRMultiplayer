using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerNetworkSetup : MonoBehaviourPunCallbacks
{
    public GameObject localXRRigGameObject;
    public GameObject avatarHead;
    public GameObject avatarBody;

    // Start is called before the first frame update
    void Start()
    {
        if(photonView.IsMine)
        {
            localXRRigGameObject.SetActive(true);

            SetLayerRecursively(avatarHead, 6);
            SetLayerRecursively(avatarBody, 7);
        }
        else
        {
            localXRRigGameObject.SetActive(false);

            SetLayerRecursively(avatarHead, 0);
            SetLayerRecursively(avatarBody, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetLayerRecursively(GameObject go, int layerNumber)
    {
        if (go == null) return;
        foreach (Transform trans in go.GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = layerNumber;
        }
    }
}
