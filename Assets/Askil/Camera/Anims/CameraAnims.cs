using UnityEngine;

public class CameraAnims : MonoBehaviour
{
    private Animator cameraAnims;

    public PlayerMovement movementSC;
    public Jump jumpSC;
    void Start()
    {
        cameraAnims = GetComponent<Animator>();
    }

    void Update()
    {
        cameraAnims.SetFloat("SpeedMultiplier", movementSC.currentSprintMultiplier);
        cameraAnims.SetBool("IsGrounded", jumpSC.isGrounded);
        cameraAnims.SetBool("Moving", movementSC.rb.linearVelocity.x != 0 || movementSC.rb.linearVelocity.z != 0);
    }
}
