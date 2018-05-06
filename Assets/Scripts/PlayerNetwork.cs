using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerNetwork : MonoBehaviour
{
    public static PlayerNetwork Instance;

    public string PlayerName { get; private set; }

    private int PlayersInGame = 0;

    private PhotonView PhotonView;

    [SerializeField] private GameObject playerPrefab;


    private void Awake()
    {
        Instance = this;

        PlayerName = "RandomName#" + Random.Range(10, 1000);

        PhotonView = GetComponent<PhotonView>();

        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }


    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("scene finished loading");
        PhotonView.RPC("RPC_LoadedGameScene", PhotonTargets.MasterClient, PhotonNetwork.player);
    }


    [PunRPC]
    private void RPC_LoadedGameScene(PhotonPlayer photonPlayer)
    {
        PlayersInGame++;
        if (PlayersInGame == PhotonNetwork.playerList.Length)
        {
            Debug.Log("All players are in the game scene.");
            PhotonView.RPC("RPC_CreatePlayer", PhotonTargets.All);
        }
    }

    [PunRPC]
    private void RPC_CreatePlayer()
    {
        GameObject obj = PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity, 0);
    }
}