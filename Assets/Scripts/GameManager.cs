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

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        LevelNumber = 1;

        GameMode = GameMode.SinglePlayer;

        RaceStarted = false;

        DidFinishRace = false;
    }
}