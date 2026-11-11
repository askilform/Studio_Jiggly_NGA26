using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class GameInstance
{
    // Local saves
    public static int[] savedWeaponIds;
    public static bool gunShowcase;

    // For TextScene;
    public static string sceneAfterTextScene;
    public static string stringForTextScene;

    public static void ClearSaves()
    {
        savedWeaponIds = null;
        gunShowcase = false;
    }
}