using System.Net;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class PlayerDetection : MonoBehaviour
{
    public Transform meshTransform;
    public LayerMask raycastHit; 

    private GameObject playerObject;
    private Transform playerTransform;
    private bool lookingAtPlayer;

    private void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        playerTransform = playerObject.transform;

        Debug.DrawLine(meshTransform.position, playerTransform.position, Color.red);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            if (Physics.Linecast(meshTransform.position, playerTransform.position, out RaycastHit hitInfo))
            {
                if (hitInfo.collider.gameObject.tag == "Player") print("[] Player HAS BEEN SPOTTED!");
            }
        }
    }

    private void OnPlayerSpotted()
    {

    }
}
