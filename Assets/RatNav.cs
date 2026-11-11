using UnityEngine;
using UnityEngine.AI;

public class RatNav : MonoBehaviour
{
    public NavMeshAgent agent;

    private GameObject[] ratPOI;

    private float cd = 5f;

    private Vector3 targetPos = Vector3.zero;

    public int behavior = 0;

    public bool hasFoundFleePos = false;

    public bool dontUseRats = false;

    public GameObject splat;

    void Start()
    {
        if (dontUseRats)
        {
            Destroy(gameObject);
        }

        agent = GetComponent<NavMeshAgent>();
        ratPOI = GameObject.FindGameObjectsWithTag("RatPOI");
        agent.SetDestination(GameObject.FindWithTag("RatPOI").transform.position);
    }


    void Update()
    {

        cd -= Time.deltaTime;

        if (cd < 0)
        {
            cd = Random.Range(0.2f, 2f);
            //behavior = Random.Range(0, 2);
            behavior = 0;
            //currentCheese = cheeses[Random.Range(0, cheeses.Length)];

            agent.SetDestination(ratPOI[Random.Range(0, ratPOI.Length)].transform.position);

        }


        if (behavior == 0)
        {
            agent.speed = 5;
        }


        // FEAR
        if (behavior == 5)
        {
            agent.speed = 9;
        }


    }


    private void OnTriggerEnter(Collider other)
    {
        

        if (other.CompareTag("Cig") || other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            FindDistantPoi();

            behavior = 5;
            cd = 4f;
            print("FUCK ME!!! HOLY FUCK! WHAT THE FUCK!???");

        }
    }

    private void FindDistantPoi()
    {
        {
            GameObject furthestPOI = ratPOI[0];
            foreach (GameObject poi in ratPOI)
            {
                if ((poi.transform.position - gameObject.transform.position).magnitude > (furthestPOI.transform.position - gameObject.transform.position).magnitude)
                {
                    furthestPOI = poi;
                }
            }

            targetPos = furthestPOI.transform.position;

            hasFoundFleePos = true;
        }
    }


    public void SplatRat()
    {
        GameObject.Instantiate(splat);
        Destroy(gameObject);
    }
}
