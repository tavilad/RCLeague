using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PickupClickHandler : MonoBehaviour, IPointerClickHandler
{



    public void OnPointerClick(PointerEventData eventData)
    {
        GetComponent<GameInfo>().ActivatePickUp();
    }
}
