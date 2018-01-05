using Assets;
using UnityEngine;
using UnityEngine.Networking;

public class Movement : MonoBehaviour
{
    public float maxTorque = 50f;
    public float maxSteerAngle = 20f;
    public Transform centerOfMass;
    public WheelCollider[] wheelcolliders = new WheelCollider[4];
    public Transform[] tireMeshes = new Transform[4];




    private new Rigidbody rigidbody;
    private GameInfo info;
    private NetworkView netView;


    private float lastSynchronizationTime = 0f;
    private float syncDelay = 0f;
    private float syncTime = 0f;
    private Vector3 syncStartPosition;
    private Vector3 syncEndPosition;

    

    public void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.centerOfMass = centerOfMass.localPosition;
        info = transform.GetComponent<GameInfo>();
        netView = transform.GetComponent<NetworkView>();
    }

    

    private void FixedUpdate()
    {
        if (netView.isMine)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                //Debug.Log("Android");
                //foreach (WheelCollider wheel in wheelcolliders)
                //{
                //    wheel.motorTorque = maxTorque;
                //}
            }

            if (Application.platform == RuntimePlatform.WindowsPlayer)
            {
                //Debug.Log("pc");
                wheelcolliders[0].steerAngle = maxSteerAngle * Input.GetAxis("Horizontal");
                wheelcolliders[1].steerAngle = maxSteerAngle * Input.GetAxis("Horizontal");
                if (transform.position.y < -2 || Input.GetKey(KeyCode.R))
                {
                    GameObjectUtil.respawn(transform);
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

            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                //Debug.Log("editor");
                wheelcolliders[0].steerAngle = maxSteerAngle * Input.GetAxis("Horizontal");
                wheelcolliders[1].steerAngle = maxSteerAngle * Input.GetAxis("Horizontal");
                if (transform.position.y < -2 || Input.GetKey(KeyCode.R))
                {
                    GameObjectUtil.respawn(transform);
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
                GameObjectUtil.respawn(transform);
            }
        }
        else
        {
            Destroy(this);
            SyncedMovement();
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




    void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
    {
        Vector3 syncPosition = rigidbody.position;
        if (stream.isWriting)
        {
            syncPosition = rigidbody.position;
            stream.Serialize(ref syncPosition);
        }
        else
        {
            stream.Serialize(ref syncPosition);

            syncTime = 0f;
            syncDelay = Time.time - lastSynchronizationTime;
            lastSynchronizationTime = Time.time;

            syncStartPosition = rigidbody.position;
            syncEndPosition = syncPosition;
        }
    }


    private void SyncedMovement()
    {
        syncTime += Time.deltaTime;
        rigidbody.position = Vector3.Lerp(syncStartPosition, syncEndPosition, syncTime / syncDelay);
        transform.position = Vector3.Lerp(syncStartPosition, syncEndPosition, syncTime / syncDelay);
    }


}