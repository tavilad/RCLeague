using Assets;
using UnityEngine;
using UnityEngine.Networking;

public class Movement : MonoBehaviour
{
    #region Car Stats

    //car stats
    public float maxTorque = 50f;
    public float maxSteerAngle = 20f;
    public Transform centerOfMass;
    public WheelCollider[] wheelcolliders = new WheelCollider[4];

    #endregion


    private new Rigidbody rigidbody;
    private GameInfo info;
    private PhotonView _photonView;


    #region SyncVariables

    //sync variables
    private float lastSynchronizationTime = 0f;
    private float syncDelay = 0f;
    private float syncTime = 0f;
    private Vector3 syncStartPosition;
    private Vector3 syncEndPosition;

    #endregion


    


    public void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.centerOfMass = centerOfMass.localPosition;
        info = transform.GetComponent<GameInfo>();
        _photonView = transform.GetComponent<PhotonView>();
        LapManager.OnRaceFinished += HandleOnRaceFinished;
    }


    private void FixedUpdate()
    {
        if (GameManager.Instance.RaceStarted)
        {
            if (_photonView.isMine)
            {
                if (Application.platform == RuntimePlatform.Android)
                {
                    foreach (WheelCollider wheel in wheelcolliders)
                    {
                        wheel.motorTorque = maxTorque;
                    }
                }

                if (Application.platform == RuntimePlatform.WindowsPlayer)
                {
                    wheelcolliders[0].steerAngle = maxSteerAngle * Input.GetAxis("Horizontal");
                    wheelcolliders[1].steerAngle = maxSteerAngle * Input.GetAxis("Horizontal");
                    if (transform.position.y < -2 || Input.GetKey(KeyCode.R))
                    {
                        GameObjectUtil.Respawn(transform);
                    }

                    if (Input.GetKey(KeyCode.F))
                    {
                        if (info != null)
                        {
                            info.ActivatePickUp();
                        }
                        else
                        {
                            print("cant find component");
                        }
                    }

                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        Jump(rigidbody, 15);
                    }

                    foreach (WheelCollider wheel in wheelcolliders)
                    {
                        wheel.motorTorque = maxTorque * Input.GetAxis("Vertical");
                    }
                }

                if (Application.platform == RuntimePlatform.WindowsEditor)
                {
                    wheelcolliders[0].steerAngle = maxSteerAngle * Input.GetAxis("Horizontal");
                    wheelcolliders[1].steerAngle = maxSteerAngle * Input.GetAxis("Horizontal");
                    if (transform.position.y < -2 || Input.GetKey(KeyCode.R))
                    {
                        GameObjectUtil.Respawn(transform);
                    }

                    if (Input.GetKey(KeyCode.F))
                    {
                        if (info != null)
                        {
                            info.ActivatePickUp();
                        }
                        else
                        {
                            print("cant find component");
                        }
                    }

                    if (Input.GetKey(KeyCode.Space))
                    {
                        Jump(rigidbody, 15);
                    }

                    foreach (WheelCollider wheel in wheelcolliders)
                    {
                        wheel.motorTorque = maxTorque * Input.GetAxis("Vertical");
                    }
                }

                if (Input.GetKey(KeyCode.R))
                {
                    GameObjectUtil.Respawn(transform);
                }
            }
            else
            {
                Destroy(this);
            }
        }
        else
        {
            Debug.Log("countdown for race start");
        }
    }

    public void MoveLeft(WheelCollider[] wheels, float steer)
    {
        //left button
        wheels[0].steerAngle = -steer;
        wheels[1].steerAngle = -steer;
    }

    public void MoveRight(WheelCollider[] wheels, float steer)
    {
        //right button
        wheels[0].steerAngle = steer;
        wheels[1].steerAngle = steer;
    }

    public static void Jump(Rigidbody rb, float jumpForce)
    {
        if (GameObjectUtil.isGrounded(rb.transform))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Debug.Log("Jumped");
        }
    }


    private void HandleOnRaceFinished()
    {
        foreach (WheelCollider wheel in wheelcolliders)
        {
            wheel.motorTorque = 0f;
        }
    }
}