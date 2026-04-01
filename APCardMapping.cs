using System;
using System.Collections.Generic;

public static class APCardMapping
{
    public static readonly Dictionary<RunModifierScript.Identifier, long> CardToLocation =
    new Dictionary<RunModifierScript.Identifier, long>
    {
        { RunModifierScript.Identifier.smallerStore, APLocations.CARD_DESPERATE },
        { RunModifierScript.Identifier.smallRoundsMoreRounds, APLocations.CARD_FIXATION },
        { RunModifierScript.Identifier.oneRoundPerDeadline, APLocations.CARD_SCREEN_ADDICTION },
        { RunModifierScript.Identifier.headStart, APLocations.CARD_COLD },
        { RunModifierScript.Identifier.extraPacks, APLocations.CARD_OLD },
        { RunModifierScript.Identifier.charmsRecycling, APLocations.CARD_BULLIES },
        { RunModifierScript.Identifier.bigDebt, APLocations.CARD_DELUSIONS },
        { RunModifierScript.Identifier._666BigBetDouble_SmallBetNoone, APLocations.CARD_IMPORTANT },
        { RunModifierScript.Identifier._666DoubleChances_JackpotRecovers, APLocations.CARD_RECOVERY },
        { RunModifierScript.Identifier.drawerTableModifications, APLocations.CARD_LIFE },
        { RunModifierScript.Identifier.interestsGrow, APLocations.CARD_INVESTMENT },
        { RunModifierScript.Identifier.lessSpaceMoreDiscount, APLocations.CARD_SACRIFICES },
        { RunModifierScript.Identifier.drawerModGamble, APLocations.CARD_GAMBLING },
        { RunModifierScript.Identifier.halven2SymbolsChances, APLocations.CARD_CLASS },
        { RunModifierScript.Identifier._666LastRoundGuaranteed, APLocations.CARD_HEARTBREAK },
        { RunModifierScript.Identifier.smallItemPool, APLocations.CARD_REPRESSED },
        { RunModifierScript.Identifier.phoneEnhancer, APLocations.CARD_FIRST },
        { RunModifierScript.Identifier.allCharmsStoreModded, APLocations.CARD_EXPENSIVE },
        { RunModifierScript.Identifier.redButtonOverload, APLocations.CARD_OVERDOSE },
    };
}
