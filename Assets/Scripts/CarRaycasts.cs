using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRaycasts : MonoBehaviour {
    private int RaysToShoot = 6;


    private Vector3 dir;

    private void Start() {

        for (int i = 0; i < 4; i++) {

            RaycastHit hit;
            if (i == 0) {
                dir = transform.forward;
            }

            if (i == 1) {
                dir = transform.right;
            }

            if (i == 3) {
                dir = -transform.forward;
            }

            if (i == 4) {
                dir = -transform.right;
            }
                
            Debug.DrawLine(transform.position, dir, Color.red, 20, true);
            if (Physics.Raycast(transform.position, dir, out hit)) {
                //here is how to do your cool stuff ;)
            }
        }
    }
}