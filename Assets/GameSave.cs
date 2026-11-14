using UnityEngine;
using System.IO;
using Unity.VisualScripting.FullSerializer;

public static class GameSave
{
    public static int LevelsAvailable = 1;

    [System.Serializable]
    public class SaveData
    {
        public int levelsAvailable;
    }

    static string path = Application.persistentDataPath + "/save.json";

    public static void Save()
    {
        SaveData data = new SaveData();
        data.levelsAvailable = LevelsAvailable;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);

        Debug.Log("Saved to: " + path);
    }

    public static void Load()
    {
        if (!File.Exists(path))
            return;

        string json = File.ReadAllText(path);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        LevelsAvailable = data.levelsAvailable;
    }
}