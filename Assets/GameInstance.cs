using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class GameInstance
{
    //SaveSystem
    public static string CurrentScene;
    public static string LastScene = "Empty";

    //Spawn
    public static Vector3 spawnLocationOverride;
    public static bool overrideStartSpawn;
}