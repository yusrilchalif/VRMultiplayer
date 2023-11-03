using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerNetworkSetup : MonoBehaviourPunCallbacks
{
    public GameObject localXRRigGameObject;
    public GameObject avatarHead;
    public GameObject avatarBody;

    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            localXRRigGameObject.SetActive(true);

            SetLayerRecursively(avatarHead, 6);
            SetLayerRecursively(avatarBody, 7);

            TeleportationArea[] teleportationAreas = GameObject.FindObjectsOfType<TeleportationArea>();

            if (teleportationAreas.Length > 0)
            {
                print("Found " + teleportationAreas.Length + "teleporting area");
                foreach (var item in teleportationAreas)
                {
                    item.teleportationProvider = localXRRigGameObject.GetComponent<TeleportationProvider>();
                }
            }
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
