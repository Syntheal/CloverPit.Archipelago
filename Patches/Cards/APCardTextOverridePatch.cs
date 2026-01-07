using HarmonyLib;

[HarmonyPatch(typeof(RunModifierScript))]
public static class APCardTextOverridePatch
{
    [HarmonyPrefix]
    [HarmonyPatch(nameof(RunModifierScript.TitleGet))]
    static bool TitleGet_Prefix(
        RunModifierScript.Identifier identifier,
        ref string __result)
    {
        if (identifier == RunModifierScript.Identifier.defaultModifier)
            return true;

        __result = "Not yet implemented";
        return false;
    }

    [HarmonyPrefix]
    [HarmonyPatch(nameof(RunModifierScript.DescriptionGet))]
    static bool DescriptionGet_Prefix(
        RunModifierScript.Identifier identifier,
        ref string __result)
    {
        if (identifier == RunModifierScript.Identifier.defaultModifier)
            return true;

        __result = "Planned to be implemented in future versions";
        return false;
    }
}
