using HarmonyLib;

[HarmonyPatch(typeof(ScreenMenuScript))]
public static class AutoOpenAPUIOnMenuPatch
{
    [HarmonyPostfix]
    [HarmonyPatch(nameof(ScreenMenuScript.Open))]
    static void OpenPostfix()
    {
        if (!APState.IsAPUIOpen && !APState.GameStarted)
        {
            APConnectUI.EnableCursor();
            APState.IsAPUIOpen = true;
        }
    }
}
