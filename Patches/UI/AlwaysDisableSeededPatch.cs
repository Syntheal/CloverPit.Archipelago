using HarmonyLib;

[HarmonyPatch(typeof(SeedMenuScript))]
public static class AlwaysDisableSeededPatch
{
    [HarmonyPostfix]
    [HarmonyPatch(nameof(SeedMenuScript.IsEnabled))]
    static bool Prefix()
    {
        return false;
    }
}
