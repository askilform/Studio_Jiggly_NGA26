using UnityEngine;

public class bulletTracerVisual : MonoBehaviour
{

    float lifetimenow = 0f;
    public float lifetimeMax = 0.8f;

    void Start()
    {
        
    }

    void Update()
    {
        lifetimenow += Time.deltaTime;

        if ( lifetimenow > lifetimeMax)
        {
            Destroy(gameObject);
        }
    }
}
