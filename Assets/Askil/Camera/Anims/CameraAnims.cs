using UnityEngine;

public class CameraAnims : MonoBehaviour
{
    private Animator cameraAnims;

    public PlayerMovement2 movementSC;
    public Jump jumpSC;
    void Start()
    {
        cameraAnims = GetComponent<Animator>();
    }

    void Update()
    {
        cameraAnims.SetFloat("SpeedMultiplier", movementSC.currentSprintMultiplier);
        cameraAnims.SetBool("IsGrounded", jumpSC.isGrounded);
        cameraAnims.SetBool("Moving", movementSC.controller.velocity.x != 0 || movementSC.controller.velocity.z != 0);
    }
}
