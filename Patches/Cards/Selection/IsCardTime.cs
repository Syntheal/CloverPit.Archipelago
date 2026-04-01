using HarmonyLib;
using Panik;
using System;

[HarmonyPatch(typeof(DeckBoxScript), nameof(DeckBoxScript.ItsMemoryCardTime))]
public static class IsCardTime
{
    static bool Prefix(ref bool __result)
    {
        if (APState.UnlockedModifiers.Count <= 0){
            __result = false;
        }
        else {
            __result = true;
        }
        
        return false;
    }
}
