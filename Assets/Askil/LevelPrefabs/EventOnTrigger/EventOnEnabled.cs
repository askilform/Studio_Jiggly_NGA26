using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EventOnEnabled : MonoBehaviour
{
    public UnityEvent WhenEnabled;

    private void OnEnable()
    {
        StartCoroutine(WaitAndRunEvent());
    }

    IEnumerator WaitAndRunEvent()
    {
        Debug.Log(">>> BEFORE EVENT");

        yield return null;

        WhenEnabled.Invoke();

        Debug.Log(">>> AFTER EVENT");
    }
}
