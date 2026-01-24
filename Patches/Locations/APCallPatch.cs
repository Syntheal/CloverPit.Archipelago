using HarmonyLib;
using Panik;

[HarmonyPatch(typeof(AbilityScript))]
public static class APCallPatch
{
    public static void doLocation(AbilityScript ability)
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        var identifier = ability.IdentifierGet();

        if (!APPhoneAbilityMapping.TryGetLocation(identifier, out long location))
            return;

        if (APLocationManager.IsChecked(location))
            return;

        APLocationManager.Complete(location);
        APSaveManager.Save();

        Plugin.Log.LogInfo($"[AP] Call ability picked: {identifier}");
    }

    [HarmonyPostfix]
    [HarmonyPatch("AFunc_OnPick_ExtraSpace")]
    public static void Postfix_OnPick_ExtraSpace(AbilityScript ability)
    {
        doLocation(ability);
    }

    [HarmonyPostfix]
    [HarmonyPatch("AFunc_OnPick_JackpotDouble")]
    public static void Postfix_OnPick_JackpotDouble(AbilityScript ability)
    {
        doLocation(ability);
    }

    [HarmonyPostfix]
    [HarmonyPatch("AFunc_OnPick_TicketsPlus5")]
    public static void Postfix_OnPick_TicketsPlus5(AbilityScript ability)
    {
        doLocation(ability);
    }

    [HarmonyPostfix]
    [HarmonyPatch("AFunc_OnPick_Discount2")]
    public static void Postfix_OnPick_Discount2(AbilityScript ability)
    {
        doLocation(ability);
    }

    [HarmonyPostfix]
    [HarmonyPatch("AFunc_OnPick_AddModifierRandomCharm_CloverKind")]
    public static void Postfix_OnPick_AddModifierRandomCharm_CloverKind(AbilityScript ability)
    {
        doLocation(ability);
    }

    [HarmonyPostfix]
    [HarmonyPatch("AFunc_OnPick_AddModifierRandomCharm_SymbolsMult")]
    public static void Postfix_OnPick_AddModifierRandomCharm_SymbolsMult(AbilityScript ability)
    {
        doLocation(ability);
    }

    [HarmonyPostfix]
    [HarmonyPatch("AFunc_OnPick_AddModifierRandomCharm_PatternsMult")]
    public static void Postfix_OnPick_AddModifierRandomCharm_PatternsMult(AbilityScript ability)
    {
        doLocation(ability);
    }

    [HarmonyPostfix]
    [HarmonyPatch("AFunc_OnPick_AddModifierRandomCharm_Obsessive")]
    public static void Postfix_OnPick_AddModifierRandomCharm_Obsessive(AbilityScript ability)
    {
        doLocation(ability);
    }

    [HarmonyPostfix]
    [HarmonyPatch("AFunc_OnPick_AddModifierRandomCharm_Gambler")]
    public static void Postfix_OnPick_AddModifierRandomCharm_Gambler(AbilityScript ability)
    {
        doLocation(ability);
    }

    [HarmonyPostfix]
    [HarmonyPatch("AFunc_OnPick_AddModifierRandomCharm_Speculative")]
    public static void Postfix_OnPick_AddModifierRandomCharm_Speculative(AbilityScript ability)
    {
        doLocation(ability);
    }

    [HarmonyPostfix]
    [HarmonyPatch("AFunc_OnPick_RechargeRedButtonPowerups")]
    public static void Postfix_OnPick_RechargeRedButtonPowerups(AbilityScript ability)
    {
        doLocation(ability);
    }

    [HarmonyPostfix]
    [HarmonyPatch("AFunc_OnPick_SymbolLemonChanceUp")]
    public static void Postfix_OnPick_SymbolLemonChanceUp(AbilityScript ability)
    {
        doLocation(ability);
    }

    [HarmonyPostfix]
    [HarmonyPatch("AFunc_OnPick_SymbolCherryChanceUp")]
    public static void Postfix_OnPick_SymbolCherryChanceUp(AbilityScript ability)
    {
        doLocation(ability);
    }

    [HarmonyPostfix]
    [HarmonyPatch("AFunc_OnPick_SymbolCloverChanceUp")]
    public static void Postfix_OnPick_SymbolCloverChanceUp(AbilityScript ability)
    {
        doLocation(ability);
    }

    [HarmonyPostfix]
    [HarmonyPatch("AFunc_OnPick_SymbolBellChanceUp")]
    public static void Postfix_OnPick_SymbolBellChanceUp(AbilityScript ability)
    {
        doLocation(ability);
    }

    [HarmonyPostfix]
    [HarmonyPatch("AFunc_OnPick_SymbolDiamondChanceUp")]
    public static void Postfix_OnPick_SymbolDiamondChanceUp(AbilityScript ability)
    {
        doLocation(ability);
    }

    [HarmonyPostfix]
    [HarmonyPatch("AFunc_OnPick_SymbolCoinsChanceUp")]
    public static void Postfix_OnPick_SymbolCoinsChanceUp(AbilityScript ability)
    {
        doLocation(ability);
    }

    [HarmonyPostfix]
    [HarmonyPatch("AFunc_OnPick_SymbolSevenChanceUp")]
    public static void Postfix_OnPick_SymbolSevenChanceUp(AbilityScript ability)
    {
        doLocation(ability);
    }

    [HarmonyPostfix]
    [HarmonyPatch("AFunc_OnPick_SymbolsValue_LemonAndCherry")]
    public static void Postfix_OnPick_SymbolsValue_LemonAndCherry(AbilityScript ability)
    {
        doLocation(ability);
    }

    [HarmonyPostfix]
    [HarmonyPatch("AFunc_OnPick_SymbolsValue_CloverAndBell")]
    public static void Postfix_OnPick_SymbolsValue_CloverAndBell(AbilityScript ability)
    {
        doLocation(ability);
    }

    [HarmonyPostfix]
    [HarmonyPatch("AFunc_OnPick_SymbolsValue_DiamondAndCoins")]
    public static void Postfix_OnPick_SymbolsValue_DiamondAndCoins(AbilityScript ability)
    {
        doLocation(ability);
    }

    [HarmonyPostfix]
    [HarmonyPatch("AFunc_OnPick_SymbolsValue_Seven")]
    public static void Postfix_OnPick_SymbolsValue_Seven(AbilityScript ability)
    {
        doLocation(ability);
    }

    [HarmonyPostfix]
    [HarmonyPatch("AFunc_OnPick_PatternsValue_3OrLessElements")]
    public static void Postfix_OnPick_PatternsValue_3OrLessElements(AbilityScript ability)
    {
        doLocation(ability);
    }

    [HarmonyPostfix]
    [HarmonyPatch("AFunc_OnPick_PatternsValue_4OrMoreElements")]
    public static void Postfix_OnPick_PatternsValue_4OrMoreElements(AbilityScript ability)
    {
        doLocation(ability);
    }

    [HarmonyPostfix]
    [HarmonyPatch("AFunc_OnPick_EvilGeneric_ShinyObjects")]
    public static void Postfix_OnPick_EvilGeneric_ShinyObjects(AbilityScript ability)
    {
        doLocation(ability);
    }

    [HarmonyPostfix]
    [HarmonyPatch("AFunc_OnPick_EvilGeneric_DoubleCloversButMoney0")]
    public static void Postfix_OnPick_EvilGeneric_DoubleCloversButMoney0(AbilityScript ability)
    {
        doLocation(ability);
    }

    [HarmonyPostfix]
    [HarmonyPatch("AFunc_OnPick_EvilGeneric_2FreeItemsTicketsZero")]
    public static void Postfix_OnPick_EvilGeneric_2FreeItemsTicketsZero(AbilityScript ability)
    {
        doLocation(ability);
    }

    [HarmonyPostfix]
    [HarmonyPatch("AFunc_OnPick_EvilGeneric_TakeOtherAbilitiesThenDeviousMod")]
    public static void Postfix_OnPick_EvilGeneric_TakeOtherAbilitiesThenDeviousMod(AbilityScript ability)
    {
        doLocation(ability);
    }

    [HarmonyPostfix]
    [HarmonyPatch("AFunc_OnPick_EvilGeneric_DoubleCoinsTicketsZero")]
    public static void Postfix_OnPick_EvilGeneric_DoubleCoinsTicketsZero(AbilityScript ability)
    {
        doLocation(ability);
    }

    [HarmonyPostfix]
    [HarmonyPatch("AFunc_OnPick_EvilHalvenChances_LemonsAndCherries")]
    public static void Postfix_OnPick_EvilHalvenChances_LemonsAndCherries(AbilityScript ability)
    {
        doLocation(ability);
    }

    [HarmonyPostfix]
    [HarmonyPatch("AFunc_OnPick_EvilHalvenChances_CloversAndBells")]
    public static void Postfix_OnPick_EvilHalvenChances_CloversAndBells(AbilityScript ability)
    {
        doLocation(ability);
    }

    [HarmonyPostfix]
    [HarmonyPatch("AFunc_OnPick_EvilHalvenChances_DiamondCoinsAndSeven")]
    public static void Postfix_OnPick_EvilHalvenChances_DiamondCoinsAndSeven(AbilityScript ability)
    {
        doLocation(ability);
    }
}