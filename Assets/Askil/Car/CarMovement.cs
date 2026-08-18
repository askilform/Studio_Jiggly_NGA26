using UnityEngine;
using UnityEngine.InputSystem;

public class CarMovement : MonoBehaviour
{
    private Rigidbody rb;
    private float SteerDebugStartLocation;
    private float SteeringAdd;
    float x;
    float z;

    [Header("Tweaks")]
    public float acceleration;
    public float turnSpeed;
    public float MaxSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

         //Push car with vertical input
        rb.AddForce(transform.forward * z * acceleration, ForceMode.Acceleration);
        rb.maxLinearVelocity = MaxSpeed;
     

        //rotate car with horizontal input
        // rotates based on how high forward or backwards velocity is
        if (x != 0)
        { 
            rb.MoveRotation(
            rb.rotation * Quaternion.Euler(
                0, 
            (x * turnSpeed * (Vector3.Dot(rb.linearVelocity, transform.forward) / MaxSpeed)),
            0));
        }
    }
}
