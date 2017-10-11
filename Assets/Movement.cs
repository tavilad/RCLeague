using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public Vector3 spawn;
    public Vector3 rotation;
    public Rigidbody rb;
    public float distanceToGround = 0.5f;

    private void Start()
    {
        spawn = transform.position;
        rotation = new Vector3(0f, 0f, 0f);
        
        
    }

    private void Update()
    {
        //transform.Translate(moveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime, 0f, moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime);

        if(Input.GetKey(KeyCode.W))
        {
            transform.Translate(0f,0f, moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime);
            
        }

        if(Input.GetKey(KeyCode.A))
        {
            transform.Rotate(new Vector3(0f,-10f,0f)*moveSpeed*Time.deltaTime*Mathf.Abs(Input.GetAxis("Horizontal")));
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0f, 10f, 0f) * moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal"));
        }

        if (transform.position.y < -2 || Input.GetKey(KeyCode.R))
        {
            respawn();
        }
        if (Input.GetKey(KeyCode.Space)&&isGrounded())
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

    private bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distanceToGround + 0.1f);
    }
}