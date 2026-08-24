using UnityEngine;

public class EndlessRoad : MonoBehaviour
{


    public GameObject[] roadSegments;
    public float tileSize = 100f;
    public float moveSpeed = 40f;
    public float howFarBack = 200f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Update()
    {
        foreach(GameObject part in roadSegments)
        {
            part.transform.position -= Vector3.forward * moveSpeed * Time.deltaTime;

            if (part.transform.position.z < howFarBack)
            {
                part.transform.position = new Vector3(0, 0, part.transform.position.z + roadSegments.Length * tileSize);
            }
        }
    }
}
