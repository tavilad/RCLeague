﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public delegate void LapDelegate();

    public static event LapDelegate OnLapChanged;

    private void OnTriggerEnter(Collider collider)
    {
        LapManager.waypts[0].gameObject.SetActive(true);
        if (!collider.CompareTag("Car"))
        {
            Debug.Log(collider.transform.tag);
            return;
        }

        if (transform == LapManager.waypts[LapManager.currentCheckpoint].transform)
        {
            if (LapManager.currentCheckpoint + 1 < LapManager.waypts.Length)
            {
                //Add to currentLap if currentCheckpoint is 0
                if (LapManager.currentCheckpoint == 0)
                    LapManager.currentLap++;
                LapManager.currentCheckpoint++;
                if (OnLapChanged != null)
                    OnLapChanged();
            }
            else
            {
                //If we dont have any Checkpoints left, go back to 0
                LapManager.currentCheckpoint = 0;
            }
        }

        Debug.Log("CheckPoint: " + LapManager.currentCheckpoint + " Lap: " + LapManager.currentLap);
    }
}