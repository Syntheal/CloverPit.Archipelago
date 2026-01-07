using HarmonyLib;

[HarmonyPatch(typeof(DrawersScript))]
public static class DrawerSkeletonFilterPatch
{
    [HarmonyPostfix]
    [HarmonyPatch("Update")]
    static void Postfix()
    {
        if (!APState.IsConnected)
            return;

        for (int i = 0; i < DrawersScript.MAX_DRAWERS; i++)
        {
            var powerup = PowerupScript.array_InDrawer[i];
            if (powerup == null)
                continue;

            if (powerup.category != PowerupScript.Category.skeleton)
                continue;

            if (APState.UnlockedPowerups.Contains(powerup.identifier))
                continue;

            Plugin.Log.LogDebug(
                $"[AP] Ejecting locked skeleton {powerup.identifier} from drawer {i}"
            );

            PowerupScript.ThrowAwayCanTriggerEffects_Set(false);
            PowerupScript.SuppressThrowAwaySound();
            PowerupScript.SuppressThrowAwayAnimation();

            PowerupScript.ThrowAway(powerup.identifier, isInitializationCall: false);

            PowerupScript.ThrowAwayCanTriggerEffects_Set(true);
        }
    }
}
