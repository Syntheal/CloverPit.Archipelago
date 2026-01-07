using HarmonyLib;
using Panik;

[HarmonyPatch(typeof(PowerupScript))]
public static class PreventVanillaUnlocksPatch
{
    [HarmonyPrefix]
    [HarmonyPatch(nameof(PowerupScript.UnlockExt))]
    static bool Prefix(
        PowerupScript.Identifier powerupIdentifier,
        bool notifyPlayer,
        bool saveGame
    )
    {
        if (!notifyPlayer)
            return true;

        if (APItems.ManagedPowerups.Contains(powerupIdentifier) && !APState.IsGrantingItem)
        {
            Plugin.Log.LogDebug($"[AP] Prevented vanilla unlock for AP-managed item: {powerupIdentifier}");
            return false;
        }

        if (IsSkeletonPiece(powerupIdentifier) && APState.UnlockedDrawers > 0)
        {
            Plugin.Log.LogDebug($"[AP] Prevented vanilla unlock for skeleton piece: {powerupIdentifier}");
            return false;
        }

        return true;
    }

    private static bool IsSkeletonPiece(PowerupScript.Identifier powerupIdentifier)
    {
        return powerupIdentifier == PowerupScript.Identifier.Skeleton_Arm1 ||
               powerupIdentifier == PowerupScript.Identifier.Skeleton_Arm2 ||
               powerupIdentifier == PowerupScript.Identifier.Skeleton_Leg1 ||
               powerupIdentifier == PowerupScript.Identifier.Skeleton_Leg2;
    }
}
