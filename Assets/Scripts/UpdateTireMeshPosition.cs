using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateTireMeshPosition : MonoBehaviour {


    public WheelCollider[] wheelcolliders = new WheelCollider[4];
    public Transform[] tireMeshes = new Transform[4];



    private void Update()
    {
        UpdateTirePosition();
    }


    public void UpdateTirePosition()
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
