using HarmonyLib;
using Panik;

[HarmonyPatch(typeof(Data.GameData), nameof(Data.GameData.RunModifier_OwnedCount_Get))]
class RunModifierOwnedCountPatch
{
    static bool Prefix(
        RunModifierScript.Identifier identifier,
        ref int __result)
    {
        if (identifier == RunModifierScript.Identifier.defaultModifier)
        {
            __result = -1;
            return false;
        }

        if (APState.UnlockedModifiers.Contains(identifier))
        {
            __result = -1;
        }
        else
        {
            __result = 0;
        }

        return false;
    }
}