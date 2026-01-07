using HarmonyLib;
using System.Reflection;

[HarmonyPatch(typeof(RewardBoxScript))]
public static class APKeysPatch
{
    [HarmonyPostfix]
    [HarmonyPatch(nameof(RewardBoxScript.Pick))]
    static void Postfix()
    {
        if (!APState.IsConnected)
            return;

        var kind = RewardBoxScript.GetRewardKind();

        switch (kind)
        {
            case RewardBoxScript.RewardKind.DrawerKey0:
            case RewardBoxScript.RewardKind.DrawerKey1:
            case RewardBoxScript.RewardKind.DrawerKey2:
            case RewardBoxScript.RewardKind.DrawerKey3:
                SendDrawerKeyLocation();
                break;

            case RewardBoxScript.RewardKind.DoorKey:
                break;
        }
    }

    private static readonly MethodInfo EndYes = AccessTools.Method(typeof(DrawersScript), "KeyAnimationAnswer_EndYes");

    private static readonly MethodInfo EndNo = AccessTools.Method(typeof(DrawersScript), "KeyAnimationAnswer_EndNo");

    private static void SendDrawerKeyLocation()
    {
        if (APState.KeysCompleted >= 4)
            return;

        int keyIndex = APState.KeysCompleted;
        long location = APLocations.GetKeyLocation(keyIndex);

        if (APLocationManager.IsChecked(location))
            return;

        APState.KeysCompleted++;

        APLocationManager.Complete(location);
        APSaveManager.Save();

        ShowDrawerKeyDialogue();
    }

    private static void ShowDrawerKeyDialogue()
    {
        if (DialogueScript.IsEnabled())
            return;

        var inst = DrawersScript.instance;
        if (inst == null)
            return;

        DialogueScript.SetQuestionDialogue(
            false,
            () => EndYes.Invoke(inst, null),
            () => EndNo.Invoke(inst, null),
            "DIALOGUE_DRAWER_UNLOCK_END_QUESTION"
        );

        DialogueScript.SetDialogueInputDelay(0.5f);
    }

}
