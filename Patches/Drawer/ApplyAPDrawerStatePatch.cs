using HarmonyLib;
using System.Reflection;

[HarmonyPatch(typeof(DrawersScript))]
public static class ApplyAPDrawerStatePatch
{
    private static readonly FieldInfo drawerIsOpenField =
        AccessTools.Field(typeof(DrawersScript), "drawerIsOpen");

    [HarmonyPostfix]
    [HarmonyPatch("Start")]
    static void StartPostfix(DrawersScript __instance)
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        var drawerIsOpen = (bool[])drawerIsOpenField.GetValue(__instance);
        for (int i = 0; i < DrawersScript.MAX_DRAWERS; i++)
        {
            drawerIsOpen[i] = false;
            PowerupScript.array_InDrawer[i] = null;
        }

        APDrawerStateApplier.Apply();

        Plugin.Log.LogInfo("[AP] Drawer state hard-reset and reapplied");
    }
}
