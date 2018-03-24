using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PickupTween : MonoBehaviour
{

    
    void Start()
    {
        Sequence pickupSequence=DOTween.Sequence();


        pickupSequence.Append(transform.DOLocalRotate(new Vector3(0, 180, 0), 5f).SetEase(Ease.Linear));
        pickupSequence.Append(transform.DOLocalRotate(new Vector3(0, 360, 0), 5f).SetEase(Ease.Linear));

        pickupSequence.SetEase(Ease.Linear);
        pickupSequence.SetLoops(-1);
    }

   

}