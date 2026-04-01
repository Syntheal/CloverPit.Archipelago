using HarmonyLib;

[HarmonyPatch(typeof(GameplayData), nameof(GameplayData.DeadlineReward_CloverTickets))]
public static class DeadlineRewardPatch
{
    [HarmonyPostfix]
    private static void Postfix(ref long __result)
    {
        __result += APState.BonusDeadline;
    }
}
