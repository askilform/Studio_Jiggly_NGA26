using System;
using Unity.Hierarchy;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class enemyMovement : MonoBehaviour
{
    private float baseSpeedReference;
    private Vector3 investigateLocation;
    private LevelMaster levelMaster;
    private EnemyZone enemyZone;
    [SerializeField] private float investigedFor;

    [NonSerialized] public NavMeshAgent agent;
    public GameObject player;
    public bool investigating;
    public GameObject mainTarget;
    public float Speed = 1;

    private float hitSpeedMultiplier = 1; // a fast regaining slow on every hit. Like a little stutter almost.
    public float hitSlowRegainRate = 3f;
 

    public float SprintSpeedMultiplier;
    public RoamingPoints roamPointSc;
    public AudioSource walkSFX;
    public float timeBeforeInvestigateStop;
    public GameObject jumpscarePrefab;

    //a big slow when damaging him a lot.
    private float crippleSpeedMultiplier = 1f; //should always be between 0 and 1
    public float crippleRecoveryTime = 3f; //How many seconds for it to reach 1 again
    private float crippleBuildupCounter = 0; //when the cripple buildup reaches the needed value, his speed is multiplied by 0. The 0 slowly goes back to 1.
    public float crippleBuildupNeeded = 10f;
    public float crippleBuildupAmbientFade = 0.2f; //fade the buildup of cripple away if youre slow
    public UnityEvent OnCrippled;

    private void OnEnable()
    {
        baseSpeedReference = Speed;
        investigating = false;  
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        levelMaster = FindFirstObjectByType<LevelMaster>();
        enemyZone = FindFirstObjectByType<EnemyZone>();

        Roam();
    }

    private void Update()
    {
        // Update destination and roaming-point
        if (mainTarget != null)
        {
            if (!investigating) agent.SetDestination(mainTarget.transform.position);
            else agent.SetDestination(investigateLocation);
        }

        // Stop investigating after enough time
        if (investigedFor > timeBeforeInvestigateStop)
        {
            investigating = false;
            investigedFor = 0;
        }

        // Start investigation
        if (levelMaster.playerRunning && levelMaster.playerInDangerArea && mainTarget != player)
        {
            if (!investigating) StartCoroutine(FindFirstObjectByType<TextPopUp>().FlashText("He Heard You!", 0.5f, true));
            Investigate(player.transform.position);
        }
   
        walkSFX.mute = agent.velocity.x == 0 && agent.velocity.z == 0;

        investigedFor += investigating ? Time.deltaTime : 0;

        CalculatePause(); //handles cripple things

        // if !Playercrouch -- Rotate towards player
        /*
        if (!levelMaster.playerCrouching)
        {
            agent.updateRotation = false;
            transform.LookAt (player.transform.position);
        }

        else agent.updateRotation = true;
        */
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player") Instantiate (jumpscarePrefab, other.transform);
    }

    public void StopMovement()
    {
        agent.isStopped = true;
    }

    public void SprintFollow()
    {
        print("[] Enemy Following player!");
        mainTarget = player;
        agent.isStopped = false;
        agent.speed = baseSpeedReference * SprintSpeedMultiplier * hitSpeedMultiplier * crippleSpeedMultiplier;
    }

    public void Roam()
    {
        print("[] Enemy Roaming!");
        mainTarget = roamPointSc.activeRoamingPoint;
        agent.speed = baseSpeedReference * hitSpeedMultiplier * crippleSpeedMultiplier;
        agent.isStopped = false;
    }

    public void Investigate(Vector3 locationToInvestigate)
    {
        print("[] Enemy Investigating!");
        
        investigateLocation = locationToInvestigate;
        investigating = true;
        agent.speed = Mathf.Lerp(baseSpeedReference, baseSpeedReference * hitSpeedMultiplier * SprintSpeedMultiplier * crippleSpeedMultiplier, 0.5f);
    }

    public void ReduceSpeedMultiplier(float reduceBy)
    {
        hitSpeedMultiplier = reduceBy;
        
    }


    public void GetHitBuildTowardsPause(float damageIn)
    {
        //Connect damage in to this, and after a while he takes a pause.
        crippleBuildupCounter += damageIn;

        if (crippleBuildupCounter >= crippleBuildupNeeded)
        {
            crippleBuildupCounter = 0; //reset counter
            crippleSpeedMultiplier = 0; //slow to zero

            OnCrippled.Invoke();

        }


    }

    private void CalculatePause()
    {

        hitSpeedMultiplier = Mathf.MoveTowards(hitSpeedMultiplier, 1f, Time.deltaTime * hitSlowRegainRate); // a fast regaining slow on every hit.

        crippleBuildupCounter = Mathf.MoveTowards(crippleBuildupCounter, 0f, Time.deltaTime * crippleBuildupAmbientFade);
        //slowly lose cripple buildup

        crippleSpeedMultiplier = Mathf.MoveTowards(crippleSpeedMultiplier, 1f, Time.deltaTime * (1f / Mathf.Max(0.01f, crippleRecoveryTime))); 
        //increase over time (if 3s is the target, 1/3 = 0.3333 per second) (can't divide by zero, that's why the max picks 0.01 if lower) 

    }

}
