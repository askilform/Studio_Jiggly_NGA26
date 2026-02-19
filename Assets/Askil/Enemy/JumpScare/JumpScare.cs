using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpScare : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1);

        LevelMaster levelMaster = FindFirstObjectByType<LevelMaster>();
        levelMaster.playerMovementSc.transform.position = levelMaster.spawnLocation;
        

        // Destroy(transform.parent.gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
