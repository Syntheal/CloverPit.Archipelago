using HarmonyLib;
using System.Diagnostics;

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

        if (APState.CardLocation || APState.goalType == "card")
        {
            RunModifierScript.Identifier id = APState.CurrentModifier;

            if (id != RunModifierScript.Identifier.defaultModifier)
            {
                if (APCardMapping.CardToLocation.TryGetValue(id, out long location)
                    && !APLocationManager.IsChecked(location))
                {
                    if (!APState.BeatModifiers.Contains(id))
                        APState.BeatModifiers.Add(id);

                    APLocationManager.Complete(location);
                }
            }

            if (APState.BeatModifiers.Count >= APState.PackAmount &&
                APState.goalType == "card" &&
                !APState.goalCompleted)
            {
                APClient.SendGoalCompletion();
                APState.goalCompleted = true;
            }
        }

        if (APState.goalType == "key")
        {
            bool matchesRequiredEnding =
                (isGoodEnding && APState.RequiredKeyEnding == 1) ||
                (!isGoodEnding && APState.RequiredKeyEnding == 0);

            if (matchesRequiredEnding)
            {
                APClient.SendGoalCompletion();
                APState.goalCompleted = true;
            }
        }

        APSaveManager.Save();
    }
}