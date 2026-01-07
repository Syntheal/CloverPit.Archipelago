using HarmonyLib;

[HarmonyPatch(typeof(CranePackScript))]
[HarmonyPatch("Update")]
static class APCranePackPatch
{
    static bool Prefix(CranePackScript __instance)
    {
        if (__instance.holder.activeSelf)
        {
            __instance.holder.SetActive(false);
        }

        return false;
    }
}
