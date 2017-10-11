using Assets;
using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 10f;
    
     
    public Rigidbody rb;
    

    private void Start()
    {
        
        
        
        
    }

    private void Update()
    {
        

        if(Input.GetKey(KeyCode.W))
        {
            transform.Translate(0f,0f, moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime);
            
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0f, 0f, moveSpeed * -Input.GetAxis("Vertical") * Time.deltaTime);

        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(new Vector3(0f,-10f,0f)*moveSpeed*Time.deltaTime*Mathf.Abs(Input.GetAxis("Horizontal")));
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0f, 10f, 0f) * moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal"));
        }

        if (transform.position.y < -2 || Input.GetKey(KeyCode.R))
        {
            GameObjectUtil.respawn(transform);
        }
        if (Input.GetKey(KeyCode.Space)&&   GameObjectUtil.isGrounded(transform))
        {
            rb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
        }
<<<<<<< Updated upstream
=======

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            rb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
        }
    }
>>>>>>> Stashed changes

        if (Input.GetKey(KeyCode.F))
        {
            GameObjectUtil.flip(transform);
            
            
        }

        print(transform.eulerAngles.x);
        print(transform.eulerAngles.y);
        print(transform.eulerAngles.z);


    }

   
    
}