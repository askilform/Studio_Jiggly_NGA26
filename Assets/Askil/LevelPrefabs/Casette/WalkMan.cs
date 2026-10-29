using UnityEngine;
using FMODUnity;
using UnityEditor.Build.Reporting;

public class WalkMan : MonoBehaviour
{
    public Animator ButtonAnims;
    public Animator TapeWheelAnims;
    public StudioEventEmitter CasetteAudio;
    public StudioEventEmitter CasetteStop;
    bool Playing;
    public void ToggleAudio()
    {
        if (!Playing)
        {
            CasetteAudio.Play();
            ButtonAnims.SetTrigger("Press");
            TapeWheelAnims.SetBool("Playing", true);
            Playing = true;
        }

        else StopAudio();
    }

    public void StopAudio()
    {
        CasetteAudio.Stop();
        Playing = false;

        ButtonAnims.SetTrigger("Press");
        TapeWheelAnims.SetBool("Playing", false);

        CasetteStop.Play();
    }

    public void onDrop()
    {
        if (Playing)
        {
            StopAudio();
        }
    }
}
