using HarmonyLib;
using System.Linq;
using Panik;

[HarmonyPatch(typeof(Data.GameData))]
public static class APUnlockedModifiersPatch
{
    [HarmonyPrefix]
    [HarmonyPatch(nameof(Data.GameData.RunModifier_UnlockedTimes_Get))]
    static bool RunModifier_UnlockedTimes_Get_Prefix(
        RunModifierScript.Identifier identifier,
        ref int __result)
    {
        if (APState.UnlockedModifiers.Contains(identifier))
        {
            __result = 1;
        }
        else
        {
            __result = 0;
        }

        return false;
    }
}
