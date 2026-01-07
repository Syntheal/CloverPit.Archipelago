using HarmonyLib;

[HarmonyPatch(typeof(Panik.VirtualCursors))]
public static class LockVirtualCursorWhileAPUIOpenPatch
{
    [HarmonyPrefix]
    [HarmonyPatch("Update")]
    static bool UpdatePrefix()
    {
        if (APState.IsAPUIOpen)
        {
            return false;
        }

        return true;
    }
}
