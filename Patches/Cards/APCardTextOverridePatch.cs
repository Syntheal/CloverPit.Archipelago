using HarmonyLib;
using Panik;
using System;

[HarmonyPatch(typeof(RunModifierScript))]
public static class APCardTextOverridePatch
{
    [HarmonyPostfix]
    [HarmonyPatch(nameof(RunModifierScript.TitleGet))]
    static void TitleGet_Postfix(
    RunModifierScript.Identifier identifier,
    ref string __result)
    {
        if (identifier == RunModifierScript.Identifier.defaultModifier)
            return;
        if (APState.CardLocation || APState.goalType == "card")
        {
            if (APState.BeatModifiers.Contains(identifier))
                __result += " (<color=green>Completed</color>)";
            else
                __result += " (<color=red>Not Completed</color>)";
        }
    }

    [HarmonyPostfix]
    [HarmonyPatch(nameof(RunModifierScript.DescriptionGet))]
    static void DescriptionGet_Postfix(
        RunModifierScript.Identifier identifier,
        ref string __result)
    {
        if (identifier == RunModifierScript.Identifier.defaultModifier)
            return;
        if (!APState.CardLocation && APState.goalType != "card")
            return;

         __result += "\n<color=#FFBF00><i>Escape cell to complete</i></color>";
    }
}
public static class APCardContext
{
    public static RunModifierScript.Identifier? CurrentCard;
}

[HarmonyPatch(typeof(CardsInspectorScript), "InspectCard_Set")]
public static class InspectCardSetPatch
{
    static void Prefix(CardScript cardToInspect)
    {
        if (cardToInspect != null)
            APCardContext.CurrentCard = cardToInspect.identifier;
        else
            APCardContext.CurrentCard = null;
    }
}

[HarmonyPatch(typeof(Translation))]
public static class APTranslationPatch
{
    [HarmonyPrefix]
    [HarmonyPatch(nameof(Translation.Get))]
    static bool Prefix(string key, ref string __result)
    {
        if (key == "CARDS_INSPECTOR_TITLE__UNDISCOVERED")
        {
            if (APCardContext.CurrentCard is RunModifierScript.Identifier id)
            {
                int? pack = APCardUnlocker.GetPack(id);

                string temp = "";

                if (APCardMapping.CardToLocation.TryGetValue(id, out long location))
                {
                    temp = APLocations.GetLocationName(location);
                }

                if (pack != null)
                    __result = $"<color=#34eb80>{temp}</color> - Found in Card Pack {pack}";
                else
                    __result = "Found in Unknown Pack";
            }
            else
            {
                __result = "Undiscovered Card";
            }

            return false;
        }

        if (key == "CARDS_INSPECTOR_DESCRIPTION__UNDISCOVERED")
        {
            __result = "Find the Archipelago item to unlock this card.";
            return false;
        }

        return true;
    }
}
