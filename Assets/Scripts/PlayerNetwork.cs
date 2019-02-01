using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerNetwork : MonoBehaviour {
    public static PlayerNetwork Instance;


    private int PlayersInGame = 0;

    private PhotonView PhotonView;

    [SerializeField] private GameObject playerPrefab;

    private List<GameObject> spawnPoints;


    public GameObject CountDownObject;

    private TextMeshProUGUI _countDownText;

    private int _countDownDuration = 5;

    private float _countDownTemp = 5;

    private void Awake() {
        Instance = this;

        PhotonView = GetComponent<PhotonView>();

        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }


    private void Update() {
        if (_countDownText != null) {
            _countDownTemp -= Time.deltaTime;
            _countDownText.text = ((int) _countDownTemp).ToString();
            if (_countDownTemp <= 0) {
                CountDownObject.SetActive(false);
            }
        }
    }


    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode) {
        if (GameManager.Instance.GameMode == GameMode.Multiplayer) {
            Debug.Log("scene finished loading");
            PhotonView.RPC("RPC_LoadedGameScene", PhotonTargets.MasterClient, PhotonNetwork.player);
        }
    }


    [PunRPC]
    private void RPC_LoadedGameScene(PhotonPlayer photonPlayer) {
        PlayersInGame++;
        if (PlayersInGame == PhotonNetwork.playerList.Length) {
            Debug.Log("All players are in the game scene.");
            PhotonView.RPC("RPC_CreatePlayer", PhotonTargets.All);
        }
    }

    [PunRPC]
    private void RPC_CreatePlayer() {
        spawnPoints = GetSpawnPoints();

        GameObject car = PhotonNetwork.Instantiate(playerPrefab.name,
            spawnPoints[PhotonNetwork.player.ID - 1].transform.position,
            spawnPoints[PhotonNetwork.player.ID - 1].transform.rotation, 0);

        CameraMovement.target = car.transform;

        ButtonScript.wheels = car.GetComponentsInChildren<WheelCollider>();

        ButtonScript.movement = car.GetComponent<Movement>();

        GameInfo.imagePick = GameObject.FindWithTag("PickupRawImage").GetComponent<RawImage>();

        car.GetComponentInChildren<TextMeshPro>().text = PhotonNetwork.playerName;


        PickupClickHandler.Car = car;

        CountDownObject = GameObject.FindWithTag("CountDownText");

        _countDownText = CountDownObject.GetComponent<TextMeshProUGUI>();

        car.gameObject.name = PhotonNetwork.playerName;

        StartCoroutine("StartRace");
    }


    public static List<GameObject> GetSpawnPoints() {
        GameObject root = GameObject.FindWithTag("SpawnPoints");

        List<GameObject> points = new List<GameObject>();

        foreach (Transform child in root.transform) {
            points.Add(child.gameObject);
        }

        return points;
    }


    IEnumerator StartRace() {
        yield return new WaitForSeconds(5);

        GameManager.Instance.RaceStarted = true;
    }
}