using HarmonyLib;

[HarmonyPatch(typeof(DoorScript))]
public static class APDoorOpenPatch
{
    [HarmonyPostfix]
    [HarmonyPatch(nameof(DoorScript.OpenTry))]
    static void Postfix(bool __result)
    {
        if (!__result || !APState.IsConnected)
            return;

        if (APState.DoorKeyUsed)
            return;

        APState.DoorKeyUsed = true;

        bool isGoodEnding = GameplayData.NineNineNine_IsTime();

        if (APState.goalType == "key")
        {
            bool matchesRequiredEnding =
                (isGoodEnding && APState.RequiredKeyEnding == 1) ||
                (!isGoodEnding && APState.RequiredKeyEnding == 0);

            if (matchesRequiredEnding)
            {
                APLocationManager.Complete(APLocations.GOAL_COMPLETE);
                APState.goalCompleted = true;
            }
        }

        APSaveManager.Save();
    }
}
