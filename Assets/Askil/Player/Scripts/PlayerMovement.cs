using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 6f;

    [Header("Mouse Settings")]
    public float mouseSensitivity = 2f;

    [Header("References")]
    public Transform cam;
    public List<AudioSource> SFX = new List<AudioSource>();
    public Jump JumpScript;

    public Rigidbody rb;
    private float xRotation = 0f;
    private float startSpeed;

    float ogColliderHeight;
    CapsuleCollider CLDR;

    [SerializeField] float sprintSpeed = 2f;
    [SerializeField] float Acceleration = 2f;

    public float currentSprintMultiplier = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startSpeed = moveSpeed;

        CLDR = rb.GetComponent<CapsuleCollider>();
        ogColliderHeight = CLDR.height;

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMouseLook();
        SprintCheck();
        CrouchCheck();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Combine input directions
        Vector3 direction = transform.right * x + transform.forward * z;

        // Normalize so diagonals aren't faster
        if (direction.magnitude > 1f)
            direction.Normalize();

        Vector3 move = direction * moveSpeed;

        rb.linearVelocity = new Vector3(move.x, rb.linearVelocity.y, move.z);
        SFX[0].mute = move.x == 0f || move.z == 0f || !JumpScript.isGrounded;
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity;

        // Rotate the body
        transform.Rotate(Vector3.up * mouseX);

        // Rotate camera (vertical)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -85f, 85f);

        cam.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    void SprintCheck()
    {
        float targetMultiplier = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : 1f;

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
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            CLDR.height = ogColliderHeight * 0.3f;
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            CLDR.height = ogColliderHeight;
        }
    }
}
