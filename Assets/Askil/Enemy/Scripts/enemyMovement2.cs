using System;
using Unity.Hierarchy;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class enemyMovement2 : MonoBehaviour
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
            agent.SetDestination(mainTarget.transform.position);
        }

        // Start roaming after enough time
        if (investigedFor > timeBeforeInvestigateStop)
        {
            StopInvestigation();
            Roam();
        }

        // Start investigation
        if (levelMaster.playerRunning && levelMaster.playerInDangerArea && mainTarget != player)
        {
            if (!investigating)
            {
                StartCoroutine(FindFirstObjectByType<TextPopUp>().FlashText("He Heard You!", 0.5f, true));
                Investigate(player.transform.position);
            }
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

        // Spawn a gameobject where the enemy should check
        GameObject investigateSpot = new GameObject("Investigating-Spot");
        investigateSpot.transform.position = locationToInvestigate;
        mainTarget = investigateSpot;
        investigedFor = 0;

        agent.speed = Mathf.Lerp(baseSpeedReference, baseSpeedReference * SprintSpeedMultiplier, 0.5f);
        investigating = true;
    }

    public void StopInvestigation()
    {
        investigating = false;
    }
}
