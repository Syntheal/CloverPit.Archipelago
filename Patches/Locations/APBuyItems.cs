using HarmonyLib;
using Panik;
using System.Numerics;
using static StoreCapsuleScript;

[HarmonyPatch(typeof(StoreCapsuleScript))]
public static class APBuyItems
{
    [HarmonyPostfix]
    [HarmonyPatch("BuyTry")]
    public static void Postfix_BuyTry(int id, ref BuyResult __result)
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (__result == BuyResult.Empty || __result == BuyResult.NotEnoughCurrency)
            return;

        PowerupScript powerupScript = storePowerups[id];

        int charmId = powerupScript.identifier.GetHashCode();

        int locationId = 230000 + charmId;

        if (!APLocationManager.IsChecked(locationId))
        {
            APLocationManager.Complete(locationId);
            APSaveManager.Save();
            Plugin.Log.LogInfo($"[AP] Location with ID {locationId} activated");
        }
    }
}
