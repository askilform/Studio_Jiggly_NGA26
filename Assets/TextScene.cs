using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextScene : MonoBehaviour
{
    public TextMeshProUGUI text;

    private void Start()
    {
        if (GameInstance.stringForTextScene == null) text.text = "--TextMissing--";

        else text.text = GameInstance.stringForTextScene;
    }

    public void ContinueToScene()
    {
        FindFirstObjectByType<LevelMaster>().ChanceScene(GameInstance.sceneAfterTextScene);
        GameInstance.sceneAfterTextScene = null;
        GameInstance.stringForTextScene = null;
    }
}
