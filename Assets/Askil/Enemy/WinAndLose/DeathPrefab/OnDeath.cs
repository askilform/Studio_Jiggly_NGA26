using System.Collections;
using UnityEngine;

public class OnDeath : MonoBehaviour
{
    private IEnumerator Start()
    {
        TextPopUp textpopSC = GameObject.FindFirstObjectByType<TextPopUp>();
        LevelMaster levelMaster = GameObject.FindFirstObjectByType<LevelMaster>();

        GameInstance.ClearSaves();
        yield return new WaitForSeconds(2);
        textpopSC.StartCoroutine(textpopSC.FlashText("it's done.", 5, false));
        yield return new WaitForSeconds(3);
        levelMaster.ChanceScene("MainMenu");
    }
}
