using HarmonyLib;
using Panik;

[HarmonyPatch(typeof(TerminalScript))]
public static class BlockTerminalNotificationPatch
{
    [HarmonyPrefix]
    [HarmonyPatch("NotificationSet")]
    static bool Prefix(PowerupScript.Identifier powerupIdentifier)
    {
        return false;
    }
}