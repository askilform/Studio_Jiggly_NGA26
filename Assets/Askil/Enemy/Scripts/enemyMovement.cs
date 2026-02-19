using System;
using Unity.Hierarchy;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class enemyMovement : MonoBehaviour
{
    private float baseSpeedReference;
    private Vector3 investigateLocation;
    private LevelMaster levelMaster;
    private EnemyZone enemyZone;
    [SerializeField] private float investigedFor;

    [NonSerialized] public NavMeshAgent agent;
    [NonSerialized] public GameObject player;
    public bool investigating;
    public GameObject mainTarget;
    public float Speed = 1;
    public float SprintSpeedMultiplier;
    public RoamingPoints roamPointSc;
    public AudioSource walkSFX;
    public float timeBeforeInvestigateStop;
    public GameObject jumpscarePrefab;

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
        if (levelMaster.playerRunning && levelMaster.playerInDangerArea)
        {
            if (!investigating) StartCoroutine(FindFirstObjectByType<TextPopUp>().FlashText("He Heard You!", 0.5f, true));
            Investigate(player.transform.position);
        }
   
        walkSFX.mute = agent.velocity.x == 0 && agent.velocity.z == 0;

        investigedFor += investigating ? Time.deltaTime : 0;


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
        agent.speed = baseSpeedReference * SprintSpeedMultiplier;
    }

    public void Roam()
    {
        print("[] Enemy Roaming!");
        mainTarget = roamPointSc.activeRoamingPoint;
        agent.speed = baseSpeedReference;
        agent.isStopped = false;
    }

    public void Investigate(Vector3 locationToInvestigate)
    {
        print("[] Enemy Investigating!");
        
        investigateLocation = locationToInvestigate;
        investigating = true;
        agent.speed = Mathf.Lerp(baseSpeedReference, baseSpeedReference * SprintSpeedMultiplier, 0.5f);
    }
}
