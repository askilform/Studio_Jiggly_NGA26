using System;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [NonSerialized] public bool isGrounded = false;
    private Rigidbody rb;
    private bool wasHittingLastFrame = false;

    public float jumpForce = 5f;
    public Animator CameraAnims;
    public List<AudioSource> SFX = new List<AudioSource>();


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        CheckGround();
        HandleJump();
    }

    void CheckGround()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        isGrounded = Physics.Raycast(ray, 1.1f);

        if (isGrounded && !wasHittingLastFrame)
        {
            OnLanding();
        }

        wasHittingLastFrame = isGrounded;
    }

    void HandleJump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            SFX[0].Play();
        }
    }

    void OnLanding()
    {
        CameraAnims.SetTrigger("Landed");
        SFX[1].Play();
    }
}
