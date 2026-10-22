using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class RatBehavior : MonoBehaviour
{
    public Rigidbody ratRigidBody;
    public GameObject[] cheeses;
    private GameObject currentCheese;
    private int behavior = 0;
    public Vector3 fearPoint = Vector3.zero;

    private float ratRotForce = 5f;
    private float ratForwardsForce = 3f;

    private float ratFleeSpeed = 8f;

    private float cd = 3f;

    private GameObject[] escapeLocations;

    void Start()
    {
        escapeLocations = GameObject.FindGameObjectsWithTag("RatPOI");
    }

    // Update is called once per frame
    void Update()
    {
        cd -= Time.deltaTime;

        if (cd < 0)
        {
            cd = Random.Range(0f, 5f);
            //currentCheese = cheeses[Random.Range(0, cheeses.Length)];
            behavior = Random.Range(0, 2);
        }



        if (behavior == 0)
        {
            ratRigidBody.linearVelocity = ratRigidBody.transform.forward * ratForwardsForce;
            float rot = Mathf.Sin(Time.time) * ratRotForce;
            ratRigidBody.angularVelocity = ratRigidBody.transform.up * rot;
        }

        if (behavior == 1)
        {
            ratRigidBody.linearVelocity = Vector3.zero;
            ratRigidBody.angularVelocity = Vector3.zero;
        }

        // FEAR
        if (behavior == 2)
        {
            ratRigidBody.linearVelocity = Vector3.zero;
            //ratRigidBody.linearVelocity =  - ratRigidBody.position;
        }

        // FEAR
        if (behavior == 3)
        {
           
            ratRigidBody.linearVelocity =  (ratRigidBody.position - fearPoint).normalized * ratFleeSpeed;
            Vector3 asdasd = new Vector3(ratRigidBody.linearVelocity.x, 0, ratRigidBody.linearVelocity.z);
            
            ratRigidBody.transform.LookAt(ratRigidBody.transform.position + asdasd);
        }

        ratRigidBody.linearVelocity = new Vector3(ratRigidBody.linearVelocity.x, -1f, ratRigidBody.linearVelocity.z);


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cig") || other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            //gameObject.transform.position = other.transform.position;
            fearPoint = other.transform.position;
            behavior = 3;
            cd = 4f;
            print("FUCK ME!!! HOLY FUCK! WHAT THE FUCK!???");



        }  
    }

}
