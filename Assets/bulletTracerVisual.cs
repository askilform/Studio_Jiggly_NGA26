using UnityEngine;

public class bulletTracerVisual : MonoBehaviour
{

    float lifetimenow = 0f;
    float lifetimeMax = 1f;

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
