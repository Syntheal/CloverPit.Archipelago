using HarmonyLib;
using System.Collections.Generic;
using System.Text;

[HarmonyPatch]
public static class PowerupNamePatch
{
    private class CacheEntry
    {
        public string BaseName;
        public long BuyId;
        public long TriggerId;
        public long ActivateId;
        public long EquipId;
    }

    private static readonly Dictionary<PowerupScript.Identifier, CacheEntry> cache = new Dictionary<PowerupScript.Identifier, CacheEntry>();

    [HarmonyPostfix]
    [HarmonyPatch(typeof(PowerupScript), nameof(PowerupScript.NameGet))]
    public static void Postfix_Powerup_NameGet(
        PowerupScript __instance,
        bool includeModTag,
        bool includeFusionIcon,
        bool sanitize,
        ref string __result)
    {
        if (!APState.IsConnected || !APState.APSaveLoaded || string.IsNullOrEmpty(__result))
            return;

        if (!APState.SacredLocation && ((int)__instance.identifier).ToString().Contains("999"))
            return;

        if (!cache.TryGetValue(__instance.identifier, out var entry))
        {
            entry = BuildCacheEntry(__instance, __result);
            cache[__instance.identifier] = entry;
        }

        var sb = new StringBuilder(__result);

        if (entry.EquipId != -1L)
        {
            sb.Append(APLocationManager.IsChecked(entry.EquipId)
                ? " (<color=green>E</color>)"
                : " (<color=red>E</color>)");
        }

        if (entry.BuyId != -1L)
        {
            sb.Append(APLocationManager.IsChecked(entry.BuyId)
                ? " (<color=green>B</color>)"
                : " (<color=red>B</color>)");
        }

        if (entry.TriggerId != -1L)
        {
            sb.Append(APLocationManager.IsChecked(entry.TriggerId)
                ? " (<color=green>T</color>)"
                : " (<color=red>T</color>)");
        }

        if (entry.ActivateId != -1L)
        {
            sb.Append(APLocationManager.IsChecked(entry.ActivateId)
                ? " (<color=green>A</color>)"
                : " (<color=red>A</color>)");
        }

        __result = sb.ToString();
    }

    private static CacheEntry BuildCacheEntry(PowerupScript instance, string result)
    {
        string baseName = StripModifierTag(result);

        string buyLoc = "Buy " + baseName;
        string triggerLoc = "Trigger " + baseName;
        string activateLoc = "Activate " + baseName;

        long buyId = buyLoc == "Buy Cigarettes"
            ? APLocations.GetLocationId("Smoke Some Cigarettes")
            : APLocations.GetLocationId(buyLoc);

        long triggerId;
        switch (triggerLoc)
        {
            case "Trigger Abyssu":
                triggerId = APLocations.GetLocationId("Throw Away Abyssu");
                break;
            case "Trigger Vorago":
                triggerId = APLocations.GetLocationId("Throw Away Vorago");
                break;
            case "Trigger Barathrum":
                triggerId = APLocations.GetLocationId("Throw Away Barathrum");
                break;
            default:
                triggerId = APLocations.GetLocationId(triggerLoc);
                break;
        }

        long activateId = APLocations.GetLocationId(activateLoc);

        long equipId = -1L;

        if (baseName == "Corpse" || baseName == "Skull")
        {
            switch (instance.identifier)
            {
                case PowerupScript.Identifier.Skeleton_Head:
                    equipId = APLocations.GetLocationId("Equip Skeleton Head");
                    break;
                case PowerupScript.Identifier.Skeleton_Arm1:
                    equipId = APLocations.GetLocationId("Equip Skeleton Arm 1");
                    break;
                case PowerupScript.Identifier.Skeleton_Arm2:
                    equipId = APLocations.GetLocationId("Equip Skeleton Arm 2");
                    break;
                case PowerupScript.Identifier.Skeleton_Leg1:
                    equipId = APLocations.GetLocationId("Equip Skeleton Leg 1");
                    break;
                case PowerupScript.Identifier.Skeleton_Leg2:
                    equipId = APLocations.GetLocationId("Equip Skeleton Leg 2");
                    break;
            }
        }

        return new CacheEntry
        {
            BaseName = baseName,
            BuyId = buyId,
            TriggerId = triggerId,
            ActivateId = activateId,
            EquipId = equipId
        };
    }

    private static string StripModifierTag(string text)
    {
        int idx = text.IndexOf(" (");
        return idx < 0 ? text : text.Substring(0, idx);
    }
}