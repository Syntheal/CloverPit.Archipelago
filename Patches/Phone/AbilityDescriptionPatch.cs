using HarmonyLib;
using Panik;
using System;
using TMPro;

[HarmonyPatch]
public static class AbilityDescriptionPatch
{
    [HarmonyPostfix]
    [HarmonyPatch(typeof(AbilityScript), "DescriptionGetTranslated", new Type[0])]
    public static void Postfix_Ability_DescriptionGetTranslated(
        AbilityScript __instance, ref string __result)
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (__instance == null || string.IsNullOrEmpty(__result))
            return;

        if (__instance.IdentifierGet() == AbilityScript.Identifier.evilHalvenChances_LemonAndCherry)
        {
            __result =
                "Permanently halve base lemon <sprite name=\"S_Lemon\"> & cherry <sprite name=\"S_Cherry\"> manifest chances. " +
                $"Then apply the <color=red>Devious</color> <rainb>trait</rainb> (<i>+0.6% to get <color=red>666</color></i>) to a random equipped charm.";
        }

        string descriptionText = __result;

        string abilityNameForLocation = __instance.NameGetTranslated();

        string locationName = "Call: " + abilityNameForLocation;
        long locId = APLocations.GetLocationId(locationName);
        if (locId == -1L) return;

        bool called = APLocationManager.IsChecked(locId);

        __result = descriptionText + (called ? "\n\n<color=green> Location Checked" + "</color>" : "\n\n<color=red> Not Checked" + "</color>");

        var phone = ToyPhoneUIScript.instance;
        if (phone == null || phone.abilityDisplays == null) return;

        foreach (var display in phone.abilityDisplays)
        {
            if (display == null || !display.gameObject.activeSelf) continue;

            var field = display.GetType().GetField("abilityDescriptionText");
            if (field == null) continue;

            var textComp = field.GetValue(display);
            if (textComp == null) continue;

            if (textComp is TMP_Text tmpText)
            {
                tmpText.enableAutoSizing = true;
                tmpText.fontSizeMin = 8;
                tmpText.fontSizeMax = 16; 
            }
            else if (textComp is UnityEngine.UI.Text uiText)
            {
                uiText.resizeTextForBestFit = true;
                uiText.resizeTextMinSize = 8;
                uiText.resizeTextMaxSize = 16;
            }
        }
    }
}
