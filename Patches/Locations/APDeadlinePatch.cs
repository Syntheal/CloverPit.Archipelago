using HarmonyLib;
using Panik;

[HarmonyPatch(typeof(GameplayData))]
public static class APDeadlinePatch
{
    private static bool NewRun = true;
    [HarmonyPostfix]
    [HarmonyPatch(nameof(GameplayData.Stats_DeadlinesCompleted_Add))]
    static void Postfix()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        int actualCompleted =
            (int)GameplayData.Stats_DeadlinesCompleted_Get();

        if (actualCompleted == 1)
            NewRun = true;

        if (APState.deadlineGoal == actualCompleted && NewRun)
        {
            APState.deadlinesCompleted++;
            NewRun = false;
        }

        while (APState.DeadlinesSentToAP < actualCompleted &&
               APState.DeadlinesSentToAP < 10)
        {
            APState.DeadlinesSentToAP++;

            long locationId =
                APLocations.GetDeadlineLocation(
                    APState.DeadlinesSentToAP
                );

            APLocationManager.Complete(locationId);

            APSaveManager.Save();

            Plugin.Log.LogInfo(
                $"[AP] Deadline {APState.DeadlinesSentToAP} completed"
            );
        }

        if (APState.goalType == "deadline" &&
            !APState.goalCompleted &&
            APState.deadlinesCompleted >= APState.deadlineAmount)
        {
            APState.goalCompleted = true;
            APClient.SendGoalCompletion();
            APSaveManager.Save();

            Plugin.Log.LogInfo("[AP] Deadline goal completed");
        }
    }
}
