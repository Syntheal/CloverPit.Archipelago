using HarmonyLib;
using UnityEngine;

[HarmonyPatch(typeof(GameplayData), nameof(GameplayData.StoreLuckGet))]
public static class StoreLuckPatch
{
    static void Postfix(ref float __result)
    {
        __result += Mathf.CeilToInt(APState.LuckSaved/2) * 1.0f;
    }
}