using UnityEngine;

public class BlobbenScript : MonoBehaviour
{

    GameObject player;

    private Rigidbody rb;
    public float CrawlSpeed = 20f;

    private Vector3 lookAtPoint = Vector3.zero;

    public float lookLerp = 0.2f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        lookAtPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");

        }


        //CRAWL TO PLAYER
        Vector3 directionToCrawl = new Vector3(1f, 0, 0);
        directionToCrawl = (player.transform.position - gameObject.transform.position).normalized;

        directionToCrawl = new Vector3(directionToCrawl.x, -2, directionToCrawl.z);


        rb.linearVelocity = directionToCrawl * CrawlSpeed;
        
        
        
        //LOOK AT A POINT THAT SLOWLY MOVES TOWARDS PLAYER
        Vector3 looktarget = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        
        lookAtPoint = Vector3.Lerp(lookAtPoint, looktarget, lookLerp * Time.deltaTime);

        transform.LookAt(lookAtPoint);


    }
}
