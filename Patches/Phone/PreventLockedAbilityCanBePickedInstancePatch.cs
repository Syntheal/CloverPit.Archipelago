using HarmonyLib;

[HarmonyPatch(typeof(AbilityScript))]
public static class PreventLockedAbilityCanBePickedInstancePatch
{
    [HarmonyPrefix]
    [HarmonyPatch(nameof(AbilityScript.CanBePicked), new System.Type[0])]
    static bool Prefix(
        AbilityScript __instance,
        ref bool __result
    )
    {
        var id = __instance.IdentifierGet();

        if (!APPhoneAbilityMapping.IsManaged(id))
            return true;

        if (!APState.UnlockedPhoneAbilities.Contains(id))
        {
            __result = false;
            return false;
        }

        return true;
    }
}
