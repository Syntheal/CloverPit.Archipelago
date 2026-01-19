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
        return true;
    }

    public static void OnAPConnected()
    {
        connected = true;
    }
}
