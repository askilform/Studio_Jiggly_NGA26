using UnityEngine;

public class CameraAnims2 : MonoBehaviour
{
    private Animator cameraAnims;

    public PlayerMovement2 movementSC;
    public Jump2 jumpSC;
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

    private void FixedUpdate()
    {
        cameraAnims.enabled = movementSC.movementAllowed;
    }

    public void OnShot()
    {
        cameraAnims.SetTrigger("OnShot");
    }

    public void KnockBack()
    {
        cameraAnims.SetTrigger("KnockBack");
    }
}
