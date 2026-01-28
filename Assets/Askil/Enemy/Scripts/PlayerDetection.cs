using System.Net;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class PlayerDetection : MonoBehaviour
{

    public Transform meshTransform;
    public LayerMask raycastHit;
    public float timeBeforeLosingPlayer = 10;

    //Player
    private GameObject playerObject;
    private Transform playerTransform;

    //PLayer-Detection
    private bool playerSpotted;
    private bool lineCastToPlayer;
    private AudioSource spottedSFX;
    [SerializeField]  private float sinceLastSawPlayer;

    [SerializeField] private enemyMovement movementSc;
    private TextPopUp textPopUpSc;


    private void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        movementSc = GetComponentInParent<enemyMovement>();
        spottedSFX = GetComponent<AudioSource>();
        textPopUpSc = GameObject.Find("TextPopUp").GetComponent<TextPopUp>();
    }

    private void Update()
    {
        playerTransform = playerObject.transform;

        if (Physics.Linecast(meshTransform.position, playerTransform.position, out RaycastHit hitInfo))
        {
            if (hitInfo.collider.tag == "Player") lineCastToPlayer = true; else lineCastToPlayer = false;
        }

        if (sinceLastSawPlayer > timeBeforeLosingPlayer && playerSpotted)
        {
            OnPlayerLost();
            playerSpotted = false;
        }

        else sinceLastSawPlayer += Time.deltaTime;
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
    }

    private void OnPlayerSpot()
    {
        StartCoroutine(textPopUpSc.FlashText("He Sees You!", 0.5f));
        spottedSFX.Play();
        movementSc.SprintFollow();
    }

    private void OnPlayerLost()
    {
        StartCoroutine(textPopUpSc.FlashText("He Lost You!", 1f));
        movementSc.Roam();
    }
}
