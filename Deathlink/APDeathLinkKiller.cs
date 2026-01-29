using Archipelago.MultiClient.Net.BounceFeatures.DeathLink;
using Panik;
using System.Linq;
using static APState;
using System.Reflection;
using System.Diagnostics;

public static class APDeathLinkKiller
{
    private static bool HasAnkhEquipped()
    {
        return PowerupScript.IsEquipped(PowerupScript.Identifier.Ankh);
    }

    public static void KillFromDeathLink(DeathLink deathLink)
    {
        if (GameplayMaster.GetGamePhase() == GameplayMaster.GamePhase.death)
            return;

        if (HasAnkhEquipped())
        {
            typeof(PowerupScript).GetMethod("Trigger_Ankh", BindingFlags.NonPublic | BindingFlags.Static)?.Invoke(null, null);
            APConsoleLog.Log(
                $"DeathLink received from {deathLink.Source} but Ankh activated.",
                APConsoleLineType.DeathLink
            );
            return;
        }

        APDeathState.PendingDeathCause = APDeathCause.DeathLink;

        GameplayMaster.instance.DieTry(
            GameplayMaster.DeathStep.lookAtTrapdoor,
            callLastChanceCallback: false
        );

        APAbilityDefinePhoneStuffPatch.totalPickedTimes.Clear();

        APConsoleLog.Log(
            $"DeathLink received from {deathLink.Source}: {deathLink.Cause}",
            APConsoleLineType.DeathLink
        );
    }
}
