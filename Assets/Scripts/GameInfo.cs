using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameInfo : MonoBehaviour
{
    private bool hasPickup;

    //public Text fps;
    private float respawnTime = 5f;

    private Vector3 respawnPosition;
    public GameObject pickup;
    public PickupInfo[] pickupsData;
    public static RawImage imagePick;
    private PickupInfo currentPick;
    private NetworkView netView;
    //public Transform[] spawnPoints;

    private void Start()
    {
        netView = transform.GetComponent<NetworkView>();
        if (netView.isMine)
        {
            hasPickup = false;
            //transform.position = spawnPoints[0].position;
            imagePick.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        //fps.text = (1 / Time.smoothDeltaTime).ToString();
        //add random pickups at level start
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "PickUp")
        {
            if (!hasPickup && netView.isMine)
            {
                imagePick.gameObject.SetActive(true);
                //get random pickup
                currentPick = GetPickup();
                imagePick.texture = currentPick.texture;
                Debug.Log("pick " + currentPick.name);

                //show in UI

                //destroy pickup object and respawn it

                //StartCoroutine(RespawnPickup(pickup, respawnPosition));

                hasPickup = true;
            }
            respawnPosition = col.gameObject.transform.position;
            StartCoroutine(RespawnPickup(pickup, respawnPosition));
            Network.Destroy(col.gameObject);
            Debug.Log(hasPickup);
        }
    }

    private IEnumerator RespawnPickup(GameObject obj, Vector3 position)
    {
        yield return new WaitForSeconds(respawnTime);
        Network.Instantiate(obj, position, new Quaternion(0, 0, 0, 0), 0);
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
        if (hasPickup && netView.isMine)
        {
            Debug.Log("Activating" + currentPick.name);
            imagePick.texture = null;
            imagePick.gameObject.SetActive(false);
            hasPickup = false;
        }
    }
}