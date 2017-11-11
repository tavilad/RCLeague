using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameInfo : MonoBehaviour
{
    private bool hasPickup;
    private PickUps pick;
    public Text fps;
    private float respawnTime = 5f;
    private Vector3 respawnPosition;
    public GameObject pickup;

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

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "PickUp")
        {
            if (!hasPickup)
            {
                //get random pickup
                pick = GetPickup();
                Debug.Log("pick");

                //show in UI

                //destroy pickup object
                respawnPosition = col.gameObject.transform.position;
                StartCoroutine(RespawnPickup(pickup, respawnPosition));
                Destroy(col.gameObject);
                hasPickup = true;
            }
        }
    }

    private IEnumerator RespawnPickup(GameObject obj, Vector3 position)
    {
        yield return new WaitForSeconds(respawnTime);
        Instantiate(obj, position, new Quaternion(0, 0, 0, 0));
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
            Debug.Log("Activating" + pick);
            hasPickup = false;
        }
    }
}