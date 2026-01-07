using HarmonyLib;

[HarmonyPatch(typeof(Panik.VirtualCursors))]
public static class LockVirtualCursorWhileAPUIOpenPatch
{
    [HarmonyPrefix]
    [HarmonyPatch("Update")]
    static bool UpdatePrefix()
    {
        if (APState.IsAPUIOpen || APUITrapPopup.Instance.isShowing || APDeathLinkUI.Instance.isShowing)
        {
            return false;
        }

        return true;
    }
}
