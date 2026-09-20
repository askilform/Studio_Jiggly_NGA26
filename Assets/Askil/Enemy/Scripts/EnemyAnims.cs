using UnityEngine;

public class EnemyAnims : MonoBehaviour
{
    private Animator animator;

    public enemyMovement movementSc;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        animator.SetFloat("Speed", movementSc.agent.velocity.magnitude);
        animator.SetFloat(
      "SpeedMultiplier",
      movementSc.mainTarget == movementSc.player ? 2f : 1f
        );
    }

    private void FixedUpdate()
    {
        animator.SetBool("Stunned", movementSc.crippleSpeedMultiplier < 1);
        animator.SetFloat("StunnedMultiplier", (1 - movementSc.crippleSpeedMultiplier) * 5);
    }
}
