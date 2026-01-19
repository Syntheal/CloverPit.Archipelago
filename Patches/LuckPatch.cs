using HarmonyLib;

[HarmonyPatch(typeof(GameplayData), nameof(GameplayData.LuckGet))]
public static class LuckPatch
{
    static void Postfix(ref float __result)
    {
        __result += APState.LuckSaved * 1.0f;
    }
}