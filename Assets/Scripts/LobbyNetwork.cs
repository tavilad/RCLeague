﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyNetwork : MonoBehaviour
{
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings("0.1");
    }


    public virtual void OnConnectedToMaster()
    {
        Debug.Log("Connected to master");

        PhotonNetwork.automaticallySyncScene = true;
        PhotonNetwork.playerName = GameManager.Instance.PlayerName;

        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }


    public virtual void OnJoinedLobby()
    {
        Debug.Log("Joined lobby");
    }
}