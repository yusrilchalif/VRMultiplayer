using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LoginManagers : MonoBehaviourPunCallbacks
{

    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion

    #region Photon Callback Methods

    public override void OnConnected()
    {
        print("OnConnected is called, the server is available ");
    }

    public override void OnConnectedToMaster()
    {
        print("Connect to master server");
    }

    #endregion
}
