using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public Rigidbody rb;

    public float moveSpeed = 20f;
    public float maxSpeedLimit = 30f;
    public float rotationSpeed = 100f;

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
    }
}
