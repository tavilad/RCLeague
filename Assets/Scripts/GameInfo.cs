using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using Assets.Scripts;
using System.Linq;

public class GameInfo : MonoBehaviour
{
    private void Start()
    {
    }

    private void Update()
    {
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "PickUp")
        {
            //get random pickup
            PickUps pick = GetPickup();
            print(pick);

            //show in UI

            //destroy pickup object
            Destroy(col.gameObject);
        }
    }

    private PickUps GetPickup()
    {
        //cast enum to list
        List<PickUps> pickups = Enum.GetValues(typeof(PickUps)).Cast<PickUps>().ToList();

        //get random pickup
        System.Random random = new System.Random();
        int pick = random.Next(0, pickups.Count);
        return pickups[pick];
    }
}