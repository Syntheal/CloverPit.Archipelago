using HarmonyLib;

[HarmonyPatch(typeof(DeckBoxUI))]
public static class APHideCardProgressTextPatch
{
    [HarmonyPostfix]
    [HarmonyPatch("_TextUpdate")]
    static void TextUpdate_Postfix(DeckBoxUI __instance)
    {
        if (__instance.textCompletedCards != null)
        {
            __instance.textCompletedCards.text = string.Empty;
            __instance.textCompletedCards.gameObject.SetActive(false);
        }
    }
}
