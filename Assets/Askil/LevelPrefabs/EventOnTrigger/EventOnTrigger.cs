using UnityEngine;
using UnityEngine.Events;

public class EventOnTrigger : MonoBehaviour
{
    public UnityEvent onTriggerEvent;
    public bool destroyAfterEvent = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            onTriggerEvent.Invoke();

            if (destroyAfterEvent) Destroy(transform.parent.gameObject);
        }
    }
}
