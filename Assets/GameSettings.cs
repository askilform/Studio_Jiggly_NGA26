using System;
using System.IO;
using UnityEngine;

public static class GameSettings
{
    public static float masterVolumeMult = 1f;
    public static bool motionBlur;
    public static bool EasyMode;
    public static int GraphicsPreset; // 0-1-2 = low, medium, high

    // JsonUtility can't serialize static fields, so values are copied through this class.
    [Serializable]
    private class SettingsData
    {
        public float masterVolumeMult = 1f;
        public bool motionBlur;
        public bool EasyMode;
        public int GraphicsPreset;
    }

    static string FilePath => Path.Combine(Application.persistentDataPath, "settings.json");

    // Runs automatically once at startup, before the first scene loads.
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Load()
    {
        try
        {
            if (!File.Exists(FilePath)) return;

            SettingsData data = JsonUtility.FromJson<SettingsData>(File.ReadAllText(FilePath));
            if (data == null) return;

            masterVolumeMult = Mathf.Clamp01(data.masterVolumeMult);
            motionBlur = data.motionBlur;
            EasyMode = data.EasyMode;
            GraphicsPreset = Mathf.Clamp(data.GraphicsPreset, 0, QualitySettings.names.Length - 1);
        }
        catch (Exception e)
        {
            Debug.LogWarning("Could not load settings, using defaults: " + e.Message);
        }
    }

    public static void Save()
    {
        try
        {
            SettingsData data = new SettingsData
            {
                masterVolumeMult = masterVolumeMult,
                motionBlur = motionBlur,
                EasyMode = EasyMode,
                GraphicsPreset = GraphicsPreset
            };

            File.WriteAllText(FilePath, JsonUtility.ToJson(data, true));
        }
        catch (Exception e)
        {
            Debug.LogError("Could not save settings: " + e.Message);
        }
    }
}