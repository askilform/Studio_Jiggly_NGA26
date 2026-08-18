using System;
using System.Collections.Generic;
using UnityEngine;

public class Jump2 : MonoBehaviour
{
    [Header("Jump Settings")]
    public float jumpForce = 5f;
    public float gravity = -20f;
    public float coyoteTime = 0.15f;

    [Header("State")]
    public bool isGrounded;

    [Header("References")]
    public Animator CameraAnims;
    public List<AudioSource> SFX = new List<AudioSource>();

    private CharacterController controller;
    private Vector3 velocity;

    [SerializeField] private float groundedTimer;
    private bool wasGroundedLastFrame;
    private float framesSinceLastLanding;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!controller.enabled) return;

        GroundCheck();
        HandleJump();
        ApplyGravity();

        framesSinceLastLanding += 1;
    }

    void GroundCheck()
    {
        bool currentlyGrounded = controller.isGrounded;

        if (currentlyGrounded)
        {
            groundedTimer = coyoteTime;

            if (!wasGroundedLastFrame)
            {
                OnLanding();
            }

            if (velocity.y < 0f)
            {
                velocity.y = -2f; // keeps controller grounded
            }
        }
        else
        {
            groundedTimer -= Time.deltaTime;
        }

        isGrounded = groundedTimer > 0f;
        wasGroundedLastFrame = currentlyGrounded;
    }

    void HandleJump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            print("Attemped Jump!");

            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);

            if (SFX.Count > 0 && SFX[0] != null)
                SFX[0].Play();

            groundedTimer = 0f; // prevents double jump via coyote time
        }
    }

    void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;

        controller.Move(Vector3.up * velocity.y * Time.deltaTime);
    }

    void OnLanding()
    {
        velocity.y = 0;

        if (CameraAnims != null)
            CameraAnims.SetTrigger("Landed");

        if (SFX.Count > 1 && SFX[1] != null && framesSinceLastLanding > 120)
            SFX[1].Play();

        framesSinceLastLanding = 0;
    }
}
