using HarmonyLib;
using Panik;

[HarmonyPatch(typeof(GeneralUiScript))]
public static class DisableSeededPatch
{
    public static bool connected = false;
    [HarmonyPrefix]
    [HarmonyPatch("_IntroMenuNewSeededGame")]
    static bool Prefix()
    {
        Sound.Play_Unpausable("SoundMenuError");
        return false;
    }
}