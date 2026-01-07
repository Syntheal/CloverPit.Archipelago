using HarmonyLib;
using static APState;

[HarmonyPatch(typeof(MainMenuScript))]
internal static class APRestartPatch
{
    [HarmonyPrefix]
    [HarmonyPatch("_RestartYes")]
    private static void Prefix_RestartYes()
    {
        APDeathState.PendingDeathCause = APDeathCause.Restart;
    }
}