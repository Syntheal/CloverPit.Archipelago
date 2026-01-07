using HarmonyLib;

[HarmonyPatch(typeof(ScreenMenuScript))]
public static class BlockMenuInputWhileAPUIOpenPatch
{
    [HarmonyPrefix]
    [HarmonyPatch("Update")]
    static bool UpdatePrefix()
    {
        if (APState.IsAPUIOpen)
            return false;

        return true;
    }
}
