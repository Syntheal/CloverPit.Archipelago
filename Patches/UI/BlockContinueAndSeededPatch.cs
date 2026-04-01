using HarmonyLib;
using Panik;

[HarmonyPatch(typeof(ScreenMenuScript), "_SelectOption")]
class BlockContinueAndSeededPatch
{
    static void Prefix(ScreenMenuScript __instance, int index)
    {
        if (__instance.optionsTextArray == null)
            return;

        var text = __instance.optionsTextArray[index]?.text;

        if (
            text != null &&
            (
                text.ToLowerInvariant().Contains("continue") ||
                text.ToLowerInvariant().Contains("seeded") ||
                text.ToLowerInvariant().Contains("red>not all")
            ))
        {
            __instance.optionsTextArray[index].text = "<color=red>Not Allowed</color>";
            Sound.Play_Unpausable("SoundMenuError");

            if (__instance.optionEventsArray != null &&
                index < __instance.optionEventsArray.Length)
            {
                __instance.optionEventsArray[index] = null;
            }
        }
    }
}