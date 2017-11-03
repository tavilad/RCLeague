using Assets;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float maxTorque = 50f;
    public float maxSteerAngle = 20f;
    public Transform centerOfMass;
    public WheelCollider[] wheelcolliders = new WheelCollider[4];

    private Rigidbody rigidbody;

    public void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.centerOfMass = centerOfMass.localPosition;
    }

    private void FixedUpdate()
    {
        float steer = maxSteerAngle * Input.GetAxis("Horizontal");
        float speed = maxTorque * Input.GetAxis("Vertical");

        wheelcolliders[0].steerAngle = steer;
        wheelcolliders[1].steerAngle = steer;

        foreach (WheelCollider wheel in wheelcolliders)
        {
            wheel.motorTorque = speed;
        }

        if (transform.position.y < -2 || Input.GetKey(KeyCode.R))
        {
            GameObjectUtil.respawn(transform);
        }
    }

    public static void MoveLeft(GameObject obj, float moveSpeed)
    {
        obj.transform.Rotate(new Vector3(0f, 10f, 0f) * moveSpeed * -Time.deltaTime);
    }

    public static void MoveRight(GameObject obj, float moveSpeed)
    {
        obj.transform.Rotate(new Vector3(0f, 10f, 0f) * moveSpeed * Time.deltaTime);
    }

    public static void Jump(Rigidbody rb)
    {
        if (GameObjectUtil.isGrounded(rb.transform))
        {
            rb.AddForce(new Vector3(0, 20, 0), ForceMode.Impulse);
        }
    }
}