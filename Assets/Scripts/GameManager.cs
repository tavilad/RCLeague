using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    public int LevelNumber { get; private set; }

    public GameMode GameMode { get; set; }

    public string PlayerName { get; set; }

    public bool RaceStarted { get; set; }

    public bool DidFinishRace { get; set; }

    public List<LapTracker> CarList;

    public GameObject playerPrefab;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        LevelNumber = 1;

        GameMode = GameMode.SinglePlayer;

        RaceStarted = false;

        DidFinishRace = false;

        CarList = new List<LapTracker>();

        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }


    private void Update()
    {
        if (CarList.Count > 0)
        {
            CarList.Sort((x, y) => x.DistanceToCheckpoint.CompareTo(y.DistanceToCheckpoint));

            Debug.Log(CarList[0].GetComponent<PhotonView>().viewID);
        }
    }

    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if ((scene.name.Equals("test") || scene.name.Equals("OvalTrack")) && GameMode == GameMode.TimeTrial)
        {
//            GameManager.Instance.RaceStarted = true;

            GameObject.Find("PickUps").SetActive(false);

            GameObject car = Instantiate(playerPrefab,
                PlayerNetwork.GetSpawnPoints()[0].transform.position,
                PlayerNetwork.GetSpawnPoints()[0].transform.rotation);

            CameraMovement.target = car.transform;

            ButtonScript.wheels = car.GetComponentsInChildren<WheelCollider>();

            ButtonScript.movement = car.GetComponent<Movement>();

            GameInfo.imagePick = GameObject.FindWithTag("PickupRawImage").GetComponent<RawImage>();

            car.GetComponent<TextMeshPro>().text = PhotonNetwork.playerName;

            Debug.Log(PlayerPrefs.GetFloat("BestTime"));

            StartCoroutine("StartRace");
        }
    }

    IEnumerator StartRace()
    {
        yield return new WaitForSeconds(5);

        GameManager.Instance.RaceStarted = true;
    }
}