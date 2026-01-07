using System;
using System.Collections.Generic;

public static class APItemMapping
{
    private static readonly Dictionary<string, PowerupScript.Identifier> Map =
        new Dictionary<string, PowerupScript.Identifier>(StringComparer.OrdinalIgnoreCase)
        {

            { "Charm: Ankh", PowerupScript.Identifier.Ankh },
            { "Charm: Horseshoe", PowerupScript.Identifier.HorseShoe },
            { "Charm: Golden Horseshoe", PowerupScript.Identifier.HorseShoeGold },
            { "Charm: Hamsa", PowerupScript.Identifier.HamsaUpside },
            { "Charm: Rotated Hamsa", PowerupScript.Identifier.HamsaInverted },
            { "Charm: Lucky Cat", PowerupScript.Identifier.LuckyCat },
            { "Charm: Chonky Cat", PowerupScript.Identifier.LuckyCatFat },
            { "Charm: Swole Cat", PowerupScript.Identifier.LuckyCatSwole },
            { "Charm: One Trick Pony", PowerupScript.Identifier.OneTrickPony },
            { "Charm: Red Shiny Rock", PowerupScript.Identifier.RedCrystal },
            { "Charm: Tarot Deck", PowerupScript.Identifier.TarotDeck },
            { "Charm: Dung Beetle", PowerupScript.Identifier.PoopBeetle },
            { "Charm: Grandmas Purse", PowerupScript.Identifier.GrandmasPurse },
            { "Charm: Pentacle", PowerupScript.Identifier.Pentacle },
            { "Charm: Property Certificate", PowerupScript.Identifier.HouseContract },
            { "Charm: Nuclear Button", PowerupScript.Identifier.Button2X },
            { "Charm: Dear Diary", PowerupScript.Identifier.DearDiary },
            { "Charm: Megaphone", PowerupScript.Identifier.Megaphone },
            { "Charm: Super Capacitor", PowerupScript.Identifier.SuperCapacitor },
            { "Charm: Dynamo", PowerupScript.Identifier.CrankGenerator },
            { "Charm: Scratch And Hope", PowerupScript.Identifier.GrattaEVinci_ScratchAndWin },
            { "Charm: Stonks", PowerupScript.Identifier.Stonks },
            { "Charm: Car Battery", PowerupScript.Identifier.CarBattery },
            { "Charm: Expired Meds", PowerupScript.Identifier.ExpiredMedicines },
            { "Charm: Midas Touch", PowerupScript.Identifier.GoldenHand_MidasTouch },
            { "Charm: Number 1", PowerupScript.Identifier.PissJar },
            { "Charm: Number 2", PowerupScript.Identifier.PoopJar },
            { "Charm: Pain Killers", PowerupScript.Identifier.Painkillers },
            { "Charm: Lost Wallet", PowerupScript.Identifier.Wallet },
            { "Charm: Lost Briefcase", PowerupScript.Identifier.MoneyBriefCase },
            { "Charm: Calendar", PowerupScript.Identifier.Calendar },
            { "Charm: Little Star", PowerupScript.Identifier.YellowStar },
            { "Charm: Wolf", PowerupScript.Identifier.Wolf },
            { "Charm: Fortune Cookie", PowerupScript.Identifier.FortuneCookie },
            { "Charm: Fidelity Card", PowerupScript.Identifier.FideltyCard },
            { "Charm: Sardines", PowerupScript.Identifier.Sardines },
            { "Charm: Broken Calculator", PowerupScript.Identifier.BrokenCalculator },
            { "Charm: The Collector", PowerupScript.Identifier.TheCollector },
            { "Charm: Channeler of Fortune", PowerupScript.Identifier.FortuneChanneler },
            { "Charm: Nose", PowerupScript.Identifier.Nose },
            { "Charm: Eye Jar", PowerupScript.Identifier.EyeJar },
            { "Charm: Voice Mail", PowerupScript.Identifier.VoiceMailTape },
            { "Charm: Garbage", PowerupScript.Identifier.Garbage },
            { "Charm: All In", PowerupScript.Identifier.AllIn },
            { "Charm: Ring Bell", PowerupScript.Identifier.RingBell },
            { "Charm: Consolation Prize", PowerupScript.Identifier.ConsolationPrize },
            { "Charm: Dark Lotus", PowerupScript.Identifier.DarkLotus },
            { "Charm: Steps Counter", PowerupScript.Identifier.StepsCounter },
            { "Charm: Weird Clock", PowerupScript.Identifier.WeirdClock },
            { "Charm: Music Tape", PowerupScript.Identifier.MusicTape },
            { "Charm: Shopping Cart", PowerupScript.Identifier.ShoppingCart },
            { "Charm: Crowbar", PowerupScript.Identifier.CrowBar },
            { "Charm: Disc A", PowerupScript.Identifier.DiscA },
            { "Charm: Disc B", PowerupScript.Identifier.DiscB },
            { "Charm: Disc C", PowerupScript.Identifier.DiscC },
            { "Charm: Cardboard House", PowerupScript.Identifier.CardboardHouse },
            { "Charm: Cigarettes", PowerupScript.Identifier.Cigarettes },
            { "Charm: Electricity Meter", PowerupScript.Identifier.ElectricityCounter },
            { "Charm: Potato Battery", PowerupScript.Identifier.PotatoPower },
            { "Charm: Fake Coin", PowerupScript.Identifier.FakeCoin },
            { "Charm: Ancient Coin", PowerupScript.Identifier.AncientCoin },
            { "Charm: Cat Food", PowerupScript.Identifier.CatTreats },
            { "Charm: Depression", PowerupScript.Identifier.Depression },
            { "Charm: Toy Train", PowerupScript.Identifier.ToyTrain },
            { "Charm: Steam Locomotive", PowerupScript.Identifier.LocomotiveSteam },
            { "Charm: Diesel Locomotive", PowerupScript.Identifier.LocomotiveDiesel },
            { "Charm: Clover Voucher", PowerupScript.Identifier.CloverVoucher },
            { "Charm: Clover Pot", PowerupScript.Identifier.CloverPot },
            { "Charm: Clover Pet", PowerupScript.Identifier.CloverPet },
            { "Charm: Clover Field", PowerupScript.Identifier.CloversLandPatch },
            { "Charm: Mushrooms", PowerupScript.Identifier.Mushrooms },
            { "Charm: Vine's Soup", PowerupScript.Identifier.VineSoupShroom },
            { "Charm: Very Big Mushroom", PowerupScript.Identifier.GiantShroom },
            { "Charm: Abyssu", PowerupScript.Identifier.Hole_Circle },
            { "Charm: Vorago", PowerupScript.Identifier.Hole_Romboid },
            { "Charm: Barathrum", PowerupScript.Identifier.Hole_Cross },
            { "Charm: Stain", PowerupScript.Identifier.Rorschach },
            { "Charm: Abstract Painting", PowerupScript.Identifier.AbstractPainting },
            { "Charm: Pareidolia", PowerupScript.Identifier.Pareidolia },
            { "Charm: Hourglass", PowerupScript.Identifier.Hourglass },
            { "Charm: Fruit Basket", PowerupScript.Identifier.FruitBasket },
            { "Charm: Seven Sins Stone", PowerupScript.Identifier.SevenSinsStone },
            { "Charm: Necklace", PowerupScript.Identifier.Necklace },
            { "Charm: Clover Bell", PowerupScript.Identifier.CloverBell },
            { "Charm: Red Pepper", PowerupScript.Identifier.HornChilyRed },
            { "Charm: Green Pepper", PowerupScript.Identifier.HornChilyGreen },
            { "Charm: Rotten Pepper", PowerupScript.Identifier.RottenPepper },
            { "Charm: Bell Pepper", PowerupScript.Identifier.BellPepper },
            { "Charm: Golden Pepper", PowerupScript.Identifier.GoldenPepper },
            { "Charm: Demon's Horn", PowerupScript.Identifier.HornDevil },
            { "Charm: Necronomicon", PowerupScript.Identifier.Necronomicon },
            { "Charm: Holy Bible", PowerupScript.Identifier.HolyBible },
            { "Charm: Baphomet", PowerupScript.Identifier.Baphomet },
            { "Charm: Cross", PowerupScript.Identifier.Cross },
            { "Charm: Rosary", PowerupScript.Identifier.Rosary },
            { "Charm: Book Of Shadows", PowerupScript.Identifier.BookOfShadows },
            { "Charm: Gabihbb'h", PowerupScript.Identifier.Gabibbh },
            { "Charm: Possessed Cell Phone", PowerupScript.Identifier.PossessedPhone },
            { "Charm: Mystical Tomato", PowerupScript.Identifier.MysticalTomato },
            { "Charm: Ritual Bell", PowerupScript.Identifier.RitualBell },
            { "Charm: Crystal Skull", PowerupScript.Identifier.CrystalSkull },
            { "Charm: Evil Deal", PowerupScript.Identifier.EvilDeal },
            { "Charm: Chastity Belt", PowerupScript.Identifier.ChastityBelt },
            { "Charm: Photo Book", PowerupScript.Identifier.PhotoBook },
            { "Charm: Lemon Picture", PowerupScript.Identifier.SymbolInstant_Lemon },
            { "Charm: Cherry Picture", PowerupScript.Identifier.SymbolInstant_Cherry },
            { "Charm: Clover Picture", PowerupScript.Identifier.SymbolInstant_Clover },
            { "Charm: Bell Picture", PowerupScript.Identifier.SymbolInstant_Bell },
            { "Charm: Diamond Picture", PowerupScript.Identifier.SymbolInstant_Diamond },
            { "Charm: Treasure Picture", PowerupScript.Identifier.SymbolInstant_Treasure },
            { "Charm: Seven Picture", PowerupScript.Identifier.SymbolInstant_Seven },
            { "Charm: Clicker", PowerupScript.Identifier.GeneralModCharm_Clicker },
            { "Charm: AA Batteries", PowerupScript.Identifier.GeneralModCharm_CloverBellBattery },
            { "Charm: Crystal Sphere", PowerupScript.Identifier.GeneralModCharm_CrystalSphere },
            { "Charm: Naughty Dealer", PowerupScript.Identifier.Boardgame_C_Dealer },
            { "Charm: Greedy King", PowerupScript.Identifier.GoldenKingMida },
            { "Charm: Raging Capitalist", PowerupScript.Identifier.Boardgame_M_Capitalist },
            { "Charm: Personal Trainer", PowerupScript.Identifier.PuppetPersonalTrainer },
            { "Charm: Electrician", PowerupScript.Identifier.PuppetElectrician },
            { "Charm: Fortune Teller", PowerupScript.Identifier.PuppetFortuneTeller },
            { "Charm: Golden Lemon", PowerupScript.Identifier.GoldenSymbol_Lemon },
            { "Charm: Golden Cherry", PowerupScript.Identifier.GoldenSymbol_Cherry },
            { "Charm: Golden Clover", PowerupScript.Identifier.GoldenSymbol_Clover },
            { "Charm: Golden Bell", PowerupScript.Identifier.GoldenSymbol_Bell },
            { "Charm: Golden Diamond", PowerupScript.Identifier.GoldenSymbol_Diamond },
            { "Charm: Golden Treasure", PowerupScript.Identifier.GoldenSymbol_Treasure },
            { "Charm: Golden Seven", PowerupScript.Identifier.GoldenSymbol_Seven },
            { "Charm: Bricks", PowerupScript.Identifier.Boardgame_C_Bricks },
            { "Charm: Wood", PowerupScript.Identifier.Boardgame_C_Wood },
            { "Charm: Sheep", PowerupScript.Identifier.Boardgame_C_Sheep },
            { "Charm: Wheat", PowerupScript.Identifier.Boardgame_C_Wheat },
            { "Charm: Stone", PowerupScript.Identifier.Boardgame_C_Stone },
            { "Charm: Harbor", PowerupScript.Identifier.Boardgame_C_Harbor },
            { "Charm: Thief", PowerupScript.Identifier.Boardgame_C_Thief },
            { "Charm: Wheelbarrow", PowerupScript.Identifier.Boardgame_M_Carriola },
            { "Charm: Shoe", PowerupScript.Identifier.Boardgame_M_Shoe },
            { "Charm: Thimble", PowerupScript.Identifier.Boardgame_M_Ditale },
            { "Charm: Iron", PowerupScript.Identifier.Boardgame_M_FerroDaStiro },
            { "Charm: Car", PowerupScript.Identifier.Boardgame_M_Car },
            { "Charm: Ship", PowerupScript.Identifier.Boardgame_M_Ship },
            { "Charm: Tuba Hat", PowerupScript.Identifier.Boardgame_M_Hat },
            { "Charm: Ace Of Hearts", PowerupScript.Identifier.PlayingCard_HeartsAce },
            { "Charm: Ace Of Clubs", PowerupScript.Identifier.PlayingCard_ClubsAce },
            { "Charm: Ace Of Diamonds", PowerupScript.Identifier.PlayingCard_DiamondsAce },
            { "Charm: Ace Of Spades", PowerupScript.Identifier.PlayingCard_SpadesAce },
            { "Charm: Jimbo", PowerupScript.Identifier.Jimbo },
            { "Charm: D4", PowerupScript.Identifier.Dice_4 },
            { "Charm: D6", PowerupScript.Identifier.Dice_6 },
            { "Charm: D20", PowerupScript.Identifier.Dice_20 },
            { "Charm: Angel's Hand", PowerupScript.Identifier._999_AngelHand },

            { "Charm: Eye Of God", PowerupScript.Identifier._999_EyeOfGod },
            { "Charm: Holy Spirit", PowerupScript.Identifier._999_HolySpirit },
            { "Charm: Sacred Heart", PowerupScript.Identifier._999_SacredHeart },
            { "Charm: Halo", PowerupScript.Identifier._999_Aureola },
            { "Charm: The Blood", PowerupScript.Identifier._999_TheBlood },
            { "Charm: The Body", PowerupScript.Identifier._999_TheBody },
            { "Charm: Eternity", PowerupScript.Identifier._999_Eternity },
            { "Charm: Adam's Ribcage", PowerupScript.Identifier._999_AdamsRibcage },
            { "Charm: Ophanim Wheels", PowerupScript.Identifier._999_OphanimWheels },

            { "Charm: count", PowerupScript.Identifier.count },
        };

    private static readonly Dictionary<PowerupScript.Identifier, string> ReverseMap =
        new Dictionary<PowerupScript.Identifier, string>();

    static APItemMapping()
    {
        foreach (var kvp in Map)
        {
            ReverseMap[kvp.Value] = kvp.Key;
        }
    }

    public static PowerupScript.Identifier ToPowerup(string apItemName)
    {
        if (!Map.TryGetValue(apItemName, out var id))
        {
            throw new Exception($"[AP] Unknown item name from AP: {apItemName}");
        }

        return id;
    }

    public static string IdentifierToPowerup(PowerupScript.Identifier id)
    {
        if (!ReverseMap.TryGetValue(id, out var itemName))
        {
            throw new Exception($"[AP] Unknown identifier from AP: {id}");
        }

        return itemName;
    }
    public static bool IsPowerupItem(string apItemName)
        => Map.ContainsKey(apItemName);
}
