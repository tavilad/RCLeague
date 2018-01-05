using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour
{
    private string masterServerIP = "18.195.125.196";
    public Transform[] spawnPoints;
    private int index;
    public Canvas controlCanvas;
    private HostData[] hostList;
    public Canvas lobbyCanvas;

    public GameObject playerPrefab;
    public GameObject buttonPrefab;
    public RawImage img;

    private void Awake()
    {
        // set the IP and port of the Master Server to connect to
        MasterServer.ipAddress = masterServerIP;
        MasterServer.port = 23466;

        // set the IP and port of the Facilitator to connect to
        Network.natFacilitatorIP = masterServerIP;
        Network.natFacilitatorPort = 50005;

        //Network.proxyIP = masterServerIP;
        //Network.proxyPort = 23466;
    }

    private void Start()
    {
        index = 0;
        controlCanvas.gameObject.SetActive(false);
    }

    private void Update()
    {
    }

    public void StartServer(int connections)
    {
        //Network.useProxy = true;
        Network.InitializeServer(5, 5555, true);
        MasterServer.RegisterHost("gametype", "gamename");

        controlCanvas.gameObject.SetActive(true);
        lobbyCanvas.gameObject.SetActive(false);
        SpawnPlayer();
    }

    private void OnServerInitialized()
    {
        Debug.Log("Server started");
    }

    private void OnConnectedToServer()
    {
        SpawnPlayer();
        controlCanvas.gameObject.SetActive(true);
        lobbyCanvas.gameObject.SetActive(false);
    }

    private void SpawnPlayer()
    {
        GameObject car = (GameObject)Network.Instantiate(playerPrefab, spawnPoints[Network.connections.Length].position, Quaternion.identity, index++);
        Debug.Log(Network.connections.Length);
        CameraMovement.target = car.transform;
        GameInfo.imagePick = img;
        ButtonScript.car = car;
        index++;
        print(index);
    }

    private void OnDisconnectedFromServer()
    {
        index--;
    }

    public void Refresh()
    {
        MasterServer.RequestHostList("gametype");
        hostList = MasterServer.PollHostList();
    }

    public void Join()
    {
        Network.Connect(hostList[0]);
    }

    private void OnPlayerConnected(NetworkPlayer player)
    {
        Debug.Log("Player " + " connected from " + player.ipAddress + ":" + player.port);
    }

    private void OnPlayerDisconnected(NetworkPlayer player)
    {
        Debug.Log("Player " + player.ipAddress + " disconnected");
        Network.DestroyPlayerObjects(player);
    }
}