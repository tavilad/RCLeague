using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CheckpointManager : MonoBehaviour
{
    public delegate void CheckPointDelegate();

    public static event CheckPointDelegate OnLapFinished;


    private void OnTriggerEnter(Collider collider)
    {
        if (!collider.gameObject.GetComponentInParent<PhotonView>().isMine &&
            GameManager.Instance.GameMode == GameMode.Multiplayer)
        {
            return;
        }

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

                OnLapFinished?.Invoke();
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