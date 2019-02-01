using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LapManager : MonoBehaviour {
    public Transform[] wayPoints = new Transform[22];
    public static int currentCheckpoint;
    public static int currentLap;
    public static Transform[] waypts;
    public static int numberOfLaps = 1;
    public static float timer;

    public delegate void LapDelegate();

    public static event LapDelegate OnRaceFinished;

    [SerializeField] private Text _timerText;
    [SerializeField] private Text _bestTimeText;

    public GameObject FinishCanvas;

    private void Start() {
        currentCheckpoint = 0;
        currentLap = 1;
        waypts = wayPoints;
        waypts[0].gameObject.SetActive(false);
        timer = 0f;
        CheckpointManager.OnLapFinished += HandleOnLapChanged;


//        PlayerPrefs.DeleteKey("BestLap");

        if (PlayerPrefs.HasKey("BestLap")) {
            _bestTimeText.text += PlayerPrefs.GetFloat("BestLap");
        } else {
            PlayerPrefs.SetFloat("BestLap", 55);
            _bestTimeText.text += PlayerPrefs.GetFloat("BestLap");
        }
    }

    private void Update() {
        if (GameManager.Instance.RaceStarted) {
            timer += Time.deltaTime;
            _timerText.text = "Current Lap Time:" + (int) timer;
            if (GameManager.Instance.GameMode == GameMode.Multiplayer ||
                GameManager.Instance.GameMode == GameMode.SinglePlayer) {
                if (currentLap > numberOfLaps) {
                    GameManager.Instance.DidFinishRace = true;
                    FinishCanvas.SetActive(true);
                    OnRaceFinished?.Invoke();
                }
            }
        }
    }


    private void HandleOnLapChanged() {
        if (timer < PlayerPrefs.GetFloat("BestLap")) {
            PlayerPrefs.SetFloat("BestLap", timer);
        }

        this._bestTimeText.text = "Best Lap Time: " + PlayerPrefs.GetFloat("BestLap").ToString();

        PlayerPrefs.Save();

        timer = 0f;
        Debug.Log(PlayerPrefs.GetFloat("BestLap"));
    }


    public void OnClickQuit() {
        Application.Quit();
    }
}