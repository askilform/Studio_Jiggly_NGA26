using System.Collections;
using UnityEngine;

public class RotateToPlayer : MonoBehaviour
{
    GameObject target;
    bool rotate = true;

    public float StopRotatingAfterTime;

    private IEnumerator Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");

        yield return new WaitForSeconds(StopRotatingAfterTime);
        rotate = false;

    }
    void Update()
    {
        if (rotate)
        {
            Vector3 direction = target.transform.position - transform.position;
            direction.y = 0f;

            if (direction.sqrMagnitude > 0.001f)
                transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
