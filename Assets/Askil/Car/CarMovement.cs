using Unity.VisualScripting;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public Rigidbody rb;

    public float moveSpeed = 20f;
    public float maxSpeedLimit = 30f;
    public float rotationSpeed = 100f;
    public GameObject SeatPosition;

    public GameObject player;

    private void OnEnable()
    {
        player.GetComponent<PlayerMovement2>().movementAllowed = false;
        player.GetComponent<CharacterController>().enabled = false;
    }

    private void FixedUpdate()
    {
        float forwardInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");

        // Forward movement
        if (forwardInput > 0 && rb.linearVelocity.magnitude < maxSpeedLimit)
        {
            rb.AddForce(transform.forward * forwardInput * moveSpeed, ForceMode.Acceleration);
        }

        // Rotation with A / D
        if (Mathf.Abs(turnInput) > 0.01f)
        {
            Quaternion turnRotation =
                Quaternion.Euler(0f, turnInput * rotationSpeed * Time.fixedDeltaTime, 0f);

            rb.MoveRotation(rb.rotation * turnRotation);
        }

        if (Input.GetKeyDown(KeyCode.Tab)) OnCarExit();

        player.transform.position = SeatPosition.transform.position;
    }

    private void OnCarExit()
    {
        print("[]");
        player.GetComponent<PlayerMovement2>().movementAllowed = true;
        player.GetComponent<CharacterController>().enabled = true;

        enabled = false;
    }
}
