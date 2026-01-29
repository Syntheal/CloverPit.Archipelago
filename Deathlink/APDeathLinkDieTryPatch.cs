using HarmonyLib;
using static APState;

[HarmonyPatch(typeof(GameplayMaster))]
internal static class APDeathLinkDieTryPatch
{
    [HarmonyPostfix]
    [HarmonyPatch(nameof(GameplayMaster.DieTry))]
    private static void Postfix_DieTry(bool __result)
    {
        if (!IsConnected || !APSaveLoaded)
        {
            APDeathState.PendingDeathCause = APDeathCause.None;
            return;
        }
        APAbilityDefinePhoneStuffPatch.totalPickedTimes.Clear();
        if (!Deathlink)
        {
            APDeathState.PendingDeathCause = APDeathCause.None;
            return;
        }
        switch (APDeathState.PendingDeathCause)
        {
            case APDeathCause.DeathLink:
                break;

            case APDeathCause.Restart:
                if (DeathLinkRestart)
                {
                    APClient.SendDeathLink("Restarted run");
                }
                break;

            case APDeathCause.Gameplay:
            case APDeathCause.None:
            default:
                APClient.SendDeathLink("Did not meet the deadline goal");
                break;
        }
        APDeathState.PendingDeathCause = APDeathCause.None;
    }
}
