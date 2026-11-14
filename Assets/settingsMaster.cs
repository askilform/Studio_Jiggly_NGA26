using FMOD.Studio;
using FMODUnity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class settingsMaster : MonoBehaviour
{
    public Scrollbar volumeBar;
    public Toggle MotionBlurToggle;
    public Toggle EasyModeToggle;
    public TMP_Dropdown GraphicsPreset;

    Bus masterbusAudio;

    private void Start()
    {
        masterbusAudio = RuntimeManager.GetBus("bus:/");
        Debug.Log("Bus valid: " + masterbusAudio.isValid());

        volumeBar.value = GameSettings.masterVolumeMult;
        masterbusAudio.setVolume(GameSettings.masterVolumeMult);

        MotionBlurToggle.isOn = GameSettings.motionBlur;
        EasyModeToggle.isOn = GameSettings.EasyMode;
        GraphicsPreset.value = GameSettings.GraphicsPreset;
    }

    public void onValueChanged()
    {
        GameSettings.masterVolumeMult = volumeBar.value;
        GameSettings.motionBlur = MotionBlurToggle.isOn;
        GameSettings.EasyMode = EasyModeToggle.isOn;
        GameSettings.GraphicsPreset = GraphicsPreset.value;

        masterbusAudio.setVolume(volumeBar.value);
        FMOD.RESULT result = masterbusAudio.setVolume(volumeBar.value);
        Debug.Log("FMOD result: " + result);
    }
}