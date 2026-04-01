using HarmonyLib;
using Panik;
using System;

[HarmonyPatch(typeof(Data.GameData), nameof(Data.GameData.RunModifier_UnlockedTotalNumber))]
public static class AP_UnlockedTotalNumber_Patch
{
    static bool Prefix(ref int __result)
    {
        __result = Math.Max(0, APState.UnlockedModifiers.Count - 1);
        return false;
    }
}
