using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class PostProcessFade : MonoBehaviour
{
    Volume volume;
    bool fadeIn;

    private void Start()
    {
        volume = GetComponent<Volume>();
    }

    public void fadeVolume(bool isIn)
    {
        volume.weight = isIn ? 0.05f : 0.95f;
        fadeIn = isIn;
    }

    private void FixedUpdate()
    {
        if (fadeIn) volume.weight += 0.01f;
        if (!fadeIn) volume.weight -= 0.01f;

        Mathf.Clamp(volume.weight,0,1);
    }
}
