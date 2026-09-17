using UnityEngine;

public class PausePrefab : MonoBehaviour
{
    bool movementEnabledBefore;

    void Start()
    {
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (GameObject.FindFirstObjectByType<PlayerMovement2>() != null)
        {
            PlayerMovement2 pM = GameObject.FindFirstObjectByType<PlayerMovement2>();

            movementEnabledBefore = pM.cameraMovementAllowed;
            pM.cameraMovementAllowed = false;
        }
    }

    public void ClosePauseMenu()
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Destroy(gameObject);
    }
}
