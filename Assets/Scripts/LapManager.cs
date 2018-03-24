using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LapManager : MonoBehaviour
{
    public Transform[] wayPoints = new Transform[2];
    public static int currentCheckpoint;
    public static int currentLap;
    public static Transform[] waypts;
    public static int numberOfLaps=3;

    private void Start()
    {
        currentCheckpoint = 0;
        currentLap = 1;
        waypts = wayPoints;
        waypts[0].gameObject.SetActive(false);
    }

    private void Update()
    {
        if (currentLap > numberOfLaps)
        {
            SceneManager.LoadScene(0);
        }
    }
}