﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LapManager : MonoBehaviour
{
    public Transform[] wayPoints = new Transform[2];
    public static int currentCheckpoint;
    public static int currentLap;
    public static Transform[] waypts;
    public static int numberOfLaps = 3;
    public static float timer;

    public delegate void LapDelegate();

    public static event LapDelegate OnRaceFinished;

    [SerializeField] private Text _timerText;

    private void Start()
    {
        currentCheckpoint = 0;
        currentLap = 1;
        waypts = wayPoints;
        waypts[0].gameObject.SetActive(false);
        timer = 0f;
        CheckpointManager.OnLapFinished += HandleOnLapChanged;
    }

    private void Update()
    {
        if (GameManager.Instance.RaceStarted)
        {
            timer += Time.deltaTime;
            _timerText.text = timer.ToString();
            if (currentLap > numberOfLaps)
            {
                GameManager.Instance.RaceStarted = false;
                if (OnRaceFinished != null)
                {
                    OnRaceFinished();
                }
            }
        }
    }


    private void HandleOnLapChanged()
    {
        timer = 0;
    }
}