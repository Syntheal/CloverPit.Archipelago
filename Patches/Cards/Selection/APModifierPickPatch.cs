using HarmonyLib;

[HarmonyPatch(typeof(GameplayData), nameof(GameplayData.RunModifier_SetCurrent))]
public static class APModifierPickPatch
{
    static void Postfix(RunModifierScript.Identifier identifier)
    {
        APState.CurrentModifier = identifier;
    }
}