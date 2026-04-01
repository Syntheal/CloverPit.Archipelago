using HarmonyLib;
using Panik;

[HarmonyPatch(typeof(Data.GameData), nameof(Data.GameData.DesiredFoilLevelGet))]
class DesiredFoilLevelPatch
{
    static bool Prefix(
        RunModifierScript.Identifier identifier,
        ref int __result)
    {
        __result = 2;
        return false;
    }
}