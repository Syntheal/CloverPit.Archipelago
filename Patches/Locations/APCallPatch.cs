using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class APCallPatch
{
    [HarmonyPostfix]
    [HarmonyPatch(typeof(AbilityScript), nameof(AbilityScript.Pick))]
    public static void Postfix_AbilityPick(
    AbilityScript.Identifier identifier)
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (!APPhoneAbilityMapping.TryGetLocation(identifier, out long location))
            return;

        if (APLocationManager.IsChecked(location))
            return;

        APLocationManager.Complete(location);
        APSaveManager.Save();

        Plugin.Log.LogInfo($"[AP] Call ability picked: {identifier}");
    }

}
