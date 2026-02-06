using System;
using Unity.Hierarchy;
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

        if (investigedFor > timeBeforeInvestigateStop)
        {
            investigating = false;
            investigedFor = 0;
        }

        if (levelMaster.playerRunning && levelMaster.playerInDangerArea)
        {
            if (!investigating) StartCoroutine(FindFirstObjectByType<TextPopUp>().FlashText("He Heard You!", 0.5f));
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
    }
}
