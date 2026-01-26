using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    public Transform meshTransform;

    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        print (playerTransform.name);
    }

    private void OnTrigger(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

        }
    }
}
