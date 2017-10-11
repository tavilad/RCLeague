using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public GameObject player;       


    private Vector3 positionOffset;

    private Space offsetPositionSpace = Space.Self;

    private bool lookAt = true;


    void Start()
    {

        positionOffset = transform.position - player.transform.position;
        
        
    }


    void LateUpdate()
    {
       

        if (offsetPositionSpace == Space.Self)
        {
            transform.position = player.transform.TransformPoint(positionOffset);
        }
        else
        {
            transform.position = player.transform.position + positionOffset;
        }

        
        if (lookAt)
        {
            transform.LookAt(player.transform);
        }
        else
        {
            transform.rotation = player.transform.rotation;
        }



    }
}

