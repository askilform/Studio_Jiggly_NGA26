using UnityEngine;

public class EnableBasedOnProgress : MonoBehaviour
{
    public int progressRequired;
    private void Start()
    {
        gameObject.SetActive(progressRequired <= GameSave.LevelsAvailable ? true : false);
    }
}
