using UnityEditor;
using UnityEngine;

public class OpenPauseMenu : MonoBehaviour
{
    public GameObject pausePrefab;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (GameObject.FindFirstObjectByType<PausePrefab>() == null) Instantiate(pausePrefab);
        }
    }
}
