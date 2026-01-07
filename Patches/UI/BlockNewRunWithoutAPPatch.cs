using HarmonyLib;
using Panik;

[HarmonyPatch(typeof(GeneralUiScript))]
public static class BlockNewRunWithoutAPPatch
{
    [HarmonyPrefix]
    [HarmonyPatch("_IntroMenuNewGame")]
    static bool Prefix()
    {
        if (!APState.IsConnected)
        {
            Sound.Play_Unpausable("SoundMenuError");
            return false;
        }
        APState.GameStarted = true;
        return true;
    }
}
