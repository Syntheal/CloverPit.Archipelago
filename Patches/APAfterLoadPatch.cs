using HarmonyLib;
using Panik;
using System.Collections.Generic;

[HarmonyPatch(typeof(Data.GameData))]
public static class APAfterLoadPatch
{
    [HarmonyPostfix]
    [HarmonyPatch("_LockedPowerups_LoadPrepare")]
    static void Postfix(Data.GameData __instance)
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        var unlocked =
            (List<PowerupScript.Identifier>)
            AccessTools.Field(typeof(Data.GameData), "unlockedPowerups")
            .GetValue(__instance);

        var locked =
            (List<PowerupScript.Identifier>)
            AccessTools.Field(typeof(Data.GameData), "lockedPowerups")
            .GetValue(__instance);

        unlocked.Clear();
        locked.Clear();

        foreach (var id in APItems.ManagedPowerups)
            locked.Add(id);

        foreach (var id in APItems.StartingPowerups)
        {
            unlocked.Add(id);
            locked.Remove(id);
        }

        foreach (var id in APState.UnlockedPowerups)
        {
            if (!unlocked.Contains(id))
                unlocked.Add(id);

            locked.Remove(id);
        }

        Plugin.Log.LogInfo("[AP] AP powerup state enforced after load");
    }
}
