using static APState;

public static class APDeathLinkKiller
{
    public static void KillFromDeathLink()
    {
        if (GameplayMaster.GetGamePhase() == GameplayMaster.GamePhase.death)
            return;

        if (APDeathState.DeathLinkKillPending)
            return;

        APDeathState.DeathLinkKillPending = true;
        APDeathState.PendingDeathCause = APDeathCause.DeathLink;

        GameplayMaster.instance.DieTry(
            GameplayMaster.DeathStep.lookAtTrapdoor,
            callLastChanceCallback: false
        );
    }
}
