using HarmonyLib;
using Panik;

[HarmonyPatch(typeof(PowerupScript))]
public static class SkeletonIsUnlockedPatch
{
    [HarmonyPrefix]
    [HarmonyPatch(nameof(PowerupScript.IsUnlocked))]
    static bool Prefix(PowerupScript.Identifier powerupIdentifier, ref bool __result)
    {
        if (powerupIdentifier == PowerupScript.Identifier.Skeleton_Head ||
            powerupIdentifier == PowerupScript.Identifier.Skeleton_Arm1 ||
            powerupIdentifier == PowerupScript.Identifier.Skeleton_Arm2 ||
            powerupIdentifier == PowerupScript.Identifier.Skeleton_Leg1 ||
            powerupIdentifier == PowerupScript.Identifier.Skeleton_Leg2)
        {
            __result = APState.UnlockedPowerups.Contains(powerupIdentifier);
            return false;
        }

        return true;
    }
}
