using UnityEngine;
using FMODUnity;
using UnityEditor.Build.Reporting;

public class WalkMan : MonoBehaviour
{
    public Animator anims;
    public StudioEventEmitter CasetteAudio;

    bool toggle;
    public void ToggleAudio()
    {
        if (!toggle) CasetteAudio.Play();
        else CasetteAudio.Stop();

        toggle = !toggle;
    }

    public void StopAudio()
    {
        CasetteAudio.Stop();
        toggle = false;
    }
}
