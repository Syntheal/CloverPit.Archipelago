using HarmonyLib;

[HarmonyPatch(typeof(AbilityScript))]
public static class APAbilityInitializeAllPatch
{
    [HarmonyPostfix]
    [HarmonyPatch(nameof(AbilityScript.InitializeAll))]
    static void Postfix()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        foreach (var kvp in AbilityScript.dict_ListsByCategory)
        {
            var list = kvp.Value;

            for (int i = list.Count - 1; i >= 0; i--)
            {
                var ability = list[i];
                var id = ability.IdentifierGet();

                if (!APPhoneAbilityMapping.IsManaged(id))
                    continue;

                if (!APState.UnlockedPhoneAbilities.Contains(id))
                {
                    list.RemoveAt(i);
                }
            }
        }
    }
}
