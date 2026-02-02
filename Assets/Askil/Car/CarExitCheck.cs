using UnityEngine;

public class CarExitCheck : MonoBehaviour
{
    public bool isColliding;

    private void OnTriggerEnter(Collider other)
    {
        isColliding = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isColliding = false;
    }
}
