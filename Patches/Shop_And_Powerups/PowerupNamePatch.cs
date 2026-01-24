using HarmonyLib;

[HarmonyPatch]
public static class PowerupNamePatch
{
    [HarmonyPostfix]
    [HarmonyPatch(typeof(PowerupScript), nameof(PowerupScript.NameGet))]
    public static void Postfix_Powerup_NameGet(
        PowerupScript __instance,
        bool includeModTag,
        bool sanitize,
        ref string __result)
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (string.IsNullOrEmpty(__result))
            return;


        string baseName = StripModifierTag(__result);

        string buyLoc = "Buy " + baseName;
        string triggerLoc = "Trigger " + baseName;
        string activateLoc = "Activate " + baseName;

        long buyId = -1L;
        if (buyLoc == "Buy Cigarettes")
        {
            buyId = APLocations.GetLocationId("Smoke Some Cigarettes");
        }
        else
        {
            buyId = APLocations.GetLocationId(buyLoc);
        }
        long triggerId = -1L;
        if (triggerLoc == "Trigger Abyssu")
        {
            triggerId = APLocations.GetLocationId("Throw Away Abyssu");
        }
        else if (triggerLoc == "Trigger Vorago")
        {
            triggerId = APLocations.GetLocationId("Throw Away Vorago");
        }
        else if (triggerLoc == "Trigger Barathrum")
        {
            triggerId = APLocations.GetLocationId("Throw Away Barathrum");
        }
        else
        {
            triggerId = APLocations.GetLocationId(triggerLoc);
        }
        long activateId = APLocations.GetLocationId(activateLoc);

        long equipId = -1L;
        if (baseName == "Corpse" || baseName == "Skull")
        {
            switch (__instance.identifier)
            {
                case (PowerupScript.Identifier.Skeleton_Head):
                    equipId = APLocations.GetLocationId("Equip Skeleton Head");
                break;
                case (PowerupScript.Identifier.Skeleton_Arm1):
                    equipId = APLocations.GetLocationId("Equip Skeleton Arm 1");
                break;
                case (PowerupScript.Identifier.Skeleton_Arm2):
                    equipId = APLocations.GetLocationId("Equip Skeleton Arm 2");
                break;
                case (PowerupScript.Identifier.Skeleton_Leg1):
                    equipId = APLocations.GetLocationId("Equip Skeleton Leg 1");
                break;
                case (PowerupScript.Identifier.Skeleton_Leg2):
                    equipId = APLocations.GetLocationId("Equip Skeleton Leg 2");
                break;
            }
            if (equipId != -1L)
                __result += APLocationManager.IsChecked(equipId) ? " (" + "<color=green>E" + "</color>)" : " (" + "<color=red>E" + "</color>)";
        }

        if (buyId != -1L)
            __result += APLocationManager.IsChecked(buyId) ? " (" + "<color=green>B" + "</color>)" : " (" + "<color=red>B" + "</color>)";

        if (triggerId != -1L)
            __result += APLocationManager.IsChecked(triggerId) ? " (" + "<color=green>T" + "</color>)" : " (" + "<color=red>T" + "</color>)";

        if (activateId != -1L)
            __result += APLocationManager.IsChecked(activateId) ? " (" + "<color=green>A" + "</color>)" : " (" + "<color=red>A" + "</color>)";
    }

    private static string StripModifierTag(string text)
    {
        int idx = text.LastIndexOf(" (");
        if (idx < 0)
            return text;

        return text.Substring(0, idx);
    }
}
