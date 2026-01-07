using HarmonyLib;
using Panik;

[HarmonyPatch(typeof(DrawersScript))]
public static class APDrawerOpenTryPatch
{
    [HarmonyPrefix]
    [HarmonyPatch(nameof(DrawersScript.OpenTry))]
    static bool OpenTry_Prefix(int index, ref bool __result)
    {
        if (!APState.IsConnected)
            return true;

        if (!APDrawerProgression.IsDrawerUnlocked(index))
        {
            Sound.Play("SoundDoorLocked");
            CameraGame.Shake(0.5f);
            __result = false;
            return false;
        }

        return true;
    }
}
