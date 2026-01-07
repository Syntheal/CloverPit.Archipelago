using HarmonyLib;
using UnityEngine;

[HarmonyPatch(typeof(DrawersScript))]
[HarmonyPatch(nameof(DrawersScript.GetDrawersUnlockedNum))]
static class APDrawerCountPatch
{
    static bool Prefix(ref int __result)
    {
        if (!APState.IsConnected)
            return true;

        __result = Mathf.Clamp(APState.KeysCompleted, 0, 4);
        return false;
    }
}
