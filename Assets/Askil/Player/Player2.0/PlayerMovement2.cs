using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 6f;
    public float sprintSpeed = 2f;
    public float Acceleration = 2f;
    public float crouchSpeed = 0.2f;
    public float gravity = -20f;

    [Header("Mouse Settings")]
    public float mouseSensitivity = 2f;

    [Header("References")]
    public Transform cam;
    public List<AudioSource> SFX = new List<AudioSource>();
    public Jump2 JumpScript;

    [Header("Dont Assign!")]
    public CharacterController controller;
    public bool isCrouching;
    public bool movementAllowed;
    public float currentSprintMultiplier = 1f;

    private float xRotation = 0f;
    private float startSpeed;
    private float ogHeight;
    private Vector3 velocity;

    float x;
    float z;


    void Start()
    {
        controller = GetComponent<CharacterController>();

        startSpeed = moveSpeed;
        ogHeight = controller.height;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        movementAllowed = true;
    }

    void Update()
    {
        HandleMouseLook();
        SpeedCheck();
        CrouchCheck();

        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        if (movementAllowed) HandleMovement();
    }

    void HandleMovement()
    {
        Vector3 direction = transform.right * x + transform.forward * z;

        if (direction.magnitude > 1f)
            direction.Normalize();

        Vector3 move = direction * moveSpeed;

        // gravity handling
        if (controller.isGrounded && velocity.y < 0f)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;

        Vector3 finalMove = move + Vector3.up * velocity.y;

        controller.Move(finalMove * Time.deltaTime);

        if (SFX.Count > 0 && SFX[0] != null)
        {
            SFX[0].mute = move.sqrMagnitude == 0f || !JumpScript.isGrounded || isCrouching;
        }
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -85f, 85f);

        cam.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    void SpeedCheck()
    {
        float targetMultiplier;

        if (!isCrouching)
            targetMultiplier = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : 1f;
        else
            targetMultiplier = crouchSpeed;

        currentSprintMultiplier = Mathf.Lerp(
            currentSprintMultiplier,
            targetMultiplier,
            Acceleration * Time.deltaTime
        );

        moveSpeed = startSpeed * currentSprintMultiplier;
        SFX[0].pitch = currentSprintMultiplier;
    }

    void CrouchCheck()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && controller.isGrounded)
        {
            controller.height = ogHeight * 0.3f;
            isCrouching = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            controller.height = ogHeight;
            isCrouching = false;
        }
    }
}
