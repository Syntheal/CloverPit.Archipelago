using HarmonyLib;

[HarmonyPatch(typeof(RewardBoxScript))]
[HarmonyPatch(nameof(RewardBoxScript.Open))]
static class APRewardBoxOpenPatch
{
    static void Postfix()
    {
        if (!APState.IsConnected)
            return;

        var kind = RewardBoxScript.GetRewardKind();

        if (kind >= RewardBoxScript.RewardKind.DrawerKey0 &&
            kind <= RewardBoxScript.RewardKind.DrawerKey3)
        {
            if (APState.KeysCompleted < 4)
            {
                APSaveManager.Save();
            }
        }
    }
}
