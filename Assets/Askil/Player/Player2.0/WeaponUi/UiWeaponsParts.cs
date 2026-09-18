using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiWeaponsParts : MonoBehaviour
{
    public Weapon_Builder weaponbBuilderSc;
    public List<RawImage> partImages = new List<RawImage>();
    public Color notCollected;
    public Color collected;

    bool started;
    CanvasGroup canvasGroup;
    private void Start()
    {
        foreach (var part in partImages)
        {
            part.color = notCollected;
        }

        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void colorUiPart(int index)
    {
        partImages[index - 1].color = collected;

        if (!started) started = true;
    }

    private void FixedUpdate()
    {
        if (started && canvasGroup.alpha != 1)
        {
            canvasGroup.alpha += 0.01f;
        }
    }
}
