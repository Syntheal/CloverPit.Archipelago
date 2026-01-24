using HarmonyLib;
using Panik;

[HarmonyPatch(typeof(GeneralUiScript))]
public static class BlockNewRunWithoutAPPatch
{
    public static bool connected = false;
    [HarmonyPrefix]
    [HarmonyPatch("_IntroMenuNewGame")]
    static bool Prefix()
    {
        if (!connected)
        {
            Sound.Play_Unpausable("SoundMenuError");
            return false;
        }
        APState.GameStarted = true;
        APClient._canProcessQueue = true;
        APClient.TryProcessQueue();
        return true;
    }
}