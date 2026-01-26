using System;
using System.Collections.Generic;
using UnityEngine;

public class Jump2 : MonoBehaviour
{
    [NonSerialized] public bool isGrounded;

    public float jumpForce = 5f;
    public float gravity = -20f;

    public Animator CameraAnims;
    public List<AudioSource> SFX = new List<AudioSource>();

    private CharacterController controller;
    private Vector3 velocity;
    private bool wasGroundedLastFrame;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        GroundCheck();
        HandleJump();
        ApplyGravity();
    }

    void GroundCheck()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && !wasGroundedLastFrame)
        {
            OnLanding();
        }

        if (isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f;
        }

        wasGroundedLastFrame = isGrounded;
    }

    void HandleJump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            SFX[0].Play();
        }
    }

    void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(Vector3.up * velocity.y * Time.deltaTime);
    }

    void OnLanding()
    {
        CameraAnims.SetTrigger("Landed");
        SFX[1].Play();
    }
}
