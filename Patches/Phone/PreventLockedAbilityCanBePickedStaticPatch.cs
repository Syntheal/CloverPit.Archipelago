using HarmonyLib;

[HarmonyPatch(typeof(AbilityScript))]
public static class PreventLockedAbilityCanBePickedStaticPatch
{
    [HarmonyPrefix]
    [HarmonyPatch(
        nameof(AbilityScript.CanBePicked),
        new[] { typeof(AbilityScript.Identifier) }
    )]
    static bool Prefix(
        AbilityScript.Identifier identifier,
        ref bool __result
    )
    {
        if (!APPhoneAbilityMapping.IsManaged(identifier))
            return true;

        if (!APState.UnlockedPhoneAbilities.Contains(identifier))
        {
            __result = false;
            return false;
        }

        return true;
    }
}
