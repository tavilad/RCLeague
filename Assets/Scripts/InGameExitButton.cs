using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class InGameExitButton : MonoBehaviour, IPointerClickHandler {
    public void OnPointerClick(PointerEventData eventData) {
        if (GameManager.Instance.GameMode == GameMode.Multiplayer) {
            PhotonNetwork.LeaveRoom();
        }


//        Destroy(GameObject.Find("PlayerNetwork"));
        SceneManager.LoadScene("lobby");
    }

    void OnLeftRoom() { }
}