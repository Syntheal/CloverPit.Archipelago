using HarmonyLib;

[HarmonyPatch(typeof(ScreenMenuScript))]
public static class BlockMenuInputWhileAPUIOpenPatch
{
    [HarmonyPrefix]
    [HarmonyPatch("Update")]
    static bool UpdatePrefix()
    {
        if (APState.IsAPUIOpen || APUITrapPopup.Instance.isShowing)
            return false;

        return true;
    }
}
