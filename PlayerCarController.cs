using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarController : MonoBehaviour
{

    [Header("Wheels collider")]
    public WheelCollider frontLeftWheelCollider;
    public WheelCollider frontRightWheelCollider;
    public WheelCollider backLeftWheelCollider;
    public WheelCollider backRightWheelCollider;

    [Header("Wheels Transform")]
    public Transform frontLeftWheelTransform;
    public Transform frontRightWheelTransform;
    public Transform backLeftWheelTransform;
    public Transform backRightWheelTransform;

    [Header("Car Engine")]
    public float accelerationForce = 300f;
    public float breakingForce = 3000f;
    public float presentBreakForce = 0f;
    public float presentAcceleration = 0f;

    [Header("Car Steering")]
    public float WheelsTorque=35f;
    private float presentTurnAngle=0f;

    private void Update()
    {
        MoveCar();
        CarSteering();
        ApplyBreaks();
    }

    private void MoveCar()
    {
        frontLeftWheelCollider.motorTorque = presentAcceleration;
        frontRightWheelCollider.motorTorque = presentAcceleration;
        backLeftWheelCollider.motorTorque = presentAcceleration;
        backRightWheelCollider.motorTorque = presentAcceleration;

        presentAcceleration=accelerationForce * Input.GetAxis("Vertical");
    }

    private void CarSteering()
    {
        presentTurnAngle=WheelsTorque*Input.GetAxis("Horizontal");
        frontLeftWheelCollider.steerAngle=presentTurnAngle;
        frontRightWheelCollider.steerAngle=presentTurnAngle;

        streeingwheels(frontLeftWheelCollider, frontLeftWheelTransform);
        streeingwheels(frontRightWheelCollider, frontRightWheelTransform);
        streeingwheels(backLeftWheelCollider, backLeftWheelTransform);
        streeingwheels(backRightWheelCollider, backRightWheelTransform);
    }

    void streeingwheels(WheelCollider WC, Transform WT)
    {
        Vector3 position;
        Quaternion rotation;

        WC.GetWorldPose(out position,out rotation);

        WT.position=position;
        WT.rotation=rotation;
    }

    public void ApplyBreaks()
    {
        if(Input.GetKey(KeyCode.Space))
            presentBreakForce=breakingForce;

        else
            presentBreakForce=0f;

        frontLeftWheelCollider.brakeTorque = presentBreakForce;
        frontRightWheelCollider.brakeTorque = presentBreakForce;
        backLeftWheelCollider.brakeTorque = presentBreakForce;
        backRightWheelCollider.brakeTorque = presentBreakForce;
    }

}
