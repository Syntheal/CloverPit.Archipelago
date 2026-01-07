using System.Collections.Generic;

public static class APLocations
{
    private const long DEADLINE_LOCATION_BASE = 223_400;
    private const long KEY_LOCATION_BASE = 223_500;
    public static long GetDeadlineLocation(int deadlineIndex)
    {
        return DEADLINE_LOCATION_BASE + deadlineIndex;
    }
    public static long GetKeyLocation(int KeyIndex)
    {
        return KEY_LOCATION_BASE + KeyIndex;
    }
    public static long GetJackpotLocation(SymbolScript.Kind jackpotKind)
    {
        int index = (int)jackpotKind;
        if (index < 1 || index >= (int)SymbolScript.Kind.count) return 0;

        return 223600 + index;
    }

    public const long ACTIVATE_FAKE_COIN = 200_001;
    public const long ACTIVATE_ANKH = 200_002;
    public const long ACTIVATE_HAMSA = 200_003;
    public const long ACTIVATE_ROTATED_HAMSA = 200_004;
    public const long ACTIVATE_LUCKYCAT = 200_005;
    public const long ACTIVATE_LUCKYCATFAT = 200_006;
    public const long ACTIVATE_LUCKYCATSWOLE = 200_007;
    public const long ACTIVATE_DUNG_BEETLE = 200_008;
    public const long ACTIVATE_PONY = 200_009;
    public const long ACTIVATE_TAROT_DECK = 200_010;
    public const long ACTIVATE_PENTACLE = 200_011;
    public const long ACTIVATE_DYNAMO = 200_012;
    public const long ACTIVATE_YELLOW_STAR = 200_013;
    public const long ACTIVATE_CONSOLATION_PRIZE = 200_014;
    public const long ACTIVATE_DISC_A = 200_015;
    public const long ACTIVATE_DISC_B = 200_016;
    public const long ACTIVATE_DISC_C = 200_017;
    public const long SMOKE_CIGS = 200_018;
    public const long ACTIVATE_POTATO = 200_019;
    public const long ACTIVATE_TOY_TRAIN = 200_020;
    public const long ACTIVATE_STEAM_LOCOMOTIVE = 200_021;
    public const long ACTIVATE_DIESEL_LOCOMOTIVE = 200_022;
    public const long ACTIVATE_CLOVER_POT = 200_023;
    public const long ACTIVATE_CLOVER_FIELD = 200_024;
    public const long ACTIVATE_SHROOMS = 200_025;
    public const long ACTIVATE_VINE_SHROOM = 200_026;
    public const long ACTIVATE_GIANT_SHROOM = 200_027;
    public const long DISCARD_ABYSSU = 200_028;
    public const long DISCARD_VORAGO = 200_029;
    public const long DISCARD_BARATHRUM = 200_030;
    public const long ACTIVATE_STAIN = 200_031;
    public const long ACTIVATE_ABSTRACT_PAINTING = 200_032;
    public const long ACTIVATE_PAREIDOLIA = 200_033;
    public const long ACTIVATE_HOURGLASS = 200_034;
    public const long ACTIVATE_7_SINS_STONE = 200_035;
    public const long ACTIVATE_CLOVER_BELL = 200_036;
    public const long ACTIVATE_RED_PEPPER = 200_037;
    public const long ACTIVATE_GREEN_PEPPER = 200_038;
    public const long ACTIVATE_ROTTEN_PEPPER = 200_039;
    public const long ACTIVATE_BELL_PEPPER = 200_040;
    public const long ACTIVATE_GOLDEN_PEPPER = 200_041;
    public const long ACTIVATE_DEVIL_HORN = 200_042;
    public const long ACTIVATE_HOLY_BIBLE = 200_043;
    public const long ACTIVATE_BAPHOMET = 200_044;
    public const long ACTIVATE_ROSARY = 200_045;
    public const long ACTIVATE_BOOK_OF_SHADOWS = 200_046;
    public const long ACTIVATE_GABIBBH = 200_047;
    public const long ACTIVATE_MYSTICAL_TOMATO = 200_048;
    public const long ACTIVATE_RITUAL_BELL = 200_049;
    public const long ACTIVATE_CRYSTAL_SKULL = 200_050;
    public const long ACTIVATE_ACE_HEARTS = 200_051;
    public const long ACTIVATE_ACE_CLUBS = 200_052;
    public const long ACTIVATE_ACE_DIAMONDS = 200_053;
    public const long ACTIVATE_ACE_SPADES = 200_054;
    public const long ACTIVATE_EYE_OF_GOD = 200_055;
    public const long ACTIVATE_HOLY_SPIRIT = 200_056;
    public const long ACTIVATE_ETERNITY = 200_057;
    public const long ACTIVATE_ADAMS_RIBCAGE = 200_058;
    public const long ACTIVATE_OPHANIM_WHEELS = 200_059;
    public const long ACTIVATE_DICE_4 = 200_060;
    public const long ACTIVATE_DICE_6 = 200_061;
    public const long ACTIVATE_DICE_20 = 200_062;

    public const long RED_BUTTON_PRESSED = 200_500;
    public const long RED_BUTTON_GOLDEN_HORSESHOE = 200_501;
    public const long RED_BUTTON_RED_CRYSTAL = 200_502;
    public const long RED_BUTTON_MIDAS_TOUCH = 200_503;
    public const long RED_BUTTON_NUMBER1 = 200_504;
    public const long RED_BUTTON_NUMBER2 = 200_505;
    public const long RED_BUTTON_RING_BELL = 200_506;
    public const long RED_BUTTON_WEIRD_CLOCK = 200_507;
    public const long RED_BUTTON_ANCIENT_COIN = 200_508;
    public const long RED_BUTTON_CROSS = 200_509;
    public const long RED_BUTTON_PHONE = 200_510;
    public const long RED_BUTTON_LEMON_PICTURE = 200_511;
    public const long RED_BUTTON_CHERRY_PICTURE = 200_512;
    public const long RED_BUTTON_CLOVER_PICTURE = 200_513;
    public const long RED_BUTTON_BELL_PICTURE = 200_514;
    public const long RED_BUTTON_DIAMOND_PICTURE = 200_515;
    public const long RED_BUTTON_TREASURE_PICTURE = 200_516;
    public const long RED_BUTTON_SEVEN_PICTURE = 200_517;
    public const long RED_BUTTON_ANGEL = 200_518;

    public const long TAKE_A_PISS = 201_001;
    public const long TAKE_A_DUMP = 201_002;

    public const long SKELETON_HEAD = 202_001;
    public const long SKELETON_ARM_1 = 202_002;
    public const long SKELETON_ARM_2 = 202_003;
    public const long SKELETON_LEG_1 = 202_004;
    public const long SKELETON_LEG_2 = 202_005;

    public const long EARN_10K_ROUND = 205_001;
    public const long EARN_100K_ROUND = 205_002;
    public const long EARN_1M_ROUND = 205_003;
    public const long EARN_10M_ROUND = 205_004;
    public const long EARN_100M_ROUND = 205_005;

    public const long HIT_MODIFIER_INTEREST = 205_006;
    public const long HIT_MODIFIER_GOLDEN = 205_007;
    public const long HIT_MODIFIER_CHAIN = 205_008;
    public const long HIT_MODIFIER_BATTERY = 205_009;
    public const long HIT_MODIFIER_CLOVER_TICKET = 205_010;
    public const long HIT_MODIFIER_REPETITION = 205_011;

    public const long EARN_10K_SPIN= 205_012;
    public const long EARN_100K_SPIN = 205_013;
    public const long EARN_1M_SPIN = 205_014;
    public const long EARN_10M_SPIN = 205_015;
    public const long EARN_100M_SPIN = 205_016;

    public const long HAVE_50_CLOVER_TICKETS = 206001;
    public const long HAVE_100_CLOVER_TICKETS = 206002;
    public const long HAVE_150_CLOVER_TICKETS = 206003;
    public const long HAVE_200_CLOVER_TICKETS = 206004;
    public const long HAVE_250_CLOVER_TICKETS = 206005;

    public const long GOOD_ENDING = 999_997;
    public const long BAD_ENDING = 999_998;
    public const long GOAL_COMPLETE = 999_999;

    public const long CALL_EXTRA_SPACE = 200_100;
    public const long CALL_JACKPOT_INCREASE_BASE = 200_102;
    public const long CALL_TICKETS_PLUS_5 = 200_103;
    public const long CALL_DISCOUNT_1 = 200_104;

    public const long CALL_RND_CHARM_MOD_CLOVER = 200_105;
    public const long CALL_RND_CHARM_MOD_SYMBMULT = 200_106;
    public const long CALL_RND_CHARM_MOD_PATMULT = 200_107;
    public const long CALL_RND_CHARM_MOD_OBSESSIVE = 200_108;
    public const long CALL_RND_CHARM_MOD_GAMBLER = 200_109;
    public const long CALL_RND_CHARM_MOD_SPECULATIVE = 200_110;

    public const long CALL_RECHARGE_RED_BUTTON_POWERUPS = 200_111;

    public const long CALL_SYMBOL_CHANCES_LEMON = 200_112;
    public const long CALL_SYMBOL_CHANCES_CHERRY = 200_113;
    public const long CALL_SYMBOL_CHANCES_CLOVER = 200_114;
    public const long CALL_SYMBOL_CHANCES_BELL = 200_115;
    public const long CALL_SYMBOL_CHANCES_DIAMOND = 200_116;
    public const long CALL_SYMBOL_CHANCES_COINS = 200_117;
    public const long CALL_SYMBOL_CHANCES_SEVEN = 200_118;

    public const long CALL_SYMBOLS_VALUE_LEMON_AND_CHERRY = 200_119;
    public const long CALL_SYMBOLS_VALUE_CLOVER_AND_BELL = 200_120;
    public const long CALL_SYMBOLS_VALUE_DIAMOND_AND_COINS = 200_121;
    public const long CALL_SYMBOLS_VALUE_SEVEN = 200_122;

    public const long CALL_PATTERNS_VALUE_3_LESS_ELEMENTS = 200_123;
    public const long CALL_PATTERNS_VALUE_4_MORE_ELEMENTS = 200_124;

    public const long CALL_EVIL_DOUBLE_CLOVERS_MONEY_ZERO = 200_126;
    public const long CALL_EVIL_SHINY_OBJECTS = 200_128;
    public const long CALL_EVIL_2_FREE_ITEMS_TICKETS_ZERO = 200_129;
    public const long CALL_EVIL_TAKE_OTHER_ABILITIES_DEVIOUS_MOD = 200_130;
    public const long CALL_EVIL_DOUBLE_COINS_TICKETS_ZERO = 200_131;

    public const long CALL_EVIL_HALVEN_CHANCES_LEMON_CHERRY = 200_132;
    public const long CALL_EVIL_HALVEN_CHANCES_CLOVER_BELL = 200_133;
    public const long CALL_EVIL_HALVEN_CHANCES_DIAMOND_COINS_SEVEN = 200_134;

    public const long CALL_HOLY_MULTIPLIER_SYMBOLS_1 = 200_135;
    public const long CALL_HOLY_MULTIPLIER_PATTERNS_1 = 200_136;
    public const long CALL_HOLY_REDUCE_CHARGES_RED_BUTTON = 200_137;
    public const long CALL_HOLY_MODIFY_STORE_CHARMS_MAKE_1_FREE = 200_138;
    public const long CALL_HOLY_PATTERNS_REPETITION_INCREASE = 200_139;
    public const long CALL_HOLY_SPAWN_SACRED_CHARM = 200_140;

    public const long CALL_HOLY_PATTERNS_VALUE_3_LESS_ELEMENTS = 200_141;
    public const long CALL_HOLY_PATTERNS_VALUE_4_MORE_ELEMENTS = 200_142;

    public static readonly Dictionary<string, long> LocationNamesToIds = new Dictionary<string, long>
    {
        { "Trigger Fake Coin", ACTIVATE_FAKE_COIN },
        { "Trigger Ankh", ACTIVATE_ANKH },
        { "Trigger Hamsa", ACTIVATE_HAMSA },
        { "Trigger Rotated Hamsa", ACTIVATE_ROTATED_HAMSA },
        { "Trigger Lucky Cat", ACTIVATE_LUCKYCAT },
        { "Trigger Chonky Cat", ACTIVATE_LUCKYCATFAT },
        { "Trigger Swole Cat", ACTIVATE_LUCKYCATSWOLE },
        { "Trigger Dung Beetle", ACTIVATE_DUNG_BEETLE },
        { "Trigger One Trick Pony", ACTIVATE_PONY },
        { "Trigger Tarot Deck", ACTIVATE_TAROT_DECK },
        { "Trigger Pentacle", ACTIVATE_PENTACLE },
        { "Trigger Dynamo", ACTIVATE_DYNAMO },
        { "Trigger Little Star", ACTIVATE_YELLOW_STAR },
        { "Trigger Consolation Prize", ACTIVATE_CONSOLATION_PRIZE },
        { "Trigger Disc A", ACTIVATE_DISC_A },
        { "Trigger Disc B", ACTIVATE_DISC_B },
        { "Trigger Disc C", ACTIVATE_DISC_C },
        { "Smoke Some Cigarettes", SMOKE_CIGS },
        { "Trigger Potato Battery", ACTIVATE_POTATO },
        { "Trigger Toy Train", ACTIVATE_TOY_TRAIN },
        { "Trigger Steam Locomotive", ACTIVATE_STEAM_LOCOMOTIVE },
        { "Trigger Diesel Locomotive", ACTIVATE_DIESEL_LOCOMOTIVE },
        { "Trigger Clover Pot", ACTIVATE_CLOVER_POT },
        { "Trigger Clover Field", ACTIVATE_CLOVER_FIELD },
        { "Trigger Mushrooms", ACTIVATE_SHROOMS },
        { "Trigger Vine's Soup", ACTIVATE_VINE_SHROOM },
        { "Trigger Very Big Mushroom", ACTIVATE_GIANT_SHROOM },
        { "Throw Away Abyssu", DISCARD_ABYSSU },
        { "Throw Away Vorago", DISCARD_VORAGO },
        { "Throw Away Barathrum", DISCARD_BARATHRUM },
        { "Trigger Stain", ACTIVATE_STAIN },
        { "Trigger Abstract Painting", ACTIVATE_ABSTRACT_PAINTING },
        { "Trigger Pareidolia", ACTIVATE_PAREIDOLIA },
        { "Trigger Hourglass", ACTIVATE_HOURGLASS },
        { "Trigger Seven Sins Stone", ACTIVATE_7_SINS_STONE },
        { "Trigger Clover Bell", ACTIVATE_CLOVER_BELL },
        { "Trigger Red Pepper", ACTIVATE_RED_PEPPER },
        { "Trigger Green Pepper", ACTIVATE_GREEN_PEPPER },
        { "Trigger Rotten Pepper", ACTIVATE_ROTTEN_PEPPER },
        { "Trigger Bell Pepper", ACTIVATE_BELL_PEPPER },
        { "Trigger Golden Pepper", ACTIVATE_GOLDEN_PEPPER },
        { "Trigger Demon's Horn", ACTIVATE_DEVIL_HORN },
        { "Trigger Holy Bible", ACTIVATE_HOLY_BIBLE },
        { "Trigger Baphomet", ACTIVATE_BAPHOMET },
        { "Trigger Rosary", ACTIVATE_ROSARY },
        { "Trigger Book Of Shadows", ACTIVATE_BOOK_OF_SHADOWS },
        { "Trigger Gabihbb'h", ACTIVATE_GABIBBH },
        { "Trigger Mystical Tomato", ACTIVATE_MYSTICAL_TOMATO },
        { "Trigger Ritual Bell", ACTIVATE_RITUAL_BELL },
        { "Trigger Crystal Skull", ACTIVATE_CRYSTAL_SKULL },
        { "Trigger Ace Of Hearts", ACTIVATE_ACE_HEARTS },
        { "Trigger Ace Of Clubs", ACTIVATE_ACE_CLUBS },
        { "Trigger Ace Of Diamonds", ACTIVATE_ACE_DIAMONDS },
        { "Trigger Ace Of Spades", ACTIVATE_ACE_SPADES },
        { "Trigger Eye Of God", ACTIVATE_EYE_OF_GOD },
        { "Trigger Holy Spirit", ACTIVATE_HOLY_SPIRIT },
        { "Trigger Eternity", ACTIVATE_ETERNITY },
        { "Trigger Adam's Ribcage", ACTIVATE_ADAMS_RIBCAGE },
        { "Trigger Ophanim Wheels", ACTIVATE_OPHANIM_WHEELS },
        { "Trigger D4", ACTIVATE_DICE_4 },
        { "Trigger D6", ACTIVATE_DICE_6 },
        { "Trigger D20", ACTIVATE_DICE_20 },

        { "Press The Red Button", RED_BUTTON_PRESSED },
        { "Activate The Golden Horseshoe", RED_BUTTON_GOLDEN_HORSESHOE },
        { "Activate Red Shiny Rock", RED_BUTTON_RED_CRYSTAL },
        { "Activate Midas Touch", RED_BUTTON_MIDAS_TOUCH },
        { "Activate Number 1", RED_BUTTON_NUMBER1 },
        { "Activate Number 2", RED_BUTTON_NUMBER2 },
        { "Activate Ring Bell", RED_BUTTON_RING_BELL },
        { "Activate Weird Clock", RED_BUTTON_WEIRD_CLOCK },
        { "Activate Ancient Coin", RED_BUTTON_ANCIENT_COIN },
        { "Activate Cross", RED_BUTTON_CROSS },
        { "Activate Possessed Cell Phone", RED_BUTTON_PHONE },
        { "Activate Lemon Picture", RED_BUTTON_LEMON_PICTURE },
        { "Activate Cherry Picture", RED_BUTTON_CHERRY_PICTURE },
        { "Activate Clover Picture", RED_BUTTON_CLOVER_PICTURE },
        { "Activate Bell Picture", RED_BUTTON_BELL_PICTURE },
        { "Activate Diamond Picture", RED_BUTTON_DIAMOND_PICTURE },
        { "Activate Treasure Picture", RED_BUTTON_TREASURE_PICTURE },
        { "Activate Seven Picture", RED_BUTTON_SEVEN_PICTURE },
        { "Activate Angel's Hand", RED_BUTTON_ANGEL },

        { "Take A Piss", TAKE_A_PISS },
        { "Take A Dump", TAKE_A_DUMP },

        { "Equip Skeleton Head", SKELETON_HEAD },
        { "Equip Skeleton Arm 1", SKELETON_ARM_1 },
        { "Equip Skeleton Arm 2", SKELETON_ARM_2 },
        { "Equip Skeleton Leg 1", SKELETON_LEG_1 },
        { "Equip Skeleton Leg 2", SKELETON_LEG_2 },

        { "Earn 10k coins in one round", EARN_10K_ROUND },
        { "Earn 100k coins in one round", EARN_100K_ROUND },
        { "Earn 1m coins in one round", EARN_1M_ROUND },
        { "Earn 10m coins in one round", EARN_10M_ROUND },
        { "Earn 100m coins in one round", EARN_100M_ROUND },

        { "Hit A Token Modifier", HIT_MODIFIER_INTEREST },
        { "Hit A Golden Modifier", HIT_MODIFIER_GOLDEN },
        { "Hit A Chain Modifier", HIT_MODIFIER_CHAIN },
        { "Hit A Battery Modifier", HIT_MODIFIER_BATTERY },
        { "Hit A Clover Ticket Modifier", HIT_MODIFIER_CLOVER_TICKET },
        { "Hit A Repetition Modifier", HIT_MODIFIER_REPETITION },

        { "Earn 10k coins in one spin", EARN_10K_SPIN },
        { "Earn 100k coins in one spin", EARN_100K_SPIN },
        { "Earn 1m coins in one spin", EARN_1M_SPIN },
        { "Earn 10m coins in one spin", EARN_10M_SPIN },
        { "Earn 100m coins in one spin", EARN_100M_SPIN },

        { "Have 50 Clover Tickets", HAVE_50_CLOVER_TICKETS },
        { "Have 100 Clover Tickets", HAVE_100_CLOVER_TICKETS },
        { "Have 150 Clover Tickets", HAVE_150_CLOVER_TICKETS },
        { "Have 200 Clover Tickets", HAVE_200_CLOVER_TICKETS },
        { "Have 250 Clover Tickets", HAVE_250_CLOVER_TICKETS },

        { "Good Ending Achieved", GOOD_ENDING },
        { "Bad Ending Achieved", BAD_ENDING },
        { "Goal Completed", GOAL_COMPLETE },

        { "Call: Please don't throw my stuff away!", CALL_EXTRA_SPACE },
        { "Call: I can't quit now!", CALL_JACKPOT_INCREASE_BASE },
        { "Call: Can I borrow some green?", CALL_TICKETS_PLUS_5 },
        { "Call: Can I eat something?", CALL_DISCOUNT_1 },

        { "Call: This time I'm betting on Green!", CALL_RND_CHARM_MOD_CLOVER },
        { "Call: This time I'm betting on Yellow!", CALL_RND_CHARM_MOD_SYMBMULT },
        { "Call: This time I'm betting on Orange!", CALL_RND_CHARM_MOD_PATMULT },
        { "Call: If I keep trying, the patterns will align!", CALL_RND_CHARM_MOD_OBSESSIVE },
        { "Call: It's kinda fun!", CALL_RND_CHARM_MOD_GAMBLER },
        { "Call: I'm sure the value will rise!", CALL_RND_CHARM_MOD_SPECULATIVE },

        { "Call: Can I get some Energy Drinks?", CALL_RECHARGE_RED_BUTTON_POWERUPS },

        { "Call: Life gave me lemons", CALL_SYMBOL_CHANCES_LEMON },
        { "Call: I used to eat healthy...", CALL_SYMBOL_CHANCES_CHERRY },
        { "Call: Today's my lucky day!", CALL_SYMBOL_CHANCES_CLOVER },
        { "Call: I need to be there!", CALL_SYMBOL_CHANCES_BELL },
        { "Call: Please, I'll give you anything!", CALL_SYMBOL_CHANCES_DIAMOND },
        { "Call: I need money!", CALL_SYMBOL_CHANCES_COINS },
        { "Call: I didn't hurt anybody...", CALL_SYMBOL_CHANCES_SEVEN },

        { "Call: I need supplements!", CALL_SYMBOLS_VALUE_LEMON_AND_CHERRY },
        { "Call: I'm feeling lucky!", CALL_SYMBOLS_VALUE_CLOVER_AND_BELL },
        { "Call: Gold and Diamonds are a good investment!", CALL_SYMBOLS_VALUE_DIAMOND_AND_COINS },
        { "Call: I'm gonna go All In!", CALL_SYMBOLS_VALUE_SEVEN },

        { "Call: I'm thinking of some strategies!", CALL_PATTERNS_VALUE_3_LESS_ELEMENTS },
        { "Call: I found a winning strategy!", CALL_PATTERNS_VALUE_4_MORE_ELEMENTS },

        { "Call: I like cryptic values!", CALL_EVIL_DOUBLE_CLOVERS_MONEY_ZERO },
        { "Call: I love shiny stuff!", CALL_EVIL_SHINY_OBJECTS },
        { "Call: I don't care about the price!", CALL_EVIL_2_FREE_ITEMS_TICKETS_ZERO },
        { "Call: My head hurts!", CALL_EVIL_TAKE_OTHER_ABILITIES_DEVIOUS_MOD },
        { "Call: Give me back my money!", CALL_EVIL_DOUBLE_COINS_TICKETS_ZERO },

        { "Call: There's nothing to eat but mould.", CALL_EVIL_HALVEN_CHANCES_LEMON_CHERRY },
        { "Call: Wait, what day is it?", CALL_EVIL_HALVEN_CHANCES_CLOVER_BELL },
        { "Call: I've already bet it all!", CALL_EVIL_HALVEN_CHANCES_DIAMOND_COINS_SEVEN },

        { "Call: I'm re-organizing my mind!", CALL_HOLY_MULTIPLIER_SYMBOLS_1 },
        { "Call: I see the patterns in my behaviour.", CALL_HOLY_MULTIPLIER_PATTERNS_1 },
        { "Call: I'm feeling more energetic recently.", CALL_HOLY_REDUCE_CHARGES_RED_BUTTON },
        { "Call: I need to heal myself.", CALL_HOLY_MODIFY_STORE_CHARMS_MAKE_1_FREE },
        { "Call: I'm being constant!", CALL_HOLY_PATTERNS_REPETITION_INCREASE },
        { "Call: Help!", CALL_HOLY_SPAWN_SACRED_CHARM },

        { "Call: I want to address some stuff.", CALL_HOLY_PATTERNS_VALUE_3_LESS_ELEMENTS },
        { "Call: I'll take back control of my life.", CALL_HOLY_PATTERNS_VALUE_4_MORE_ELEMENTS },

        { "Deadline 1 Complete", GetDeadlineLocation(1) },
        { "Deadline 2 Complete", GetDeadlineLocation(2) },
        { "Deadline 3 Complete", GetDeadlineLocation(3) },
        { "Deadline 4 Complete", GetDeadlineLocation(4) },
        { "Deadline 5 Complete", GetDeadlineLocation(5) },
        { "Deadline 6 Complete", GetDeadlineLocation(6) },
        { "Deadline 7 Complete", GetDeadlineLocation(7) },
        { "Deadline 8 Complete", GetDeadlineLocation(8) },
        { "Deadline 9 Complete", GetDeadlineLocation(9) },
        { "Deadline 10 Complete", GetDeadlineLocation(10) },
        { "Deadline 11 Complete", GetDeadlineLocation(11) },
        { "Deadline 12 Complete", GetDeadlineLocation(12) },
        { "Deadline 13 Complete", GetDeadlineLocation(13) },
        { "Deadline 14 Complete", GetDeadlineLocation(14) },
        { "Deadline 15 Complete", GetDeadlineLocation(15) },

        { "Key 1 Collected", GetKeyLocation(0) },
        { "Key 2 Collected", GetKeyLocation(1) },
        { "Key 3 Collected", GetKeyLocation(2) },
        { "Key 4 Collected", GetKeyLocation(3) },

        { "Jackpot With Lemon", GetJackpotLocation(SymbolScript.Kind.lemon) },
        { "Jackpot With Cherry", GetJackpotLocation(SymbolScript.Kind.cherry) },
        { "Jackpot With Clover", GetJackpotLocation(SymbolScript.Kind.clover) },
        { "Jackpot With Bell", GetJackpotLocation(SymbolScript.Kind.bell) },
        { "Jackpot With Diamond", GetJackpotLocation(SymbolScript.Kind.diamond) },
        { "Jackpot With Treasure", GetJackpotLocation(SymbolScript.Kind.coins) },
        { "Jackpot With Seven", GetJackpotLocation(SymbolScript.Kind.seven) },

        { "Trigger 666", GetJackpotLocation(SymbolScript.Kind.six) },
        { "Trigger 999", GetJackpotLocation(SymbolScript.Kind.nine) }
    };

    public static void PopulateLocationNames()
    {
        foreach (var charm in APBuyItems.CharmToItemId)
        {
            string charmName = charm.Key;
            int charmItemId = charm.Value;

            string locationName = $"Buy {charmName.Substring(7)}";

            long locationId = APBuyItems.GetBuyLocationId(charmItemId);

            LocationNamesToIds[locationName] = locationId;
        }
    }

    public static string GetLocationName(long locationId)
    {
        foreach (var entry in LocationNamesToIds)
        {
            if (entry.Value == locationId)
            {
                return entry.Key;
            }
        }
        return null;
    }
}
