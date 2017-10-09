using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public Vector3 spawn;
    public Vector3 rotation;
    public Rigidbody rb;

    private void Start()
    {
        spawn = transform.position;
        rotation = new Vector3(0f, 0f, 0f);
    }

    private void Update()
    {
        transform.Translate(moveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime, 0f, moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime);

        if (transform.position.y < -2)
        {
            respawn();
        }
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
        }
    }

    private void respawn()
    {
        print(transform.position.y);
        transform.position = spawn;
        transform.rotation = Quaternion.Euler(rotation);
    }
}