using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentRoomCanvas : MonoBehaviour {


    public void OnClickStartScene()
    {
        if (PhotonNetwork.player.IsMasterClient)
        {
            PhotonNetwork.LoadLevel(1);
        }
        else
        {
            Debug.Log("Only master client can start the match");
        }
    }
    
    
    
}
