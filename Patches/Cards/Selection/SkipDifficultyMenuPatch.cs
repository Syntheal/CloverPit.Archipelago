using HarmonyLib;
using System.Reflection;

[HarmonyPatch(typeof(DeckBoxUI), "OpenDifficultySelectionMenu")]
public static class SkipDifficultyMenuPatch
{
    static bool Prefix(object __instance)
    {
        if (!APState.HardMode)
        {
            var method = AccessTools.Method(__instance.GetType(), "SelectionMenu_PickNormal");
            method?.Invoke(__instance, null);
        } else
        {
            var method = AccessTools.Method(__instance.GetType(), "SelectionMenu_PickHardcore");
            method?.Invoke(__instance, null);
        }
        return false;
    }
}