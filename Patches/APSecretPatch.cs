using HarmonyLib;
using System.Reflection;
using Panik;

[HarmonyPatch]
public static class APSecretPatch
{
    static MethodBase TargetMethod()
    {
        var t = AccessTools.TypeByName("Panik.Data+GameData");
        return AccessTools.Method(t, "IsPowerupSecret");
    }

    static bool Prefix(PowerupScript.Identifier powerup, ref bool __result)
    {
        if (APState.UnlockedPowerups.Contains(powerup))
        {
            __result = false;
            return false;
        }

        return true;
    }
}
