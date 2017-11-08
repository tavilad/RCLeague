using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameInfo : MonoBehaviour
{
    private bool hasPickup;
    private PickUps pick;
    public Text fps;

    private void Start()
    {
        hasPickup = false;
    }

    private void Update()
    {
        fps.text = (1 / Time.smoothDeltaTime).ToString();
        //add random pickups at level start

        //respawn pickup
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "PickUp")
        {
            if (!hasPickup)
            {
                //get random pickup
                pick = GetPickup();
                print(pick);

                //show in UI

                //destroy pickup object
                Destroy(col.gameObject);
                hasPickup = true;
            }
        }
    }

    private PickUps GetPickup()
    {
        //cast enum to list
        List<PickUps> pickups = Enum.GetValues(typeof(PickUps)).Cast<PickUps>().ToList();

        //get random pickup
        System.Random random = new System.Random();
        int index = random.Next(0, pickups.Count);
        return pickups[index];
    }

    public void ActivatePickUp()
    {
        if (hasPickup)
        {
            print("Activating " + pick);
            hasPickup = false;
        }
    }
}