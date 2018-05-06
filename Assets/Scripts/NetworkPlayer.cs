using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayer : Photon.MonoBehaviour
{
    private PhotonView _photonView;

    [SerializeField] private GameObject playerCamera;
    [SerializeField] private MonoBehaviour[] playerScripts;
    
    void Start()
    {
        _photonView = GetComponent<PhotonView>();


        if (!_photonView.isMine)
        {
            playerCamera.SetActive(false);

            foreach (var monoBehaviour in playerScripts)
            {
                monoBehaviour.enabled = false;
            }
        }
    }


}