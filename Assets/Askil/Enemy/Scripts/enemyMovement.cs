using System;
using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.AI;

public class enemyMovement : MonoBehaviour
{
    [NonSerialized ]public NavMeshAgent agent;
    private float baseSpeedReference;
    private GameObject target;
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
        if (Input.GetKeyDown(KeyCode.Alpha0)) StopMovement();
        if (Input.GetKeyDown(KeyCode.Alpha1)) WalkFollow();
        if (Input.GetKeyDown(KeyCode.Alpha2)) SprintFollow();
        if (Input.GetKeyDown(KeyCode.Alpha3)) Sneakfollow();
        if (Input.GetKeyDown(KeyCode.Alpha4)) Roam();

        if (target != null) agent.SetDestination(target.transform.position);
    }

    public void StopMovement()
    {
        agent.isStopped = true;
    }

    public void WalkFollow()
    {
        target = player;
        agent.isStopped = false;
        agent.speed = baseSpeedReference;
    }

    public void SprintFollow()
    {
        target = player;
        agent.isStopped = false;
        agent.speed = baseSpeedReference * SprintSpeedMultiplier;
    }

    public void Sneakfollow()
    {
        target = player;
        agent.isStopped = false;
        agent.speed = baseSpeedReference * SneakSpeedMultiplier;
    }

    public void Roam()
    {
        agent.isStopped = false;
        target = roamPointSc.activeRoamingPoint;
    }
}
