using System.Net;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class PlayerDetection : MonoBehaviour
{

    public Transform meshTransform;
    public LayerMask raycastHit;
    public float timeBeforeLosingPlayer = 10;
    public AudioSource onSpottedSFX;

    [Header("Dont Assign")]
    public float sinceLastSawPlayer;

    //Player
    private GameObject playerObject;
    private Transform playerTransform;

    //PLayer-Detection
    private bool playerSpotted;
    private bool lineCastToPlayer;



    [SerializeField] private enemyMovement movementSc;
    private TextPopUp textPopUpSc;
    [SerializeField] private LevelMaster levelMaster;


    private void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        movementSc = GetComponentInParent<enemyMovement>();
        textPopUpSc = GameObject.Find("TextPopUp").GetComponent<TextPopUp>();
        levelMaster = FindFirstObjectByType<LevelMaster>();
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
        }

        else sinceLastSawPlayer += Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player" && lineCastToPlayer && !playerSpotted)
        {
            // While seeing player
            OnPlayerSpot();
        }
    }

    private void OnPlayerSpot()
    {
        if (levelMaster.playerInDangerArea)
        {
            print("[] Enemy Spotted Player");
            StartCoroutine(textPopUpSc.FlashText("He Sees You!", 0.5f));
            onSpottedSFX.Play();
            movementSc.SprintFollow();

            playerSpotted = true;
            sinceLastSawPlayer = 0;
        }
    }

    public void OnPlayerLost()
    {
        print("[] Enemy Lost Player");
        StartCoroutine(textPopUpSc.FlashText("He Lost You!", 1f));
        movementSc.Roam();
        playerSpotted = false;
    }
}
