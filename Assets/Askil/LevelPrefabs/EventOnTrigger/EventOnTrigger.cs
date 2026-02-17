using UnityEngine;
using UnityEngine.Events;

public class EventOnTrigger : MonoBehaviour
{
    public UnityEvent onTriggerEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            onTriggerEvent.Invoke();
            Destroy(transform.parent.gameObject);
        }
           
    }

}
