using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class GameInstance
{
    public static int[] savedWeaponIds;
    public static bool gunShowcase;

    public static void ClearSaves()
    {
        // savedWeaponIds = null; Fix Later lol...
        gunShowcase = false;
    }
}