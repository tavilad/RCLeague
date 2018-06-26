using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PickupClickHandler : MonoBehaviour, IPointerClickHandler {
    public static GameObject Car;


    public void OnPointerClick(PointerEventData eventData) {
        Debug.Log("click");

        Car.GetComponent<GameInfo>().ActivatePickUp();
    }
}