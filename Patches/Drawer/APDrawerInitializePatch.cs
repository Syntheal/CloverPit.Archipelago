using HarmonyLib;
using System.Reflection;

[HarmonyPatch(typeof(DrawersScript))]
public static class APDrawerInitializePatch
{
    private static readonly FieldInfo drawerIsUnlockedField =
        AccessTools.Field(typeof(DrawersScript), "drawerIsUnlocked");

    [HarmonyPostfix]
    [HarmonyPatch(nameof(DrawersScript.Initialize))]
    static void Initialize_Postfix()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        var inst = DrawersScript.instance;
        if (inst == null)
        {
            Plugin.Log.LogWarning("[AP] DrawersScript.instance is null during Initialize");
            return;
        }

        var unlocked = (bool[])drawerIsUnlockedField.GetValue(inst);

        for (int i = 0; i < unlocked.Length; i++)
        {
            unlocked[i] = APDrawerProgression.IsDrawerUnlocked(i);
        }

        Plugin.Log.LogInfo("[AP] Drawer unlock state overridden at init");
    }
}
