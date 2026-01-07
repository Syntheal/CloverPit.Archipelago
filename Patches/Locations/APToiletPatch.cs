using HarmonyLib;
using Panik;
using static WCScript;

[HarmonyPatch(typeof(WCScript))]
public static class APToiletPatch
{
    [HarmonyPostfix]
    [HarmonyPatch(nameof(StartAction))]
    static void Postfix(ActionType actionType)
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        switch (actionType)
        {
            case ActionType.piss:
                APLocationManager.Complete(APLocations.TAKE_A_PISS);
                break;

            case ActionType.poop:
                APLocationManager.Complete(APLocations.TAKE_A_DUMP);
                break;
        }
    }
}
