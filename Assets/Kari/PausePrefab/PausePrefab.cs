using System.Collections;
using UnityEngine;

public class PausePrefab : MonoBehaviour
{
    bool movementEnabledBefore;
    PlayerMovement2 pM;

    public Animator animator;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.2f);

        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (GameObject.FindFirstObjectByType<PlayerMovement2>() != null)
        {
            pM = GameObject.FindFirstObjectByType<PlayerMovement2>();
            pM.walkingAudioInstance.setVolume((0));

            movementEnabledBefore = pM.cameraMovementAllowed;
            pM.cameraMovementAllowed = false;
        }
    }

    private void Update()
    {
        pM.walkingAudioInstance.setVolume((0));

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ClosePauseMenu();
        }
    }

    public void ClosePauseMenu()
    {
        StartCoroutine(ClosePauseMenuCoroutine());
    }

    IEnumerator ClosePauseMenuCoroutine()
    {
        print("Tried To UnPause");

        Time.timeScale = 1f;

        PlayerMovement2 pM = GameObject.FindFirstObjectByType<PlayerMovement2>();

        pM.cameraMovementAllowed = movementEnabledBefore;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        print("Finished UnPause" + Time.timeScale);

        animator.SetTrigger("FadeOut");
        yield return new WaitForSecondsRealtime(0.2f);

        Destroy(gameObject);
    }

    public void GoToScene(string SceneToGoTo)
    {
        Time.timeScale = 1f;
        GameObject.FindFirstObjectByType<LevelMaster>().ChanceScene(SceneToGoTo);
    }
}
