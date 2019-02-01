using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public enum SpeedMeasurement {
    KPH = 0,
    MPH = 1
}


public class GameManager : MonoBehaviour {
    public static GameManager Instance;


    public GameObject CountDownObject;

    private TextMeshProUGUI _countDownText;

    private int _countDownDuration = 5;

    private float _countDownTemp = 5;

    public int LevelNumber { get; private set; }

    public GameMode GameMode { get; set; }

    public string PlayerName { get; set; }

    public bool RaceStarted { get; set; }

    public bool DidFinishRace { get; set; }

    public List<GameObject> CarList;

    public GameObject playerPrefab;

    public SpeedMeasurement SpeedMeasurement;

    private void Awake() {
        if (Instance == null) Instance = this;

        LevelNumber = 1;

        GameMode = GameMode.SinglePlayer;

        RaceStarted = false;

        DidFinishRace = false;

        CarList = new List<GameObject>();

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
        if ((scene.name.Equals("test") || scene.name.Equals("OvalTrack")) && (GameMode == GameMode.TimeTrial || GameMode == GameMode.SinglePlayer)) {
//            GameManager.Instance.RaceStarted = true;

            CountDownObject = GameObject.FindWithTag("CountDownText");

            _countDownText = CountDownObject.GetComponent<TextMeshProUGUI>();

            if (GameMode == GameMode.TimeTrial) {
                GameObject.Find("PickUps").SetActive(false);
            }


            GameObject car = Instantiate(playerPrefab,
                PlayerNetwork.GetSpawnPoints()[0].transform.position,
                PlayerNetwork.GetSpawnPoints()[0].transform.rotation);

            CameraMovement.target = car.transform;

            ButtonScript.wheels = car.GetComponentsInChildren<WheelCollider>();

            ButtonScript.movement = car.GetComponent<Movement>();

            GameInfo.imagePick = GameObject.FindWithTag("PickupRawImage").GetComponent<RawImage>();

            car.GetComponentInChildren<TextMeshPro>().text = PhotonNetwork.playerName;

            PickupClickHandler.Car = car;

            Debug.Log(PlayerPrefs.GetFloat("BestLap"));

            StartCoroutine("StartRace");
        }
    }


    IEnumerator StartRace() {
        yield return new WaitForSeconds(_countDownDuration);

        GameManager.Instance.RaceStarted = true;
    }
}