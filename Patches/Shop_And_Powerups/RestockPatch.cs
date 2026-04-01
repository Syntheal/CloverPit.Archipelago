using HarmonyLib;
using Panik;
using System.Collections.Generic;

namespace CloverPit.Archipelago.Patches
{
    [HarmonyPatch(typeof(StoreCapsuleScript))]
    public static class RestockPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(StoreCapsuleScript.Restock))]
        public static void Postfix_Restock_APOverride()
        {
            if (!APState.IsConnected)
                return;
            
            if (!APState.APSaveLoaded)
                return;

            PowerupScript[] shop = StoreCapsuleScript.storePowerups;

            for (int i = 0; i < shop.Length; i++)
            {
                if (shop[i] != null)
                {
                    bool uncheckedLoc = HasUncheckedLocation(shop[i]);

                    if (uncheckedLoc)
                        return;
                }
            }

            List<PowerupScript> candidates = new List<PowerupScript>();

            foreach (PowerupScript.Identifier id in APState.UnlockedPowerups)
            {
                PowerupScript powerup = GetPowerupFromIdentifier(id);
                if (powerup == null)
                    continue;

                if (!PowerupScript.list_NotBought.Contains(powerup))
                    continue;

                if (StoreCapsuleScript.PowerupIsForbiddenToRestock(
                        id,
                        considerDeadlineNumberCondition: true,
                        considerEquipOrDrawerCondition: true))
                    continue;

                if (!HasUncheckedLocation(powerup))
                    continue;

                candidates.Add(powerup);
            }

            if (candidates.Count == 0)
                return;

            int roll = R.Rng_Store.Range(0, 100);

            if (roll < 70)
                return;

            List<int> validSlots = new List<int>();
            for (int i = 0; i < shop.Length; i++)
            {
                if (i == 4)
                    continue;

                if (shop[i] != null)
                    validSlots.Add(i);
            }

            if (validSlots.Count == 0)
                return;

            int slotIndex = validSlots[R.Rng_Store.Range(0, validSlots.Count)];
            PowerupScript replacement = candidates[R.Rng_Store.Range(0, candidates.Count)];

            if (shop[slotIndex].identifier != PowerupScript.Identifier.Cigarettes)
            {
                shop[slotIndex] = replacement;
                replacement.ModifierReEvaluate(true, false);
            }
            StoreCapsuleScript.RefreshCostTextAll();
            PowerupScript.RefreshPlacementAll();
        }

        private static PowerupScript GetPowerupFromIdentifier(PowerupScript.Identifier id)
        {
            for (int i = 0; i < PowerupScript.list_NotBought.Count; i++)
            {
                PowerupScript p = PowerupScript.list_NotBought[i];
                if (p != null && p.identifier == id)
                    return p;
            }
            return null;
        }

        private static bool HasUncheckedLocation(PowerupScript p)
        {
            string baseName = StripModifierTag(p.NameGet(false, true));

            long buyId;
            if (baseName == "Cigarettes")
                buyId = APLocations.GetLocationId("Smoke Some Cigarettes");
            else
                buyId = APLocations.GetLocationId("Buy " + baseName);

            long triggerId;
            if (baseName == "Abyssu")
                triggerId = APLocations.GetLocationId("Throw Away Abyssu");
            else if (baseName == "Vorago")
                triggerId = APLocations.GetLocationId("Throw Away Vorago");
            else if (baseName == "Barathrum")
                triggerId = APLocations.GetLocationId("Throw Away Barathrum");
            else
                triggerId = APLocations.GetLocationId("Trigger " + baseName);

            long activateId = APLocations.GetLocationId("Activate " + baseName);

            long equipId = -1L;
            if (baseName == "Skull")
            {
                equipId = APLocations.GetLocationId("Equip Skeleton Head");
            }

            return
                (buyId != -1L && !APLocationManager.IsChecked(buyId)) ||
                (triggerId != -1L && !APLocationManager.IsChecked(triggerId)) ||
                (activateId != -1L && !APLocationManager.IsChecked(activateId)) ||
                (equipId != -1L && !APLocationManager.IsChecked(equipId));
        }

        private static string StripModifierTag(string text)
        {
            int idx = text.IndexOf(" (");
            if (idx < 0)
                return text;

            return text.Substring(0, idx);
        }
    }
}
