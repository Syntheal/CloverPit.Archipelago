using HarmonyLib;
using Panik;
using UnityEngine;

[HarmonyPatch(typeof(GameplayMaster), nameof(GameplayMaster.IsIntroDialogueFinished))]
class DeckBoxCloverTicketInitPatch
{
    static bool triggered = false;

    static void Postfix(bool __result)
    {
        if (!__result)
        {
            triggered = false;
            return;
        }

        if (triggered)
            return;

        triggered = true;

        APState.CurrentModifier = RunModifierScript.Identifier.defaultModifier;
        APState.DoorKeyUsed = false;

        GameplayData.CloverTicketsAdd(APState.StartTicketsSaved, false);
        APState.BonusDeadline = APState.BonusTicketsSaved;

        if (APState.UnlockedModifiers.Count > 0 &&
            !GameplayData.RunModifier_AlreadyPicked())
        {
            DeckBoxUI.Open(DeckBoxUI.UiKind.pickCardForTheRun);
        }
    }
}