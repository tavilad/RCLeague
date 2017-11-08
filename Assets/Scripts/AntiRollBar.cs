using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiRollBar : MonoBehaviour
{
    public WheelCollider wheelLeft;
    public WheelCollider wheelRight;

    public float antiRoll = 50;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        WheelHit hit;
        float travelL = 1.0f;
        float travelR = 1.0f;

        //see if wheel is grounded
        bool groundedL = wheelLeft.GetGroundHit(out hit);

        //calculate suspension travel :0=fully compressed, 1=fully extended
        if (groundedL)
            travelL = (-wheelLeft.transform.InverseTransformPoint(hit.point).y - wheelLeft.radius) / wheelLeft.suspensionDistance;

        //see if wheel is grounded
        bool groundedR = wheelRight.GetGroundHit(out hit);

        //calculate suspension travel :0=fully compressed, 1=fully extended
        if (groundedR)
            travelR = (-wheelRight.transform.InverseTransformPoint(hit.point).y - wheelRight.radius) / wheelRight.suspensionDistance;

        //calculate the antiroll force: travel distance *antiroll
        float antiRollForce = (travelL - travelR) * antiRoll;

        //substract from one spring
        if (groundedL)
            rb.AddForceAtPosition(wheelLeft.transform.up * -antiRollForce,
                   wheelLeft.transform.position);

        //add it to the other
        if (groundedR)
            rb.AddForceAtPosition(wheelRight.transform.up * antiRollForce,
                   wheelRight.transform.position);
    }
}