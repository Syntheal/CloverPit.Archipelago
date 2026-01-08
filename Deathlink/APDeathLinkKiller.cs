using static APState;
using Archipelago.MultiClient.Net.BounceFeatures.DeathLink;

public static class APDeathLinkKiller
{
    public static void KillFromDeathLink(DeathLink deathLink)
    {
        if (GameplayMaster.GetGamePhase() == GameplayMaster.GamePhase.death)
            return;

        APDeathState.PendingDeathCause = APDeathCause.DeathLink;

        GameplayMaster.instance.DieTry(
            GameplayMaster.DeathStep.lookAtTrapdoor,
            callLastChanceCallback: false
        );
        APAbilityDefinePhoneStuffPatch.totalPickedTimes.Clear();

        APConsoleLog.Log($"DeathLink received from {deathLink.Source}: {deathLink.Cause}", APConsoleLineType.DeathLink);
    }
}
