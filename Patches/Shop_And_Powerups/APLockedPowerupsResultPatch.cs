using HarmonyLib;
using Panik;
using System.Collections.Generic;
using System.Linq;

[HarmonyPatch(typeof(Data.GameData))]
public static class APLockedPowerupsResultPatch
{
    static readonly PowerupScript.Identifier FILLER =
        PowerupScript.Identifier.FortuneCookie;

    [HarmonyPostfix]
    [HarmonyPatch("_LockedPowerupsResultingList_Compute")]
    static void Postfix(Data.GameData __instance)
    {
        if (!APState.IsConnected)
            return;

        var field = AccessTools.Field(
            typeof(Data.GameData),
            "lockedPowerups_ResultingList"
        );

        var list = field?.GetValue(__instance)
            as List<PowerupScript.Identifier>;

        if (list == null)
            return;

        if (list.Any(id => id != FILLER))
        {
            list.RemoveAll(id => id == FILLER);
            return;
        }

        list.Clear();
        list.Add(FILLER);

        Plugin.Log.LogWarning(
            "[AP] Shop contains only filler"
        );
    }
}
