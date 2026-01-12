using UnityEngine;

public class gunscript : MonoBehaviour
{

    public LayerMask layerMask;

    public GameObject indicatorObj;


    void Start()
    {
        
    }

    void Update()
    {

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 50f, layerMask))

        { 
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow); 
            Debug.Log("Did Hit"); 
            indicatorObj.transform.position = hit.point;
        }
        else
        { 
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white); 
            Debug.Log("Did not Hit"); 
        }

    }
}
