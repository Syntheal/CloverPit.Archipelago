using HarmonyLib;
using Panik;
using System.Collections.Generic;

public static class APSkeletonProgression
{
    public static readonly PowerupScript.Identifier[] Order =
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
        if (APState.UnlockedSkeleton >= Order.Length)
            return;

        var __instance = Data.game;

        var unlocked = (List<PowerupScript.Identifier>)
            AccessTools.Field(typeof(Data.GameData), "unlockedPowerups").GetValue(__instance);
        var locked = (List<PowerupScript.Identifier>)
            AccessTools.Field(typeof(Data.GameData), "lockedPowerups").GetValue(__instance);

        foreach (var id in Order)
        {
            if (!APState.UnlockedPowerups.Contains(id))
            {
                APState.UnlockedSkeleton++;
                APState.UnlockedPowerups.Add(id);

                PowerupUnlocker.Unlock(id);

                if (!unlocked.Contains(id)) unlocked.Add(id);
                locked.Remove(id);

                var powerupObj = PowerupScript.GetPowerup_Quick(id);
                powerupObj?.MaterialColorReset();

                APSaveManager.Save();

                Plugin.Log.LogInfo($"[AP] Progressive Skeleton unlocked: {id}");
                return;
            }
        }

        Plugin.Log.LogInfo("[AP] All skeleton parts already unlocked");
    }
}
