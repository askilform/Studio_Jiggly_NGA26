using UnityEngine;
using UnityEngine.InputSystem;

public class WheelColliderMovement : MonoBehaviour
{
    float x;
    float z;
    float currentStearAngle;
    float currentBreakForce;

    [Header("Refs")]

    public WheelCollider frontLeftWheelCollider;
    public WheelCollider frontRightWheelCollider;
    public WheelCollider rearLeftWheelCollider;
    public WheelCollider rearRightWheelCollider;


    private void FixedUpdate()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        frontLeftWheelCollider.motorTorque = z * 1000;
        frontRightWheelCollider.motorTorque = z * 1000;

        currentBreakForce = (Input.GetKey(KeyCode.Space)) ? 1000 : 0;

        frontLeftWheelCollider.brakeTorque = currentBreakForce;
        frontRightWheelCollider.brakeTorque = currentBreakForce;
       // rearLeftWheelCollider.brakeTorque = currentBreakForce;
      //  rearRightWheelCollider.brakeTorque = currentBreakForce;

        currentStearAngle = 30 * x; 
        frontLeftWheelCollider.steerAngle = currentStearAngle;
        frontRightWheelCollider.steerAngle = currentStearAngle;
       // rearLeftWheelCollider.steerAngle = currentStearAngle;
       // rearRightWheelCollider.steerAngle = currentStearAngle;
    }
}
