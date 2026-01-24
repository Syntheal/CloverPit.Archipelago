using HarmonyLib;
using Panik;
using System;
using System.Collections.Generic;
using System.Linq;
using static StoreCapsuleScript;

[HarmonyPatch(typeof(StoreCapsuleScript))]
public static class APBuyItems
{
    public static readonly Dictionary<string, int> CharmToItemId = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
    {
        { "Charm: Ankh", 123001 },
        { "Charm: Horseshoe", 123002 },
        { "Charm: Golden Horseshoe", 123003 },
        { "Charm: Hamsa", 123004 },
        { "Charm: Rotated Hamsa", 123005 },
        { "Charm: Lucky Cat", 123006 },
        { "Charm: Chonky Cat", 123007 },
        { "Charm: Swole Cat", 123008 },
        { "Charm: One Trick Pony", 123009 },
        { "Charm: Red Shiny Rock", 123010 },
        { "Charm: Tarot Deck", 123011 },
        { "Charm: Dung Beetle", 123012 },
        { "Charm: Grandma's Purse", 123013 },
        { "Charm: Pentacle", 123014 },
        { "Charm: Property Certificate", 123015 },
        { "Charm: Nuclear Button", 123016 },
        { "Charm: Dear Diary", 123017 },
        { "Charm: Megaphone", 123018 },
        { "Charm: Super Capacitor", 123019 },
        { "Charm: Dynamo", 123020 },
        { "Charm: Scratch And Hope", 123021 },
        { "Charm: Stonks", 123022 },
        { "Charm: Car Battery", 123023 },
        { "Charm: Expired Meds", 123024 },
        { "Charm: Midas Touch", 123025 },
        { "Charm: Number 1", 123026 },
        { "Charm: Number 2", 123027 },
        { "Charm: Pain Killers", 123028 },
        { "Charm: Lost Wallet", 123029 },
        { "Charm: Calendar", 123030 },
        { "Charm: Little Star", 123031 },
        { "Charm: Fortune Cookie", 123032 },
        { "Charm: Fidelity Card", 123033 },
        { "Charm: Sardines", 123034 },
        { "Charm: Broken Calculator", 123035 },
        { "Charm: The Collector", 123036 },
        { "Charm: Channeler of Fortune", 123037 },
        { "Charm: Nose", 123038 },
        { "Charm: Eye Jar", 123039 },
        { "Charm: Voicemail", 123040 },
        { "Charm: Ring Bell", 123041 },
        { "Charm: Consolation Prize", 123042 },
        { "Charm: Dark Lotus", 123043 },
        { "Charm: Weird Clock", 123044 },
        { "Charm: Music Tape", 123045 },
        { "Charm: Shopping Cart", 123046 },
        { "Charm: Crowbar", 123047 },
        { "Charm: Disc A", 123048 },
        { "Charm: Disc B", 123049 },
        { "Charm: Disc C", 123050 },
        { "Charm: Cardboard House", 123051 },
        { "Charm: Lost Briefcase", 123052 },
        { "Charm: Electricity Meter", 123053 },
        { "Charm: Potato Battery", 123054 },
        { "Charm: Ancient Coin", 123055 },
        { "Charm: Cat Food", 123056 },
        { "Charm: Depression", 123057 },
        { "Charm: Toy Train", 123058 },
        { "Charm: Steam Locomotive", 123059 },
        { "Charm: Diesel Locomotive", 123060 },
        { "Charm: Clover Voucher", 123061 },
        { "Charm: CloverPot", 123062 },
        { "Charm: CloverPet", 123063 },
        { "Charm: CloverField", 123064 },
        { "Charm: Mushrooms", 123065 },
        { "Charm: Vine's Soup", 123066 },
        { "Charm: Very Big Mushroom", 123067 },
        { "Charm: Abyssu", 123068 },
        { "Charm: Vorago", 123069 },
        { "Charm: Barathrum", 123070 },
        { "Charm: Stain", 123071 },
        { "Charm: Abstract Painting", 123072 },
        { "Charm: Pareidolia", 123073 },
        { "Charm: Hourglass", 123074 },
        { "Charm: Fruit Basket", 123075 },
        { "Charm: 7 Sins Stone", 123076 },
        { "Charm: Necklace", 123077 },
        { "Charm: CloverBell", 123078 },
        { "Charm: Red Pepper", 123079 },
        { "Charm: Green Pepper", 123080 },
        { "Charm: Rotten Pepper", 123081 },
        { "Charm: Bell Pepper", 123082 },
        { "Charm: Golden Pepper", 123083 },
        { "Charm: Demon's Horn", 123084 },
        { "Charm: Necronomicon", 123085 },
        { "Charm: Holy Bible", 123086 },
        { "Charm: Baphomet", 123087 },
        { "Charm: Cross", 123088 },
        { "Charm: Rosary", 123089 },
        { "Charm: Book Of Shadows", 123090 },
        { "Charm: Gabihbb'h", 123091 },
        { "Charm: Possessed Cell Phone", 123092 },
        { "Charm: Magical Tomato", 123093 },
        { "Charm: Ritual Bell", 123094 },
        { "Charm: Crystal Skull", 123095 },
        { "Charm: Evil Deal", 123096 },
        { "Charm: Chastity Belt", 123097 },
        { "Charm: Photo Book", 123098 },
        { "Charm: Lemon Picture", 123099 },
        { "Charm: Cherry Picture", 123100 },
        { "Charm: Clover Picture", 123101 },
        { "Charm: Bell Picture", 123102 },
        { "Charm: Diamond Picture", 123103 },
        { "Charm: Treasure Picture", 123104 },
        { "Charm: Seven Picture", 123105 },
        { "Charm: Clicker", 123106 },
        { "Charm: AA Batteries", 123107 },
        { "Charm: Crystal Sphere", 123108 },
        { "Charm: Naughty Dealer", 123109 },
        { "Charm: Greedy King", 123110 },
        { "Charm: Raging Capitalist", 123111 },
        { "Charm: Personal Trainer", 123112 },
        { "Charm: Electrician", 123113 },
        { "Charm: Fortune Teller", 123114 },
        { "Charm: Golden Lemon", 123115 },
        { "Charm: Golden Cherry", 123116 },
        { "Charm: Golden Clover", 123117 },
        { "Charm: Golden Bell", 123118 },
        { "Charm: Golden Diamond", 123119 },
        { "Charm: Golden Treasure", 123120 },
        { "Charm: Golden Seven", 123121 },
        { "Charm: Bricks", 123122 },
        { "Charm: Wood", 123123 },
        { "Charm: Sheep", 123124 },
        { "Charm: Wheat", 123125 },
        { "Charm: Stone", 123126 },
        { "Charm: Harbor", 123127 },
        { "Charm: Thief", 123128 },
        { "Charm: Wheelbarrow", 123129 },
        { "Charm: Shoe", 123130 },
        { "Charm: Thimble", 123131 },
        { "Charm: Iron", 123132 },
        { "Charm: Car", 123133 },
        { "Charm: Ship", 123134 },
        { "Charm: Tuba Hat", 123135 },
        { "Charm: Ace Of Hearts", 123136 },
        { "Charm: Ace Of Clubs", 123137 },
        { "Charm: Ace Of Diamonds", 123138 },
        { "Charm: Ace Of Spades", 123139 },
        { "Charm: Jimbo", 123140 },
        { "Charm: D4", 123141 },
        { "Charm: D6", 123142 },
        { "Charm: D20", 123143 },
        { "Charm: Fake Coin", 123144 }
    };

    public static long GetBuyLocationId(int charmItemId)
    {
        return 230000 + charmItemId;
    }

    private static string PendingAPActivation = null;

    [HarmonyPrefix]
    [HarmonyPatch("BuyTry")]
    public static void Prefix_BuyTry(int id, ref BuyResult __result)
    {
        if (id == 4)
            return;

        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (id < 0 || id >= storePowerups.Count() || storePowerups[id] == null)
        {
            return;
        }

        PowerupScript powerupScript = storePowerups[id];

        if (IsSkeletonPowerup(powerupScript.identifier))
            return;

        string charmName = APItemMapping.IdentifierToPowerup(powerupScript.identifier);

        if (charmName.StartsWith("Skeleton"))
            return;

        PendingAPActivation = charmName;
    }

    [HarmonyPostfix]
    [HarmonyPatch("BuyTry")]
    public static void Postfix_BuyTry(int id, ref BuyResult __result)
    {
        if (!APState.IsConnected || !APState.APSaveLoaded || __result != BuyResult.Success)
            return;

        if (string.IsNullOrEmpty(PendingAPActivation))
            return;

        if (CharmToItemId.TryGetValue(PendingAPActivation, out var charmItemId))
        {
            long locationId = GetBuyLocationId(charmItemId);

            if (!APLocationManager.IsChecked(locationId))
            {
                APLocationManager.Complete(locationId);
                APSaveManager.Save();
                Plugin.Log.LogInfo($"Activated location: {APLocations.GetLocationName(locationId)}");
            }
        }

        PendingAPActivation = null;
    }
    private static bool IsSkeletonPowerup(PowerupScript.Identifier identifier)
    {
        return identifier == PowerupScript.Identifier.Skeleton_Head ||
               identifier == PowerupScript.Identifier.Skeleton_Arm1 ||
               identifier == PowerupScript.Identifier.Skeleton_Arm2 ||
               identifier == PowerupScript.Identifier.Skeleton_Leg1 ||
               identifier == PowerupScript.Identifier.Skeleton_Leg2;
    }
}
