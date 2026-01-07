using System;
using System.Collections.Generic;

public static class APPhoneAbilityMapping
{
    public static readonly Dictionary<AbilityScript.Identifier, long> AbilityToLocation =
    new Dictionary<AbilityScript.Identifier, long>
    {
        { AbilityScript.Identifier.extraSpace, APLocations.CALL_EXTRA_SPACE },
        { AbilityScript.Identifier.jackpotIncreaseBase, APLocations.CALL_JACKPOT_INCREASE_BASE },
        { AbilityScript.Identifier.ticketsPlus5, APLocations.CALL_TICKETS_PLUS_5 },
        { AbilityScript.Identifier.discount1, APLocations.CALL_DISCOUNT_1 },

        { AbilityScript.Identifier.rndCharmMod_Clover, APLocations.CALL_RND_CHARM_MOD_CLOVER },
        { AbilityScript.Identifier.rndCharmMod_SymbMult, APLocations.CALL_RND_CHARM_MOD_SYMBMULT },
        { AbilityScript.Identifier.rndCharmMod_PatMult, APLocations.CALL_RND_CHARM_MOD_PATMULT },
        { AbilityScript.Identifier.rndCharmMod_Obsessive, APLocations.CALL_RND_CHARM_MOD_OBSESSIVE },
        { AbilityScript.Identifier.rndCharmMod_Gambler, APLocations.CALL_RND_CHARM_MOD_GAMBLER },
        { AbilityScript.Identifier.rndCharmMod_Speculative, APLocations.CALL_RND_CHARM_MOD_SPECULATIVE },

        { AbilityScript.Identifier.rechargeRedButtonPowerups, APLocations.CALL_RECHARGE_RED_BUTTON_POWERUPS },

        { AbilityScript.Identifier.symbolChances_Lemon, APLocations.CALL_SYMBOL_CHANCES_LEMON },
        { AbilityScript.Identifier.symbolChances_Cherry, APLocations.CALL_SYMBOL_CHANCES_CHERRY },
        { AbilityScript.Identifier.symbolChances_Clover, APLocations.CALL_SYMBOL_CHANCES_CLOVER },
        { AbilityScript.Identifier.symbolChances_Bell, APLocations.CALL_SYMBOL_CHANCES_BELL },
        { AbilityScript.Identifier.symbolChances_Diamond, APLocations.CALL_SYMBOL_CHANCES_DIAMOND },
        { AbilityScript.Identifier.symbolChances_Coins, APLocations.CALL_SYMBOL_CHANCES_COINS },
        { AbilityScript.Identifier.symbolChances_Seven, APLocations.CALL_SYMBOL_CHANCES_SEVEN },

        { AbilityScript.Identifier.symbolsValue_LemonAndCherry, APLocations.CALL_SYMBOLS_VALUE_LEMON_AND_CHERRY },
        { AbilityScript.Identifier.symbolsValue_CloverAndBell, APLocations.CALL_SYMBOLS_VALUE_CLOVER_AND_BELL },
        { AbilityScript.Identifier.symbolsValue_DiamondAndCoins, APLocations.CALL_SYMBOLS_VALUE_DIAMOND_AND_COINS },
        { AbilityScript.Identifier.symbolsValue_Seven, APLocations.CALL_SYMBOLS_VALUE_SEVEN },

        { AbilityScript.Identifier.patternsValue_3LessElements, APLocations.CALL_PATTERNS_VALUE_3_LESS_ELEMENTS },
        { AbilityScript.Identifier.patternsValue_4MoreElements, APLocations.CALL_PATTERNS_VALUE_4_MORE_ELEMENTS },

        { AbilityScript.Identifier.evilGeneric_DoubleCloversButMoney0, APLocations.CALL_EVIL_DOUBLE_CLOVERS_MONEY_ZERO },
        { AbilityScript.Identifier.evilGeneric_ShinyObjects, APLocations.CALL_EVIL_SHINY_OBJECTS },
        { AbilityScript.Identifier.evilGeneric_2FreeItemsTicketsZero, APLocations.CALL_EVIL_2_FREE_ITEMS_TICKETS_ZERO },
        { AbilityScript.Identifier.evilGeneric_TakeOtherAbilitiesButDeviousMod, APLocations.CALL_EVIL_TAKE_OTHER_ABILITIES_DEVIOUS_MOD },
        { AbilityScript.Identifier.evilGeneric_DoubleCoinsTicketsZero, APLocations.CALL_EVIL_DOUBLE_COINS_TICKETS_ZERO },

        { AbilityScript.Identifier.evilHalvenChances_LemonAndCherry, APLocations.CALL_EVIL_HALVEN_CHANCES_LEMON_CHERRY },
        { AbilityScript.Identifier.evilHalvenChances_CloverAndBell, APLocations.CALL_EVIL_HALVEN_CHANCES_CLOVER_BELL },
        { AbilityScript.Identifier.evilHalvenChances_DiamondCoinsAndSeven, APLocations.CALL_EVIL_HALVEN_CHANCES_DIAMOND_COINS_SEVEN },

        { AbilityScript.Identifier.holyGeneric_MultiplierSymbols_1, APLocations.CALL_HOLY_MULTIPLIER_SYMBOLS_1 },
        { AbilityScript.Identifier.holyGeneric_MultiplierPatterns_1, APLocations.CALL_HOLY_MULTIPLIER_PATTERNS_1 },
        { AbilityScript.Identifier.holyGeneric_ReduceChargesNeeded_ForRedButtonCharms, APLocations.CALL_HOLY_REDUCE_CHARGES_RED_BUTTON },
        { AbilityScript.Identifier.holyGeneric_ModifyStoreCharms_Make1Free, APLocations.CALL_HOLY_MODIFY_STORE_CHARMS_MAKE_1_FREE },
        { AbilityScript.Identifier.holyGeneric_PatternsRepetitionIncrase, APLocations.CALL_HOLY_PATTERNS_REPETITION_INCREASE },
        { AbilityScript.Identifier.holyGeneric_SpawnSacredCharm, APLocations.CALL_HOLY_SPAWN_SACRED_CHARM },

        { AbilityScript.Identifier.holyPatternsValue_3LessElements, APLocations.CALL_HOLY_PATTERNS_VALUE_3_LESS_ELEMENTS },
        { AbilityScript.Identifier.holyPatternsValue_4MoreElements, APLocations.CALL_HOLY_PATTERNS_VALUE_4_MORE_ELEMENTS },
    };

    public static bool TryGetLocation(
        AbilityScript.Identifier identifier,
        out long location)
    {
        return AbilityToLocation.TryGetValue(identifier, out location);
    }

    private static readonly Dictionary<string, AbilityScript.Identifier> Map =
        new Dictionary<string, AbilityScript.Identifier>(StringComparer.OrdinalIgnoreCase)
        {
            { "Call: extraSpace", AbilityScript.Identifier.extraSpace },
            { "Call: interestsUp", AbilityScript.Identifier.interestsUp },
            { "Call: jackpotIncreaseBase", AbilityScript.Identifier.jackpotIncreaseBase },
            { "Call: ticketsPlus5", AbilityScript.Identifier.ticketsPlus5 },
            { "Call: discount1", AbilityScript.Identifier.discount1 },
            { "Call: rndCharmMod_Clover", AbilityScript.Identifier.rndCharmMod_Clover },
            { "Call: rndCharmMod_SymbMult", AbilityScript.Identifier.rndCharmMod_SymbMult },
            { "Call: rndCharmMod_PatMult", AbilityScript.Identifier.rndCharmMod_PatMult },
            { "Call: rndCharmMod_Obsessive", AbilityScript.Identifier.rndCharmMod_Obsessive },
            { "Call: rndCharmMod_Gambler", AbilityScript.Identifier.rndCharmMod_Gambler },
            { "Call: rndCharmMod_Speculative", AbilityScript.Identifier.rndCharmMod_Speculative },
            { "Call: rechargeRedButtonPowerups", AbilityScript.Identifier.rechargeRedButtonPowerups },
            { "Call: symbolChances_Lemon", AbilityScript.Identifier.symbolChances_Lemon },
            { "Call: symbolChances_Cherry", AbilityScript.Identifier.symbolChances_Cherry },
            { "Call: symbolChances_Clover", AbilityScript.Identifier.symbolChances_Clover },
            { "Call: symbolChances_Bell", AbilityScript.Identifier.symbolChances_Bell },
            { "Call: symbolChances_Diamond", AbilityScript.Identifier.symbolChances_Diamond },
            { "Call: symbolChances_Coins", AbilityScript.Identifier.symbolChances_Coins },
            { "Call: symbolChances_Seven", AbilityScript.Identifier.symbolChances_Seven },
            { "Call: symbolsValue_LemonAndCherry", AbilityScript.Identifier.symbolsValue_LemonAndCherry },
            { "Call: symbolsValue_CloverAndBell", AbilityScript.Identifier.symbolsValue_CloverAndBell },
            { "Call: symbolsValue_DiamondAndCoins", AbilityScript.Identifier.symbolsValue_DiamondAndCoins },
            { "Call: symbolsValue_Seven", AbilityScript.Identifier.symbolsValue_Seven },
            { "Call: patternsValue_3LessElements", AbilityScript.Identifier.patternsValue_3LessElements },
            { "Call: patternsValue_4MoreElements", AbilityScript.Identifier.patternsValue_4MoreElements },
            { "Call: evilGeneric_SpawnSkeletonPiece", AbilityScript.Identifier.evilGeneric_SpawnSkeletonPiece },
            { "Call: evilGeneric_DoubleCloversButMoney0", AbilityScript.Identifier.evilGeneric_DoubleCloversButMoney0 },
            { "Call: evilGeneric_More666", AbilityScript.Identifier.evilGeneric_More666 },
            { "Call: evilGeneric_ShinyObjects", AbilityScript.Identifier.evilGeneric_ShinyObjects },
            { "Call: evilGeneric_2FreeItemsTicketsZero", AbilityScript.Identifier.evilGeneric_2FreeItemsTicketsZero },
            { "Call: evilGeneric_TakeOtherAbilitiesButDeviousMod", AbilityScript.Identifier.evilGeneric_TakeOtherAbilitiesButDeviousMod },
            { "Call: evilGeneric_DoubleCoinsTicketsZero", AbilityScript.Identifier.evilGeneric_DoubleCoinsTicketsZero },
            { "Call: evilHalvenChances_LemonAndCherry", AbilityScript.Identifier.evilHalvenChances_LemonAndCherry },
            { "Call: evilHalvenChances_CloverAndBell", AbilityScript.Identifier.evilHalvenChances_CloverAndBell },
            { "Call: evilHalvenChances_DiamondCoinsAndSeven", AbilityScript.Identifier.evilHalvenChances_DiamondCoinsAndSeven },
            { "Call: holyGeneric_MultiplierSymbols_1", AbilityScript.Identifier.holyGeneric_MultiplierSymbols_1 },
            { "Call: holyGeneric_MultiplierPatterns_1", AbilityScript.Identifier.holyGeneric_MultiplierPatterns_1 },
            { "Call: holyGeneric_ReduceChargesNeeded_ForRedButtonCharms", AbilityScript.Identifier.holyGeneric_ReduceChargesNeeded_ForRedButtonCharms },
            { "Call: holyGeneric_ModifyStoreCharms_Make1Free", AbilityScript.Identifier.holyGeneric_ModifyStoreCharms_Make1Free },
            { "Call: holyGeneric_PatternsRepetitionIncrase", AbilityScript.Identifier.holyGeneric_PatternsRepetitionIncrase },
            { "Call: holyGeneric_SpawnSacredCharm", AbilityScript.Identifier.holyGeneric_SpawnSacredCharm },
            { "Call: holyPatternsValue_3LessElements", AbilityScript.Identifier.holyPatternsValue_3LessElements },
            { "Call: holyPatternsValue_4MoreElements", AbilityScript.Identifier.holyPatternsValue_4MoreElements },
            { "Call: count", AbilityScript.Identifier.count },
        };

    public static AbilityScript.Identifier ToAbility(string apItemName)
    {
        if (!Map.TryGetValue(apItemName, out var id))
            throw new Exception($"[AP] Unknown phone ability item: {apItemName}");

        return id;
    }

    public static bool IsManaged(AbilityScript.Identifier id) => Map.ContainsValue(id);
    public static bool IsPhoneAbility(string apItemName) => Map.ContainsKey(apItemName);
}
