using UnityEngine;
using UnityEngine.SceneManagement;

public class TextScene : MonoBehaviour
{
    public void ContinueToScene()
    {
        FindFirstObjectByType<LevelMaster>().ChanceScene(GameInstance.sceneAfterTextScene);
        GameInstance.sceneAfterTextScene = null;
    }
}
