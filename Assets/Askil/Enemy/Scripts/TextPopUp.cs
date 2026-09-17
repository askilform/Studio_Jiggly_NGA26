using FMODUnity;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TextPopUp : MonoBehaviour
{
    private float lerpValue;
    private TextMeshProUGUI txt;
    public GameObject blackBackground;
    public StudioEventEmitter Sfx;

    private void Start()
    {
        txt = GetComponent<TextMeshProUGUI>();
        txt.color = new Color(0, 0, 0, 0);
        blackBackground.SetActive(false);
    }

    public void TextFlashEvent(string message0, bool SfxOn)
    {
        StartCoroutine(FlashText(message0, 1, false, SfxOn));
    }

    public IEnumerator FlashText(string message, float duration, bool background, bool SfxOn)
    {
        // if (background) blackBackground.SetActive(true);
        txt.text = message;
        txt.color = Color.white;
        if (SfxOn) Sfx.Play();

        yield return new WaitForSeconds(duration);

        txt.color = new Color(0, 0, 0, 0);
        blackBackground.SetActive(false);
    }
}
