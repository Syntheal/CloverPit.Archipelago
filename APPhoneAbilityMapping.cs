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

        { AbilityScript.Identifier.holyGeneric_ModifyStoreCharms_Make1Free, APLocations.CALL_HOLY_MODIFY_STORE },
        { AbilityScript.Identifier.holyGeneric_MultiplierPatterns_1, APLocations.CALL_HOLY_MULT_PATTERN },
        { AbilityScript.Identifier.holyGeneric_MultiplierSymbols_1, APLocations.CALL_HOLY_MULT_SYMB },
        { AbilityScript.Identifier.holyGeneric_PatternsRepetitionIncrase, APLocations.CALL_HOLY_PATTERN_REP },
        { AbilityScript.Identifier.holyGeneric_ReduceChargesNeeded_ForRedButtonCharms, APLocations.CALL_HOLY_REDUCE },
        { AbilityScript.Identifier.holyGeneric_SpawnSacredCharm, APLocations.CALL_HOLY_HELP },

        { AbilityScript.Identifier.holyPatternsValue_3LessElements, APLocations.CALL_HOLY_3LESS },
        { AbilityScript.Identifier.holyPatternsValue_4MoreElements, APLocations.CALL_HOLY_4MORE },
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
            { "Call: Please don't throw my stuff away!", AbilityScript.Identifier.extraSpace },
            { "Call: interestsUp", AbilityScript.Identifier.interestsUp },
            { "Call: I can't quit now!", AbilityScript.Identifier.jackpotIncreaseBase },
            { "Call: Can I borrow some green?", AbilityScript.Identifier.ticketsPlus5 },
            { "Call: Can I eat something?", AbilityScript.Identifier.discount1 },
            { "Call: This time I'm betting on Green!", AbilityScript.Identifier.rndCharmMod_Clover },
            { "Call: This time I'm betting on Yellow!", AbilityScript.Identifier.rndCharmMod_SymbMult },
            { "Call: This time I'm betting on Orange!", AbilityScript.Identifier.rndCharmMod_PatMult },
            { "Call: If I keep trying, the patterns will align!", AbilityScript.Identifier.rndCharmMod_Obsessive },
            { "Call: It's kinda fun!", AbilityScript.Identifier.rndCharmMod_Gambler },
            { "Call: I'm sure the value will rise!", AbilityScript.Identifier.rndCharmMod_Speculative },
            { "Call: Can I get some Energy Drinks?", AbilityScript.Identifier.rechargeRedButtonPowerups },
            { "Call: Life gave me lemons", AbilityScript.Identifier.symbolChances_Lemon },
            { "Call: I used to eat healthy...", AbilityScript.Identifier.symbolChances_Cherry },
            { "Call: Today's my lucky day!", AbilityScript.Identifier.symbolChances_Clover },
            { "Call: I need to be there!", AbilityScript.Identifier.symbolChances_Bell },
            { "Call: Please, I'll give you anything", AbilityScript.Identifier.symbolChances_Diamond },
            { "Call: I need money!", AbilityScript.Identifier.symbolChances_Coins },
            { "Call: I didn't hurt anybody...", AbilityScript.Identifier.symbolChances_Seven },
            { "Call: I need supplements!", AbilityScript.Identifier.symbolsValue_LemonAndCherry },
            { "Call: I'm feeling lucky!", AbilityScript.Identifier.symbolsValue_CloverAndBell },
            { "Call: Gold and Diamonds are a good investment!", AbilityScript.Identifier.symbolsValue_DiamondAndCoins },
            { "Call: I'm gonna go All In!", AbilityScript.Identifier.symbolsValue_Seven },
            { "Call: I'm thinking of some strategies!", AbilityScript.Identifier.patternsValue_3LessElements },
            { "Call: I found a winning strategy!", AbilityScript.Identifier.patternsValue_4MoreElements },
            { "Call: evilGeneric_SpawnSkeletonPiece", AbilityScript.Identifier.evilGeneric_SpawnSkeletonPiece },
            { "Call: I like cryptic values!", AbilityScript.Identifier.evilGeneric_DoubleCloversButMoney0 },
            { "Call: evilGeneric_More666", AbilityScript.Identifier.evilGeneric_More666 },
            { "Call: I love shiny stuff!", AbilityScript.Identifier.evilGeneric_ShinyObjects },
            { "Call: I don't care about the price!", AbilityScript.Identifier.evilGeneric_2FreeItemsTicketsZero },
            { "Call: My head hurts!", AbilityScript.Identifier.evilGeneric_TakeOtherAbilitiesButDeviousMod },
            { "Call: Give me back my money!", AbilityScript.Identifier.evilGeneric_DoubleCoinsTicketsZero },
            { "Call: There's nothing to eat but mould.", AbilityScript.Identifier.evilHalvenChances_LemonAndCherry },
            { "Call: Wait, what day is it?", AbilityScript.Identifier.evilHalvenChances_CloverAndBell },
            { "Call: I've already bet it all!", AbilityScript.Identifier.evilHalvenChances_DiamondCoinsAndSeven },
            { "Call: I'm re-organizing my mind!", AbilityScript.Identifier.holyGeneric_MultiplierSymbols_1 },
            { "Call: I see the patterns in my behaviour.", AbilityScript.Identifier.holyGeneric_MultiplierPatterns_1 },
            { "Call: I'm feeling more energetic recently.", AbilityScript.Identifier.holyGeneric_ReduceChargesNeeded_ForRedButtonCharms },
            { "Call: I need to heal myself.", AbilityScript.Identifier.holyGeneric_ModifyStoreCharms_Make1Free },
            { "Call: I'm being constant!", AbilityScript.Identifier.holyGeneric_PatternsRepetitionIncrase },
            { "Call: Help!", AbilityScript.Identifier.holyGeneric_SpawnSacredCharm },
            { "Call: I want to address some stuff.", AbilityScript.Identifier.holyPatternsValue_3LessElements },
            { "Call: I'll take back control of my life.", AbilityScript.Identifier.holyPatternsValue_4MoreElements },
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
