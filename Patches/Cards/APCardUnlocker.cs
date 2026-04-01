using Panik;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public static class APCardUnlocker
{
    private static readonly Dictionary<int, RunModifierScript.Identifier[]> packs = new Dictionary<int, RunModifierScript.Identifier[]>
    {
        { 1, new[] { RunModifierScript.Identifier.phoneEnhancer, RunModifierScript.Identifier.redButtonOverload, RunModifierScript.Identifier.smallerStore } },
        { 2, new[] { RunModifierScript.Identifier.smallItemPool, RunModifierScript.Identifier.interestsGrow, RunModifierScript.Identifier.lessSpaceMoreDiscount } },
        { 3, new[] { RunModifierScript.Identifier.smallRoundsMoreRounds, RunModifierScript.Identifier.oneRoundPerDeadline, RunModifierScript.Identifier.headStart } },
        { 4, new[] { RunModifierScript.Identifier.extraPacks, RunModifierScript.Identifier._666BigBetDouble_SmallBetNoone, RunModifierScript.Identifier._666DoubleChances_JackpotRecovers } },
        { 5, new[] { RunModifierScript.Identifier._666LastRoundGuaranteed, RunModifierScript.Identifier.drawerTableModifications, RunModifierScript.Identifier.drawerModGamble } },
        { 6, new[] { RunModifierScript.Identifier.halven2SymbolsChances, RunModifierScript.Identifier.charmsRecycling } },
        { 7, new[] { RunModifierScript.Identifier.allCharmsStoreModded, RunModifierScript.Identifier.bigDebt } }
    };

    public static void GrantPack(string playerName, string itemName)
    {
        const string prefix = "Card Pack ";

        if (!itemName.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
        {
            Plugin.Log.LogError($"Invalid item name '{itemName}'");
            return;
        }

        if (!int.TryParse(itemName.Substring(prefix.Length), out int packNumber))
        {
            Plugin.Log.LogError($"Cannot parse pack number from '{itemName}'");
            return;
        }

        if (!packs.ContainsKey(packNumber))
        {
            Plugin.Log.LogError($"Pack number {packNumber} does not exist");
            return;
        }

        foreach (var id in packs[packNumber])
        {
            if (!APState.UnlockedModifiers.Contains(id))
            {
                APState.UnlockedModifiers.Add(id);
                if (playerName == APState.SlotName)
                    playerName = "Yourself";

                APConsoleLog.Log($"Card {id} sent by {playerName}", APConsoleLineType.ItemReceived);
            }
        }

        APSaveManager.Save();
    }
    public static int? GetPack(RunModifierScript.Identifier id)
    {
        foreach (var kvp in packs)
        {
            if (kvp.Value.Contains(id))
                return kvp.Key;
        }

        return null;
    }
}
