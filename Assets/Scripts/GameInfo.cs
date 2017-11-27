using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameInfo : MonoBehaviour
{
    private bool hasPickup;
    public Text fps;
    private float respawnTime = 5f;
    private Vector3 respawnPosition;
    public GameObject pickup;
    public PickupInfo[] pickupsData;
    public RawImage imagePick;
    private PickupInfo currentPick;

    private void Start()
    {
        hasPickup = false;
    }

    private void Update()
    {
        fps.text = (1 / Time.smoothDeltaTime).ToString();
        //add random pickups at level start
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "PickUp")
        {
            if (!hasPickup)
            {
                //get random pickup
                currentPick = GetPickup();
                imagePick.texture = currentPick.texture;
                Debug.Log("pick " + currentPick.name);

                //show in UI

                //destroy pickup object and respawn it
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

    private PickupInfo GetPickup()
    {
        //get random pickup
        System.Random random = new System.Random();
        int index = random.Next(0, pickupsData.Length);
        return pickupsData[index];
    }

    public void ActivatePickUp()
    {
        if (hasPickup)
        {
            Debug.Log("Activating" + currentPick.name);
            imagePick.texture = null;
            hasPickup = false;
        }
    }
}