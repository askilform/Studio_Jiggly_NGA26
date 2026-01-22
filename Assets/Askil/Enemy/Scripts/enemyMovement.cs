using System;
using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.AI;

public class enemyMovement : MonoBehaviour
{
    [NonSerialized ]public NavMeshAgent agent;
    private float baseSpeedReference;
    private GameObject target;
    private bool FollowingPlayer;
    [NonSerialized] public GameObject player;


    public float Speed = 1;
    public float SprintSpeedMultiplier;
    public float SneakSpeedMultiplier;
    public RoamingPoints roamPointSc;

    private void Start()
    {
        baseSpeedReference = Speed;
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");     
    }

    private void Update()
    {
        // Debugs
        if (Input.GetKeyDown(KeyCode.Alpha0)) StopMovement();
        if (Input.GetKeyDown(KeyCode.Alpha2)) SprintFollow();
        if (Input.GetKeyDown(KeyCode.Alpha4)) Roam();

        // Update destination and roaming-point
        if (target != null) agent.SetDestination(target.transform.position);
        if (!FollowingPlayer) target = roamPointSc.activeRoamingPoint;
    }

    public void StopMovement()
    {
        agent.isStopped = true;
        FollowingPlayer = false;
    }

    public void SlowFollow()
    {

    }

    public void SprintFollow()
    {
        target = player;
        agent.isStopped = false;
        agent.speed = baseSpeedReference * SprintSpeedMultiplier;
        FollowingPlayer = true;
    }

    public void Roam()
    {
        agent.isStopped = false;
        FollowingPlayer = false;
    }
}
