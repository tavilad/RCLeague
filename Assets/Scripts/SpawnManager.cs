using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject playerPrefab;
    public static bool isSinglePlayer;
    public static bool isMultiPlayer;

    private void Start()
    {
        if (isSinglePlayer)
        {
            playerPrefab.transform.position = spawnPoints[0].position;
        }
        else
            if (isMultiPlayer)
            {
            }
    }

    private void Update()
    {
    }
}