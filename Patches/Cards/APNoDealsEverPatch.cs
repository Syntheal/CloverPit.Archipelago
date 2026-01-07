using HarmonyLib;

[HarmonyPatch(typeof(GameplayData))]
[HarmonyPatch(nameof(GameplayData.RunModifier_DealIsAvailable_Set))]
static class APNoDealsEverPatch
{
    static bool Prefix(bool value)
    {
        return value == false;
    }
}
