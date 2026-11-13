using TMPro;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class settingsMaster : MonoBehaviour
{
    public Scrollbar volumeBar;
    public Toggle MotionBlurToggle;
    public Toggle EasyModeToggle;
    public TMP_Dropdown GraphicsPreset;

    private void Start()
    {
        volumeBar.value = GameSettings.masterVolumeMult;
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
    }
}
