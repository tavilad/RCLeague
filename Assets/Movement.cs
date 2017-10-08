using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {


    public float moveSpeed = 10f;

	void Start ()
    {
		
	}
	
	
	void Update ()
    {

        //print(Input.GetAxis("Horizontal"));
        transform.Translate(moveSpeed*Input.GetAxis("Horizontal") * Time.deltaTime, 0f, moveSpeed * Input.GetAxis("Vertical")*Time.deltaTime);

	}



}
