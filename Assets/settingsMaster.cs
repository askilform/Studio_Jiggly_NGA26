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

        volumeBar.SetValueWithoutNotify(GameSettings.masterVolumeMult);
        MotionBlurToggle.SetIsOnWithoutNotify(GameSettings.motionBlur);
        EasyModeToggle.SetIsOnWithoutNotify(GameSettings.EasyMode);
        GraphicsPreset.SetValueWithoutNotify(GameSettings.GraphicsPreset);

        masterbusAudio.setVolume(GameSettings.masterVolumeMult);
        QualitySettings.SetQualityLevel(GameSettings.GraphicsPreset, true);
    }

    public void onValueChanged()
    {
        GameSettings.masterVolumeMult = volumeBar.value;
        GameSettings.motionBlur = MotionBlurToggle.isOn;
        GameSettings.EasyMode = EasyModeToggle.isOn;
        GameSettings.GraphicsPreset = GraphicsPreset.value;

        FMOD.RESULT result = masterbusAudio.setVolume(volumeBar.value);
        QualitySettings.SetQualityLevel(GraphicsPreset.value, true);

        Debug.Log("FMOD result: " + result);
    }

    // Write to disk when leaving the settings scene/menu or quitting,
    // instead of on every slider tick.
    private void OnDisable()
    {
        GameSettings.Save();
    }
}