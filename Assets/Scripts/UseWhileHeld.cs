using UnityEngine;
using UnityEngine.Events;

public class UseWhileHeld : MonoBehaviour
{

    public UnityEvent UseWhileHeldEvent;

    public void triggerUseWhileHeld()
    {
        UseWhileHeldEvent.Invoke();
    }
}
