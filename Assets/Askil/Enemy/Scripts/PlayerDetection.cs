using System.Net;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class PlayerDetection : MonoBehaviour
{

    public Transform meshTransform;
    public LayerMask raycastHit; 

    //Player
    private GameObject playerObject;
    private Transform playerTransform;

    //PLayer-Detection
    private bool playerSpotted;
    private bool lineCastToPlayer;
    private AudioSource spottedSFX;
    private float sinceLastSawPlayer;

    [SerializeField] private enemyMovement movementSc;


    private void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        movementSc = GetComponentInParent<enemyMovement>();
        spottedSFX = GetComponent<AudioSource>();
    }

    private void Update()
    {
        playerTransform = playerObject.transform;

        if (Physics.Linecast(meshTransform.position, playerTransform.position, out RaycastHit hitInfo))
        {
            if (hitInfo.collider.tag == "Player") lineCastToPlayer = true; else lineCastToPlayer = false;
        }

        if (sinceLastSawPlayer > 5 && playerSpotted)
        {
            OnPlayerLost();
            playerSpotted = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player" && lineCastToPlayer && !playerSpotted)
        {
            // While seeing player
            OnPlayerSpot();
            playerSpotted = true;
            sinceLastSawPlayer = 0;
        }

        else sinceLastSawPlayer += Time.deltaTime;
    }

    private void OnPlayerSpot()
    {
        spottedSFX.Play();
        movementSc.SprintFollow();
    }

    private void OnPlayerLost()
    {
        movementSc.Roam();
    }
}
