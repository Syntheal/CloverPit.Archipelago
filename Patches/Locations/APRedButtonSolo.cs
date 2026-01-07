using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[HarmonyPatch(typeof(RedButtonScript))]
internal static class APRedButtonSolo
{
    [HarmonyPostfix]
    [HarmonyPatch(nameof(RedButtonScript.Press))]
    private static void Postfix_RedButtonPressed()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (APLocationManager.IsChecked(APLocations.RED_BUTTON_PRESSED))
            return;

        APLocationManager.Complete(APLocations.RED_BUTTON_PRESSED);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Red Button pressed");
    }
}
