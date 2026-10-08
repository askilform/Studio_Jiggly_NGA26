using FMODUnity;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarMovement : MonoBehaviour
{
    private Rigidbody rb;
    private float SteerDebugStartLocation;
    private float SteeringAdd;
    float x;
    float z;
    public float timeInCar;

    public StudioEventEmitter carEngineAudio;
    public float CurrentRpmRead;
    public float rpmToEngine;

    [Header("Tweaks")]
    public float acceleration;
    public float turnSpeed;
    public float MaxSpeed;

    private void OnEnable()
    {
        timeInCar = 0;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        carEngineAudio.SetParameter("RPM", 0.15f * 6500);
    }

    private void FixedUpdate()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        if (timeInCar > 1.5)
        {
            //Push car with vertical input
            rb.AddForce(transform.forward * z * acceleration, ForceMode.Acceleration);
            rb.maxLinearVelocity = MaxSpeed;
        }
     

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

        CurrentRpmRead = Mathf.Abs(Vector3.Dot(rb.linearVelocity, transform.forward) / MaxSpeed);

        rpmToEngine = Mathf.Lerp(
            (CurrentRpmRead * 6500),
            x > 0.1f ? 6500 : 1000,
            0.2f
            );

        carEngineAudio.SetParameter("RPM", Mathf.RoundToInt(rpmToEngine));

        timeInCar += 0.02f;
    }
}
