using System.Net;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class PlayerDetection : MonoBehaviour
{

    public Transform meshTransform;
    public LayerMask raycastHit;
    public float timeBeforeLosingPlayer = 10;
    public AudioSource onSpottedSFX;
    public float detectionSpeed;
    public Gradient lightGradient;
    public Light headLight;

    [Header("Dont Assign")]
    public float sinceLastSawPlayer;
    public float detectionProcent;

    //Player
    private GameObject playerObject;
    private Transform playerTransform;
    [SerializeField] private Slider enemyDetectionSlider;

    //Player-Detection
    private bool playerSpotted;
    private bool lineCastToPlayer;


    [SerializeField] private enemyMovement movementSc;
    private TextPopUp textPopUpSc;
    [SerializeField] private LevelMaster levelMaster;


    private void OnEnable()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        movementSc = GetComponentInParent<enemyMovement>();
        textPopUpSc = GameObject.Find("TextPopUp").GetComponent<TextPopUp>();
        levelMaster = FindFirstObjectByType<LevelMaster>();
        enemyDetectionSlider = GameObject.Find("EnemyDetectionSlider").GetComponent<Slider>();

        print("[]" + playerObject.transform.name);
    }

    private void Update()
    {
        if (Physics.Linecast(meshTransform.position, playerTransform.position, out RaycastHit hitInfo))
        {
            if (hitInfo.collider.tag == "Player") lineCastToPlayer = true; else lineCastToPlayer = false;
        }

        if (sinceLastSawPlayer > timeBeforeLosingPlayer && playerSpotted)
        {
            OnPlayerLost();
        }

        else sinceLastSawPlayer += Time.deltaTime;

       if (sinceLastSawPlayer > 2 && detectionProcent > 0) detectionProcent -= detectionSpeed * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        playerTransform = playerObject.transform;
        enemyDetectionSlider.value = (detectionProcent / 100);
        headLight.color = lightGradient.Evaluate(detectionProcent / 100);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player" && lineCastToPlayer && !playerSpotted && detectionProcent < 110)
        {
            detectionProcent += detectionSpeed * Time.deltaTime;
            sinceLastSawPlayer = 0;

            if (detectionProcent > 100 && !playerSpotted) OnPlayerSpot();
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
