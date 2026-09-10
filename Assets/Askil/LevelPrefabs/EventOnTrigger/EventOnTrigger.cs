using UnityEngine;
using UnityEngine.Events;

public class EventOnTrigger : MonoBehaviour
{
    public UnityEvent onTriggerEvent;
    public bool destroyAfterEvent = true;

    public bool InCar;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player" && !InCar)
        {
            onTriggerEvent.Invoke();

            if (destroyAfterEvent) Destroy(transform.parent.gameObject);
        }

        if (other.transform.tag == "Car" && InCar)
        {
            onTriggerEvent.Invoke();

            if (destroyAfterEvent) Destroy(transform.parent.gameObject);
        }
    }
}
