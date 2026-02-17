using UnityEditor;
using UnityEngine;

public class LevelMaster : MonoBehaviour
{
    [Header("Enemy")]

    [Header("Music")]
    public AudioSource ChillMusic;
    public AudioSource DangerMusic;
    public AudioSource ChaseMusic;

    [Header("Player")]
    [SerializeField] private PlayerMovement2 playerMovementSc;
    private float ogPlayerSpeed;
    public bool playerCrouching;
    public bool playerRunning;
    public bool playerInDangerArea;
    public bool playerSprinting;

    private void Start()
    {
        playerMovementSc = FindFirstObjectByType<PlayerMovement2>();
        ogPlayerSpeed = playerMovementSc.moveSpeed;
    }

    private void FixedUpdate()
    {
        playerCrouching = playerMovementSc.isCrouching;
        playerRunning = playerMovementSc.moveSpeed > (ogPlayerSpeed * playerMovementSc.sprintSpeed) - 0.5f;
    }
}
