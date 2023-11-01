using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class LoginManagers : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_InputField usernameInput;

    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion

    #region UI Callback
    public void ConnectAnonymously()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void ConnectToPhotonServer()
    {
        if(usernameInput != null)
        {
            PhotonNetwork.NickName = usernameInput.text;
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    
    #endregion

    #region Photon Callback Methods

    public override void OnConnected()
    {
        print("OnConnected is called, the server is available ");
    }

    public override void OnConnectedToMaster()
    {
        print("Connect to master server with player name : " + PhotonNetwork.NickName);
        PhotonNetwork.LoadLevel(1);
    }

    #endregion
}
