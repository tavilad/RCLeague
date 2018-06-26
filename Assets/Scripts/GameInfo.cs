using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameInfo : MonoBehaviour {
    #region Variables

    private bool hasPickup;


    private float respawnTime = 15f;

    private Vector3 respawnPosition;
    public GameObject pickupPrefab;
    public PickupInfo[] pickupsData;
    public static RawImage imagePick;
    private PickupInfo currentPick;
    private PhotonView photonView;
    public GameObject trails;


    private Movement movement;


    public float explosionPower = 3000f;
    public float explosionRadius = 1000f;

    private Rigidbody rb;

    #endregion

    private void Start() {
        movement = transform.GetComponent<Movement>();
        photonView = transform.GetComponent<PhotonView>();
        rb = transform.GetComponent<Rigidbody>();


        if (photonView.isMine) {
            hasPickup = false;
            imagePick.gameObject.SetActive(false);
        } else {
            if (GameManager.Instance.GameMode == GameMode.SinglePlayer) {
                hasPickup = false;
                imagePick.gameObject.SetActive(false);
            }
        }
    }

    #region PickUp Handling

    private void OnTriggerEnter(Collider col) {
        if (photonView.isMine) {
            if (col.gameObject.CompareTag("PickUp")) {
                if (!hasPickup) {
                    imagePick.gameObject.SetActive(true);
                    currentPick = GetPickup();
                    imagePick.texture = currentPick.texture;
                    Debug.Log("pick " + currentPick.name);


                    hasPickup = true;
                    respawnPosition = col.gameObject.transform.position;
                    StartCoroutine(RespawnPickup(pickupPrefab, respawnPosition));
                    photonView.RPC("DestroyPickup", PhotonTargets.All, col.gameObject.name);
                }
            }
        } else {
            if (GameManager.Instance.GameMode == GameMode.SinglePlayer) {
                if (col.gameObject.CompareTag("PickUp")) {
                    if (!hasPickup) {
                        imagePick.gameObject.SetActive(true);
                        currentPick = GetPickup();
                        imagePick.texture = currentPick.texture;
                        Debug.Log("pick " + currentPick.name);


                        hasPickup = true;
                        respawnPosition = col.gameObject.transform.position;
                        StartCoroutine(RespawnPickup(pickupPrefab, respawnPosition));
                        if (GameManager.Instance.GameMode == GameMode.Multiplayer) {
                            photonView.RPC("DestroyPickup", PhotonTargets.All, col.gameObject.name);
                        } else {
                            if (GameManager.Instance.GameMode == GameMode.SinglePlayer) {
                                Destroy(col.gameObject);
                            }
                        }
                    }
                }
            }
        }
    }

    private IEnumerator RespawnPickup(GameObject obj, Vector3 position) {
        yield return new WaitForSeconds(respawnTime);
//        PhotonNetwork.Instantiate(obj.name, position, new Quaternion(0, 0, 0, 0), 0, null);
        if (GameManager.Instance.GameMode == GameMode.Multiplayer) {
            photonView.RPC("RespawnPickup", PhotonTargets.All, obj.name, position);
        } else {
            if (GameManager.Instance.GameMode == GameMode.SinglePlayer) {
                Instantiate(pickupPrefab, position, Quaternion.identity);
            }
        }
    }


    private IEnumerator ActivateBattery(float speedBonus) {
        trails.SetActive(true);
        movement.maxTorque += speedBonus;
        yield return new WaitForSeconds(3);
        movement.maxTorque -= speedBonus;
        trails.SetActive(false);
    }

    private PickupInfo GetPickup() {
        //get random pickup
        System.Random random = new System.Random();
        int index = random.Next(0, pickupsData.Length);
        return pickupsData[index];
    }

    private void FireRocket() {
        Debug.Log("FIRING ROCKET!");
        if (GameManager.Instance.GameMode == GameMode.Multiplayer) {
            PhotonNetwork.Instantiate("RocketTest", transform.position + new Vector3(0f, 0.5f, 0f) + transform.forward * 2,
                transform.rotation, 0);
        } else {
            if (GameManager.Instance.GameMode == GameMode.SinglePlayer) {
                Instantiate(Resources.Load("RocketTest"), transform.position + new Vector3(0f, 0.5f, 0f) + transform.forward * 2,
                    transform.rotation);
            }
        }
    }

    public void ActivatePickUp() {
        if (hasPickup && photonView.isMine) {
            switch (currentPick.name) {
                case "Battery":
                    Debug.Log("Activating " + currentPick.name);
                    StartCoroutine(ActivateBattery(50));
                    Debug.Log("Battery ended");
                    break;
                case "Rocket":
                    Debug.Log("Activating " + currentPick.name);
                    FireRocket();
                    break;
                case "Bomb":
                    Debug.Log("Activating " + currentPick.name);
                    rb.AddExplosionForce(explosionPower, transform.position, explosionRadius);
                    Debug.Log("Explosion");
                    break;
            }

            imagePick.texture = null;
            imagePick.gameObject.SetActive(false);
            hasPickup = false;
        } else {
            if (GameManager.Instance.GameMode == GameMode.SinglePlayer) {
                switch (currentPick.name) {
                    case "Battery":
                        Debug.Log("Activating " + currentPick.name);
                        StartCoroutine(ActivateBattery(50));
                        Debug.Log("Battery ended");
                        break;
                    case "Rocket":
                        Debug.Log("Activating " + currentPick.name);
                        FireRocket();
                        break;
                    case "Bomb":
                        Debug.Log("Activating " + currentPick.name);
                        rb.AddExplosionForce(explosionPower, transform.position, explosionRadius);
                        Debug.Log("Explosion");
                        break;
                }

                imagePick.texture = null;
                imagePick.gameObject.SetActive(false);
                hasPickup = false;
            }
        }
    }

    [PunRPC]
    public void SetPlayerName() {
        GetComponent<TextMeshPro>().text = PhotonNetwork.playerName;
    }

    [PunRPC]
    public void DestroyPickup(string name) {
        Destroy(GameObject.Find(name));
    }

    [PunRPC]
    public void RespawnPickup(string name, Vector3 position) {
        Instantiate(Resources.Load(name, typeof(GameObject)), position, new Quaternion(0, 0, 0, 0));
    }

    #endregion
}