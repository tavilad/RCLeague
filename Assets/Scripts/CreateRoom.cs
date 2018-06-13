using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CreateRoom : MonoBehaviour
{

    [SerializeField] private Text _roomName;

    public Text RoomName
    {
        get { return _roomName; }
    }

    public void OnClick_CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions() {IsVisible = true, IsOpen = true, MaxPlayers = 4};
        
        if (PhotonNetwork.CreateRoom(RoomName.text,roomOptions,TypedLobby.Default))
        {
            Debug.Log("room created");
        }
        else
        {
            Debug.Log("cant create room");
        }
    }

    private void OnPhotonCreateRoomFailed(object[] codeAndMessage)
    {
        Debug.Log(codeAndMessage[1]);
    }

    private void OnCreateRoom()
    {
        Debug.Log("room created succesfully");
    }
}
