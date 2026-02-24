using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMaster : MonoBehaviour
{
    [Header("Enemy")]

    [Header("Music")]
    public AudioSource ChillMusic;
    public AudioSource DangerMusic;
    public AudioSource ChaseMusic;

    [Header("Other")]
    public CanvasGroup canvasGroup;
    public bool PlayerInScene = true;
    private bool sceneIn;

    [Header("Dont Assign")]
    [SerializeField] public PlayerMovement2 playerMovementSc;
    public GameObject playerBody;
    private float ogPlayerSpeed;
    public bool playerCrouching;
    public bool playerRunning;
    public bool playerInDangerArea;
    public bool playerSprinting;
    public WeaponPart[] weaponPartScripts;

    private void Start()
    {
        PlayerInScene = (FindFirstObjectByType<PlayerMovement2>() != null);

        sceneIn = true;
        canvasGroup.alpha = 1.0f;

        print("LastScene; " + GameInstance.LastScene);
        GameInstance.CurrentScene = SceneManager.GetActiveScene().name;
        print("CurrentScene; " + GameInstance.CurrentScene);

        if (GameInstance.overrideStartSpawn) playerBody.transform.position = GameInstance.spawnLocationOverride;

        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (PlayerInScene)
        {
            playerMovementSc = FindFirstObjectByType<PlayerMovement2>();
            ogPlayerSpeed = playerMovementSc.moveSpeed;

            // Find Weaponparts and  destroy them if player has them
            weaponPartScripts = GameObject.FindObjectsByType<WeaponPart>(FindObjectsSortMode.None);
            foreach (WeaponPart weaponPart in weaponPartScripts)
            {
                if (GameInstance.savedWeaponIds.Contains(weaponPart.id)) Destroy(weaponPart.transform.gameObject);
            }
        } 
    }

    private void FixedUpdate()
    {
        if (PlayerInScene)
        {
            playerCrouching = playerMovementSc.isCrouching;
            playerRunning = playerMovementSc.moveSpeed > (ogPlayerSpeed * playerMovementSc.sprintSpeed) - 0.5f;
        }

        if (canvasGroup.alpha < 1.1 || canvasGroup.alpha < 0) canvasGroup.alpha += sceneIn ? -0.05f : 0.1f;
    }

    public void ChanceScene(string sceneName) { StartCoroutine(ChanceSceneCoroutine(sceneName)); }

    public IEnumerator ChanceSceneCoroutine(string sceneName)
    {
        GameInstance.LastScene = SceneManager.GetActiveScene().name;
        GameInstance.overrideStartSpawn = false;

        sceneIn = false;
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(sceneName);
    }

    public void UpdateSpawnPoint(GameObject ObjectWithSpawnLocation)
    {
        GameInstance.overrideStartSpawn = true;
        GameInstance.spawnLocationOverride = ObjectWithSpawnLocation.transform.position;
        print("SetNewSpawnLocation");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
