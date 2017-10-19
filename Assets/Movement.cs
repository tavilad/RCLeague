using Assets;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 10f;

    public Rigidbody rb;

    private void Start()
    {
        //rb = GetComponent<Rigidbody>();
        print(Camera.main.pixelWidth + " " + Camera.main.pixelHeight);
    }

    private void FixedUpdate()
    {
        transform.Translate(0f, 0f, moveSpeed * Time.deltaTime);

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
            rb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
        }
    }
}