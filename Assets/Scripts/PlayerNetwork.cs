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

    private List<GameObject> spawnPoints;


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

        spawnPoints = GetSpawnPoints();
        
        GameObject car = PhotonNetwork.Instantiate(playerPrefab.name,
            spawnPoints[PhotonNetwork.player.ID - 1].transform.position,
            spawnPoints[PhotonNetwork.player.ID - 1].transform.rotation, 0);

        CameraMovement.target = car.transform;

        ButtonScript.wheels = car.GetComponentsInChildren<WheelCollider>();

        ButtonScript.movement = car.GetComponent<Movement>();
    }


    private List<GameObject> GetSpawnPoints()
    {
        GameObject root = GameObject.FindWithTag("SpawnPoints");

        List<GameObject> points = new List<GameObject>();

        foreach (Transform child in root.transform)
        {
            points.Add(child.gameObject);
        }

        return points;
    }
}