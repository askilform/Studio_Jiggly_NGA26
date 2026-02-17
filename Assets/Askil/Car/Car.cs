using Unity.VisualScripting;
using UnityEngine;

public class Car : MonoBehaviour
{
    public Rigidbody rb;
    public CarExitCheck exitSc;

    public float moveSpeed = 20f;
    public float maxSpeedLimit = 30f;
    public float rotationSpeed = 100f;
    public GameObject SeatPosition;

    public GameObject player;
    private TextPopUp uiSc;

    private void OnEnable()
    {
        player.GetComponent<PlayerMovement2>().movementAllowed = false;
        player.GetComponent<CharacterController>().enabled = false;
        uiSc = GameObject.FindFirstObjectByType<TextPopUp>();

        rb.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        float forwardInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");

        // Forward movement
        if (Mathf.Abs(forwardInput) > 0.01f && rb.linearVelocity.magnitude < maxSpeedLimit)
        {
            rb.AddForce(transform.forward * forwardInput * moveSpeed, ForceMode.Acceleration);
        }


        if (Mathf.Abs(turnInput) > 0.01f && forwardInput != 0)
        {
            float direction = forwardInput > 0 ? 1f : -1f;

            Quaternion turnRotation =
                Quaternion.Euler(0f, turnInput * rotationSpeed * direction * Time.fixedDeltaTime, 0f);

            rb.MoveRotation(rb.rotation * turnRotation);
        }


        player.transform.position = SeatPosition.transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!exitSc.isColliding) OnCarExit();
            else uiSc.StartCoroutine(uiSc.FlashText("Door Is Blocked!", 0.5f, false));
        }
    }
    private void OnCarExit()
    {
        player.transform.position = exitSc.transform.position;

        player.GetComponent<PlayerMovement2>().movementAllowed = true;
        player.GetComponent<CharacterController>().enabled = true;

        enabled = false;
    }
}
