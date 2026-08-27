using UnityEngine;
using UnityEngine.Events;

public class EventOnEnabled : MonoBehaviour
{
    public UnityEvent OnEnabled;

    private void OnEnable()
    {
        OnEnabled.Invoke();
    }
}
