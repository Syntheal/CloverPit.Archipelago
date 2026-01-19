public static class APSkeletonProgression
{
    private static readonly PowerupScript.Identifier[] Order =
    {
        PowerupScript.Identifier.Skeleton_Head,
        PowerupScript.Identifier.Skeleton_Arm1,
        PowerupScript.Identifier.Skeleton_Arm2,
        PowerupScript.Identifier.Skeleton_Leg1,
        PowerupScript.Identifier.Skeleton_Leg2,
    };

    public static void GrantNext()
    {
        APState.SkeletonsReceived++;
        if (APState.SkeletonsReceived <= APState.UnlockedSkeleton)
            return;
        foreach (var id in Order)
        {
            APState.UnlockedSkeleton++;
            if (!APState.UnlockedPowerups.Contains(id))
            {
                PowerupUnlocker.Unlock(id);
                APState.UnlockedPowerups.Add(id);
                APSaveManager.Save();

                Plugin.Log.LogInfo($"[AP] Progressive Skeleton unlocked: {id}");
                return;
            }
        }

        Plugin.Log.LogInfo("[AP] All skeleton parts already unlocked");
    }
}
