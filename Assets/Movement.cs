using Assets;
using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 10f;

    
     
    
    

    private void Start()
    {
        
        
        
        
    }

    private void Update()
    {




        transform.Translate(0f, 0f, moveSpeed * Time.deltaTime);




        if (transform.position.y < -2 || Input.GetKey(KeyCode.R))
        {
            GameObjectUtil.respawn(transform);
        }

  

     

    }

    public static void MoveLeft(GameObject obj,float moveSpeed)
    {
        obj.transform.Rotate(new Vector3(0f, 10f, 0f) * moveSpeed * -Time.deltaTime);
        print("left");
    }

    public static void MoveRight(GameObject obj, float moveSpeed)
    {
        obj.transform.Rotate(new Vector3(0f, 10f, 0f) * moveSpeed * Time.deltaTime);
        print("right");
    }


    public static void Jump(Rigidbody rb)
    {
       if(GameObjectUtil.isGrounded(rb.transform))
        {
            rb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
        }
       
       
    }

    



}