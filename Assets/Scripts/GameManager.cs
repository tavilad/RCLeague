using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    public int LevelNumber { get; private set; }

    public GameMode GameMode { get; set; }

    public string PlayerName { get; set; }

    public bool RaceStarted { get; set; }

    public bool DidFinishRace { get; set; }

    public List<LapTracker> CarList;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        LevelNumber = 1;

        GameMode = GameMode.SinglePlayer;

        RaceStarted = false;

        DidFinishRace = false;

        CarList = new List<LapTracker>();
    }


    private void Update()
    {
        if (CarList.Count > 0)
        {
            CarList.Sort((x, y) => x.DistanceToCheckpoint.CompareTo(y.DistanceToCheckpoint));
            
            Debug.Log(CarList[0].GetComponent<PhotonView>().viewID);
        }
    }
}