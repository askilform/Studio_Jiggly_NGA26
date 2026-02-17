using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TextPopUp : MonoBehaviour
{
    private float lerpValue;
    private TextMeshProUGUI txt;
    public GameObject blackBackground;

    private void Start()
    {
        txt = GetComponent<TextMeshProUGUI>();
        txt.color = new Color(0, 0, 0, 0);
        blackBackground.SetActive(false);
    }

    public IEnumerator FlashText(string message, float duration, bool background)
    {
        if (background) blackBackground.SetActive(true);
        txt.text = message;
        txt.color = Color.white;

        yield return new WaitForSeconds(duration);

        txt.color = new Color(0, 0, 0, 0);
        blackBackground.SetActive(false);
    }
}
