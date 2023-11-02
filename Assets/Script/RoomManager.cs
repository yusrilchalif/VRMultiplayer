using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class RoomManager : MonoBehaviourPunCallbacks
{
    private string mapType;

    [SerializeField] TextMeshProUGUI occupancyRateTextVRLab;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

        if(PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.JoinLobby();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }



    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        print("The message: " + message);
        CreateAndJoinRoom();
    }

    public void OnEnterButtonClicked_VRLAB()
    {
        mapType = MultiplayerVRConstant.MAP_TYPE_VALUE_VRLAB;

        ExitGames.Client.Photon.Hashtable expectedCustomRoomProperties = new ExitGames.Client.Photon.Hashtable { { MultiplayerVRConstant.MAP_TYPE_KEY, mapType } };
        PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties, 0);
    }

    public override void OnCreatedRoom()
    {
        print("A room is created by name: " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnJoinedRoom()
    {
        print("The local player: " + PhotonNetwork.NickName + " Join the room" + PhotonNetwork.CurrentRoom.Name + "Player count " + PhotonNetwork.CurrentRoom.PlayerCount);

        if(PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey(MultiplayerVRConstant.MAP_TYPE_KEY))
        {
            object mapType;
            if(PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(MultiplayerVRConstant.MAP_TYPE_KEY, out mapType))
            {
                print("Joines room with the map: " + (string)mapType);
                if((string)mapType == MultiplayerVRConstant.MAP_TYPE_VALUE_VRLAB)
                {
                    //Load VRLab scene
                    PhotonNetwork.LoadLevel(2);
                }
                //Create else for add a room
            }
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        print(newPlayer.NickName + " joined to " + "Player count: " + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if(roomList.Count == 0)
        {
            occupancyRateTextVRLab.text = 0 + " / " + 15;
        }

        foreach (RoomInfo room in roomList)
        {
            print(room.Name);
            if(room.Name.Contains(MultiplayerVRConstant.MAP_TYPE_VALUE_VRLAB))
            {
                //Update the occupancy text
                print("Room is a vrlab. player count is: " + room.PlayerCount);
                occupancyRateTextVRLab.text = room.PlayerCount + " / " + 15;
            }
            //Create else for add a room
        }
    }

    public override void OnJoinedLobby()
    {
        print("Joined the lobby");
    }

    void CreateAndJoinRoom()
    {
        string randomName = "Room_" + mapType + Random.Range(0, 10000);
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 15;

        string[] roomPropsInLobby = { MultiplayerVRConstant.MAP_TYPE_KEY };
        ExitGames.Client.Photon.Hashtable customRoomProperties = new ExitGames.Client.Photon.Hashtable() { { MultiplayerVRConstant.MAP_TYPE_KEY, mapType} };

        roomOptions.CustomRoomPropertiesForLobby = roomPropsInLobby;
        roomOptions.CustomRoomProperties = customRoomProperties;

        PhotonNetwork.CreateRoom(randomName, roomOptions);
    }
}
