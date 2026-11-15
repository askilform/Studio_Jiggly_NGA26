using UnityEngine;
using UnityEngine.Events;

public class SimpleEvent : MonoBehaviour
{
    public UnityEvent eventToRun;

    public void RunEvent()
    {
        eventToRun.Invoke();
    }
}
