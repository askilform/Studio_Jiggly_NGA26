using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class toggleMotionBlur : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Volume globalVolumeHere = GetComponent<Volume>();

        MotionBlur mb;
        if (globalVolumeHere.profile.TryGet<MotionBlur>(out mb)) mb.active = GameSettings.motionBlur;
    }
}
