using System;
using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.AI;

public class enemyMovement : MonoBehaviour
{

    private float baseSpeedReference;
    private Vector3 investigateLocation;
    private bool investigating = false;
    private LevelMaster levelMaster;
    

    [NonSerialized] public NavMeshAgent agent;
    [NonSerialized] public GameObject player;
    public GameObject mainTarget;
    public float Speed = 1;
    public float SprintSpeedMultiplier;
    public RoamingPoints roamPointSc;
    public AudioSource walkSFX;

    private void Start()
    {
        baseSpeedReference = Speed;
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        levelMaster = FindFirstObjectByType<LevelMaster>();

        Roam();
    }

    private void Update()
    {
        // Debugs
        if (Input.GetKeyDown(KeyCode.Alpha0)) StopMovement();
        if (Input.GetKeyDown(KeyCode.Alpha2)) SprintFollow();
        if (Input.GetKeyDown(KeyCode.Alpha4)) Roam();

        // Update destination and roaming-point
        if (mainTarget != null)
        {
            if (!investigating) agent.SetDestination(mainTarget.transform.position);
            else agent.SetDestination(investigateLocation);
        }

        if (transform.position == investigateLocation) investigating = false;

        walkSFX.mute = agent.velocity.x == 0 && agent.velocity.z == 0;
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
