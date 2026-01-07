using HarmonyLib;

[HarmonyPatch(typeof(MemoryPackDealUI))]
public static class APDisableMemoryPackDealPatch
{
    [HarmonyPrefix]
    [HarmonyPatch(nameof(MemoryPackDealUI.DealPropose))]
    static bool DealPropose_Prefix()
    {
        return false;
    }
}
