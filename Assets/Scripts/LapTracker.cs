using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapTracker : MonoBehaviour
{
    private float _distanceToCheckpoint;

    public float DistanceToCheckpoint
    {
        get { return _distanceToCheckpoint; }
    }

    void Start()
    {
        this._distanceToCheckpoint = 0f;
    }


    void Update()
    {
        this._distanceToCheckpoint =
            Vector3.Distance(this.transform.position, LapManager.waypts[LapManager.currentCheckpoint].transform.position);
    }
}