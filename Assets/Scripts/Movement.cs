using Assets;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float maxTorque = 50f;
    public float maxSteerAngle = 20f;
    public Transform centerOfMass;
    public WheelCollider[] wheelcolliders = new WheelCollider[4];
    public Transform[] tireMeshes = new Transform[4];
    private new Rigidbody rigidbody;
    private GameInfo info;

    public void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.centerOfMass = centerOfMass.localPosition;
        info = transform.GetComponent<GameInfo>();
    }

    private void Update()
    {
        UpdateTireMeshPosition();
    }

    private void FixedUpdate()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            //Debug.Log("Android");
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
                Movement.Jump(rigidbody);
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
                Jump(rigidbody);
            }
        }

        foreach (WheelCollider wheel in wheelcolliders)
        {
            wheel.motorTorque = maxTorque;
        }

        if (Input.GetKey(KeyCode.R))
        {
            GameObjectUtil.respawn(transform);
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

    public static void Jump(Rigidbody rb)
    {
        if (GameObjectUtil.isGrounded(rb.transform))
        {
            rb.AddForce(new Vector3(0, 20, 0), ForceMode.Impulse);
            Debug.Log("Jumped");
        }
    }

    public void UpdateTireMeshPosition()
    {
        for (int i = 0; i < tireMeshes.Length; i++)
        {
            Vector3 position;
            Quaternion rotation;

            wheelcolliders[i].GetWorldPose(out position, out rotation);

            tireMeshes[i].position = position;
            tireMeshes[i].rotation = rotation;
        }
    }
}