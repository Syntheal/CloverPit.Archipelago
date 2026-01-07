using HarmonyLib;
using System.Collections.Generic;

[HarmonyPatch(typeof(StoreCapsuleScript))]
public static class APStoreCapsuleSafetyPatch
{
    private const int MIN_SHOP_ITEMS = 4;

    private static readonly PowerupScript.Identifier Fallback =
        PowerupScript.Identifier.MoneyBriefCase;

    [HarmonyPrefix]
    [HarmonyPatch(nameof(StoreCapsuleScript.InitializeAll))]
    static void Prefix(StoreCapsuleScript __instance)
    {
        var fields = AccessTools.GetDeclaredFields(typeof(StoreCapsuleScript));

        foreach (var field in fields)
        {
            if (!typeof(List<PowerupScript.Identifier>).IsAssignableFrom(field.FieldType))
                continue;

            var list = field.GetValue(__instance) as List<PowerupScript.Identifier>;
            if (list == null)
                continue;

            if (list.Count >= MIN_SHOP_ITEMS)
                return;

            Plugin.Log.LogWarning(
                $"[AP] StoreCapsule list '{field.Name}' had {list.Count} items, padding"
            );

            while (list.Count < MIN_SHOP_ITEMS)
                list.Add(Fallback);

            return;
        }

        Plugin.Log.LogError(
            "[AP] StoreCapsuleSafetyPatch failed to locate shop list"
        );
    }
}
