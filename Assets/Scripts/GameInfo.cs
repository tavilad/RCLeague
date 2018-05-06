using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameInfo : MonoBehaviour
{

    #region Variables
    private bool hasPickup;

   
    private float respawnTime = 5f;

    private Vector3 respawnPosition;
    public GameObject pickupPrefab;
    public PickupInfo[] pickupsData;
    public static RawImage imagePick;
    private PickupInfo currentPick;
    private PhotonView photonView;
    public GameObject trails;


    private Movement movement;
   

    public float explosionPower=3000f;
    public float explosionRadius=1000f;

    private Rigidbody rb;

    #endregion

    private void Start()
    {
        movement = transform.GetComponent<Movement>();
        photonView = transform.GetComponent<PhotonView>();
        rb = transform.GetComponent<Rigidbody>();


        if (photonView.isMine)
        {
            hasPickup = false;
            //transform.position = spawnPoints[0].position;
            imagePick.gameObject.SetActive(false);
        }
    }

    #region PickUp Handling
    private void OnTriggerEnter(Collider col)
    {
        if (photonView.isMine)
        {
            if (col.gameObject.CompareTag("PickUp"))
            {
                if (!hasPickup)
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
                    respawnPosition = col.gameObject.transform.position;
                    StartCoroutine(RespawnPickup(pickupPrefab, respawnPosition));
                    Network.Destroy(col.gameObject);
                }   
                Debug.Log(hasPickup);
            }
        }
    }

    private IEnumerator RespawnPickup(GameObject obj, Vector3 position)
    {
        yield return new WaitForSeconds(respawnTime);
        Network.Instantiate(obj, position, new Quaternion(0, 0, 0, 0), 0);
    }


    private IEnumerator ActivateBattery(float speedBonus)
    {
        trails.SetActive(true);
        movement.maxTorque += speedBonus;
        yield return new WaitForSeconds(10);
        movement.maxTorque -= speedBonus;
        trails.SetActive(false);
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
        if (hasPickup && photonView.isMine)
        {
            switch (currentPick.name)
            {
                case "Battery":
                    Debug.Log("Activating " + currentPick.name);
                    StartCoroutine(ActivateBattery(50));
                    Debug.Log("Battery ended");
                    break;
                case "Rocket":
                    Debug.Log("Activating " + currentPick.name);
                    break;
                case "Bomb":
                    Debug.Log("Activating " + currentPick.name);
                    rb.AddExplosionForce(explosionPower,transform.position,explosionRadius);
                    Debug.Log("Explosion");
                    break;
            }
            imagePick.texture = null;
            imagePick.gameObject.SetActive(false);
            hasPickup = false;
        }
    }


    #endregion
}