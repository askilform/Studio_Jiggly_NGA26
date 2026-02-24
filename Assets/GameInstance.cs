using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class GameInstance
{
    public static int[] savedWeaponIds;

    public static void ClearSaves()
    {
        savedWeaponIds = null;
    }
}