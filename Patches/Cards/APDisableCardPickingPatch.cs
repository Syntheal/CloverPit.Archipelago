using HarmonyLib;
using Panik;
using System.Collections.Generic;
using System.Reflection;

[HarmonyPatch(typeof(DeckBoxUI))]
public static class APDisableCardPickingPatch
{
    [HarmonyPrefix]
    [HarmonyPatch("PickCard")]
    static bool PickCard_Prefix(DeckBoxUI __instance, ref bool __result)
    {
        var cardsField = typeof(DeckBoxUI)
            .GetField("_cardsList", BindingFlags.NonPublic | BindingFlags.Instance);
        var indexField = typeof(DeckBoxUI)
            .GetField("cardNavigationIndex", BindingFlags.NonPublic | BindingFlags.Instance);

        if (cardsField == null || indexField == null)
            return true;

        var cards = cardsField.GetValue(__instance) as List<CardScript>;
        int index = (int)indexField.GetValue(__instance);

        if (cards == null || index < 0 || index >= cards.Count)
            return true;

        if (cards[index].identifier == RunModifierScript.Identifier.defaultModifier)
        {
            return true;
        }

        Sound.Play("SoundMenuError");
        CameraGame.Shake(1f);

        __result = false;
        return false;
    }
}
