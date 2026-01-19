using HarmonyLib;
using Panik;
using System.Numerics;

[HarmonyPatch(typeof(PowerupScript))]
public static class APCharmPatch
{
    [HarmonyPostfix]
    [HarmonyPatch("Trigger_FakeCoin")]
    public static void Postfix_FakeCoin()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (GameplayData.RndActivationFailsafe_FakeCoin != 0)
            return;

        if (APLocationManager.IsChecked(APLocations.ACTIVATE_FAKE_COIN))
            return;

        APLocationManager.Complete(APLocations.ACTIVATE_FAKE_COIN);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Fake Coin activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("Trigger_Ankh")]
    public static void Postfix_Trigger_Ankh()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (APLocationManager.IsChecked(APLocations.ACTIVATE_ANKH))
            return;

        APLocationManager.Complete(APLocations.ACTIVATE_ANKH);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Ankh activated");
    }

    private static int _lastHamsaUpsideUnlock = 0;
    [HarmonyPostfix]
    [HarmonyPatch("Trigger_UpsideHamsa")]
    public static void Postfix_Trigger_UpsideHamsa()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (Data.game.UnlockSteps_HamsaUpside <= _lastHamsaUpsideUnlock)
            return;

        _lastHamsaUpsideUnlock = Data.game.UnlockSteps_HamsaUpside;

        if (APLocationManager.IsChecked(APLocations.ACTIVATE_HAMSA))
            return;

        APLocationManager.Complete(APLocations.ACTIVATE_HAMSA);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Hamsa activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("Trigger_InvertedHamsa")]
    public static void Postfix_Trigger_InvertedHamsa()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (GameplayData.SpinsLeftGet() > 0)
            return;

        if (APLocationManager.IsChecked(APLocations.ACTIVATE_ROTATED_HAMSA))
            return;

        APLocationManager.Complete(APLocations.ACTIVATE_ROTATED_HAMSA);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Rotated Hamsa activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("Trigger_LuckyCat")]
    public static void Postfix_Trigger_LuckyCat()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (SlotMachineScript.GetPatternsCount() < 3)
            return;

        if (APLocationManager.IsChecked(APLocations.ACTIVATE_LUCKYCAT))
            return;

        APLocationManager.Complete(APLocations.ACTIVATE_LUCKYCAT);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Lucky Cat activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("Trigger_LuckyCatFat")]
    public static void Postfix_Trigger_LuckyCatFat()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (SlotMachineScript.GetPatternsCount() < 7)
            return;

        if (APLocationManager.IsChecked(APLocations.ACTIVATE_LUCKYCATFAT))
            return;

        APLocationManager.Complete(APLocations.ACTIVATE_LUCKYCATFAT);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Chonky Cat activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("Trigger_LuckyCatSwole")]
    public static void Postfix_Trigger_LuckyCatSwole()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (SlotMachineScript.GetPatternsCount() < 15)
            return;

        if (APLocationManager.IsChecked(APLocations.ACTIVATE_LUCKYCATSWOLE))
            return;

        APLocationManager.Complete(APLocations.ACTIVATE_LUCKYCATSWOLE);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Swole Cat activated");
    }

    private static BigInteger _lastPoopBeetleBonus = BigInteger.Zero;
    [HarmonyPostfix]
    [HarmonyPatch("Trigger_PoopBeetle")]
    public static void Postfix_Trigger_PoopBeetle()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        BigInteger current = GameplayData.Powerup_PoopBeetle_SymbolsIncreaseN_Get();

        if (current <= _lastPoopBeetleBonus)
            return;

        _lastPoopBeetleBonus = current;

        if (APLocationManager.IsChecked(APLocations.ACTIVATE_DUNG_BEETLE))
            return;

        APLocationManager.Complete(APLocations.ACTIVATE_DUNG_BEETLE);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Dung Beetle activated");
    }


    [HarmonyPostfix]
    [HarmonyPatch("Trigger_OneTrickPony")]
    public static void Postfix_Trigger_OneTrickPony()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (GameplayData.Powerup_OneTrickPony_TargetSpinIndexGet() != GameplayData.SpinsLeftGet())
            return;

        if (APLocationManager.IsChecked(APLocations.ACTIVATE_PONY))
            return;

        APLocationManager.Complete(APLocations.ACTIVATE_PONY);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] One Trick Pony activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("Trigger_TarotDeck")]
    public static void Postfix_Trigger_TarotDeck()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (APLocationManager.IsChecked(APLocations.ACTIVATE_TAROT_DECK))
            return;

        APLocationManager.Complete(APLocations.ACTIVATE_TAROT_DECK);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Tarot Deck activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("Trigger_Pentacle")]
    public static void Postfix_Trigger_Pentacle()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (SlotMachineScript.GetPatternsCount() < 5)
            return;

        if (APLocationManager.IsChecked(APLocations.ACTIVATE_PENTACLE))
            return;

        APLocationManager.Complete(APLocations.ACTIVATE_PENTACLE);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Pentacle activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("Trigger_CrankGenerator")]
    public static void Postfix_Trigger_CrankGenerator()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (GameplayData.RndActivationFailsafe_CrankGenerator != 0)
            return;

        if (APLocationManager.IsChecked(APLocations.ACTIVATE_DYNAMO))
            return;

        APLocationManager.Complete(APLocations.ACTIVATE_DYNAMO);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Dynamo activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("Trigger_YellowStar")]
    public static void Postfix_Trigger_YellowStar()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (GameplayData.SpinsWithoutReward_Get() < 1)
            return;

        if (APLocationManager.IsChecked(APLocations.ACTIVATE_YELLOW_STAR))
            return;

        APLocationManager.Complete(APLocations.ACTIVATE_YELLOW_STAR);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Little Star activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("DiscA_TriggerTry")]
    public static void Postfix_DiscA_FinalizeSpin()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (GameplayData.Powerup_DiscA_SpinsCounter != 7)
            return;

        if (APLocationManager.IsChecked(APLocations.ACTIVATE_DISC_A))
            return;

        APLocationManager.Complete(APLocations.ACTIVATE_DISC_A);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Disc A activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("DiscB_TriggerTry")]
    public static void Postfix_DiscB_TriggerTry()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (GameplayData.Powerup_DiscB_SpinsCounter != 7)
            return;

        if (APLocationManager.IsChecked(APLocations.ACTIVATE_DISC_B))
            return;

        APLocationManager.Complete(APLocations.ACTIVATE_DISC_B);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Disc B activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("DiscC_TriggerTry")]
    public static void Postfix_DiscC_FinalizeSpin()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (GameplayData.Powerup_DiscC_SpinsCounter != 7)
            return;

        if (APLocationManager.IsChecked(APLocations.ACTIVATE_DISC_C))
            return;

        APLocationManager.Complete(APLocations.ACTIVATE_DISC_C);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Disc C activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("PFunc_OnEquip_Cigarettes")]
    public static void Postfix_Smoke_Cigs()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (APLocationManager.IsChecked(APLocations.SMOKE_CIGS))
            return;

        APLocationManager.Complete(APLocations.SMOKE_CIGS);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Smoked Some Cigarettes");
    }

    [HarmonyPostfix]
    [HarmonyPatch("Trigger_PotatoPower")]
    public static void Postfix_Trigger_PotatoBattery()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (GameplayData.SpinsWithoutReward_Get() < 2)
            return;

        if (APLocationManager.IsChecked(APLocations.ACTIVATE_POTATO))
            return;

        APLocationManager.Complete(APLocations.ACTIVATE_POTATO);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Potato Battery activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("Trigger_ToyTrain")]
    public static void Postfix_Trigger_ToyTrain()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (GameplayData.SpinsWithoutReward_Get() < 2)
            return;

        if (APLocationManager.IsChecked(APLocations.ACTIVATE_TOY_TRAIN))
            return;

        APLocationManager.Complete(APLocations.ACTIVATE_TOY_TRAIN);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Toy Train activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("Trigger_SteamLocomotive")]
    public static void Postfix_Trigger_SteamLocomotive()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (GameplayData.SpinsWithoutReward_Get() < 3)
            return;

        if (APLocationManager.IsChecked(APLocations.ACTIVATE_STEAM_LOCOMOTIVE))
            return;

        APLocationManager.Complete(APLocations.ACTIVATE_STEAM_LOCOMOTIVE);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Steam Locomotive activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("Trigger_DieselLocomotive")]
    public static void Postfix_Trigger_DieselLocomotive()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (GameplayData.SpinsWithoutReward_Get() < 3)
            return;

        if (APLocationManager.IsChecked(APLocations.ACTIVATE_DIESEL_LOCOMOTIVE))
            return;

        APLocationManager.Complete(APLocations.ACTIVATE_DIESEL_LOCOMOTIVE);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Diesel Locomotive activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("Trigger_CloverPot")]
    public static void Postfix_Trigger_CloverPot()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (PowerupScript.CloverPotTicketsBonus(false, true) <= 0)
            return;

        if (APLocationManager.IsChecked(APLocations.ACTIVATE_CLOVER_POT))
            return;

        APLocationManager.Complete(APLocations.ACTIVATE_CLOVER_POT);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Clover Pot activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("FieldOfClovers_Trigger")]
    public static void Postfix_FieldOfClovers_Trigger()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (APLocationManager.IsChecked(APLocations.ACTIVATE_CLOVER_FIELD))
            return;

        APLocationManager.Complete(APLocations.ACTIVATE_CLOVER_FIELD);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Clover Field activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("Trigger_Shrooms")]
    public static void Postfix_Trigger_Shrooms()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (SlotMachineScript.GetPatternsCount() < 3)
            return;

        if (APLocationManager.IsChecked(APLocations.ACTIVATE_SHROOMS))
            return;

        APLocationManager.Complete(APLocations.ACTIVATE_SHROOMS);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Mushrooms activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("Trigger_VineShroom")]
    public static void Postfix_Trigger_VineShroom()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (SlotMachineScript.GetPatternsCount() < 5)
            return;

        if (APLocationManager.IsChecked(APLocations.ACTIVATE_VINE_SHROOM))
            return;

        APLocationManager.Complete(APLocations.ACTIVATE_VINE_SHROOM);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Vine's Soup activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("Trigger_GiantShroom")]
    public static void Postfix_Trigger_GiantShroom()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (SlotMachineScript.GetPatternsCount() < 5)
            return;

        if (APLocationManager.IsChecked(APLocations.ACTIVATE_GIANT_SHROOM))
            return;

        APLocationManager.Complete(APLocations.ACTIVATE_GIANT_SHROOM);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Very Big Mushroom activated");
    }

    [HarmonyPrefix]
    [HarmonyPatch("PFunc_OnThrowAway_HoleCircle")]
    public static void Prefix_HoleCircle(
        PowerupScript powerup,
        ref PowerupScript.Identifier __state)
    {
        __state = GameplayData.PowerupHoleCircle_CharmGet();
    }

    [HarmonyPostfix]
    [HarmonyPatch("PFunc_OnThrowAway_HoleCircle")]
    public static void Postfix_HoleCircle(
        PowerupScript powerup,
        PowerupScript.Identifier __state)
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (__state == PowerupScript.Identifier.undefined || __state == PowerupScript.Identifier.count)
            return;

        if (APLocationManager.IsChecked(APLocations.DISCARD_ABYSSU))
            return;

        APLocationManager.Complete(APLocations.DISCARD_ABYSSU);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Abyssu thrown away");
    }

    [HarmonyPrefix]
    [HarmonyPatch("PFunc_OnThrowAway_HoleRomboid")]
    public static void Prefix_HoleRomboid(
    PowerupScript powerup,
    ref PowerupScript.Identifier __state)
    {
        __state = GameplayData.PowerupHoleRomboid_CharmGet();
    }

    [HarmonyPostfix]
    [HarmonyPatch("PFunc_OnThrowAway_HoleRomboid")]
    public static void Postfix_HoleRomboid(
    PowerupScript powerup,
    PowerupScript.Identifier __state)
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (__state == PowerupScript.Identifier.undefined ||
            __state == PowerupScript.Identifier.count)
            return;

        if (APLocationManager.IsChecked(APLocations.DISCARD_VORAGO))
            return;

        APLocationManager.Complete(APLocations.DISCARD_VORAGO);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Vorago thrown away");
    }

    [HarmonyPrefix]
    [HarmonyPatch("PFunc_OnThrowAway_HoleCross")]
    public static void Prefix_HoleCross(
    PowerupScript powerup,
    ref AbilityScript.Identifier __state)
    {
        __state = GameplayData.PowerupHoleCross_AbilityGet();
    }

    [HarmonyPostfix]
    [HarmonyPatch("PFunc_OnThrowAway_HoleCross")]
    public static void Postfix_HoleCross(
    PowerupScript powerup,
    AbilityScript.Identifier __state)
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (__state == AbilityScript.Identifier.undefined ||
            __state == AbilityScript.Identifier.count)
            return;

        if (APLocationManager.IsChecked(APLocations.DISCARD_BARATHRUM))
            return;

        APLocationManager.Complete(APLocations.DISCARD_BARATHRUM);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Barathrum thrown away");
    }

    [HarmonyPostfix]
    [HarmonyPatch("Trigger_Rorschach")]
    public static void Postfix_Trigger_Rorschach()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        PatternScript.Kind biggest = SlotMachineScript.GetBiggestPatternScored();
        if (biggest == PatternScript.Kind.undefined)
            return;

        if (PatternScript.GetElementsCount(biggest) < 4)
            return;

        if (APLocationManager.IsChecked(APLocations.ACTIVATE_STAIN))
            return;

        APLocationManager.Complete(APLocations.ACTIVATE_STAIN);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Stain activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("Trigger_AbstractPainting")]
    public static void Postfix_Trigger_AbstractPainting()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        PatternScript.Kind biggest = SlotMachineScript.GetBiggestPatternScored();
        if (biggest == PatternScript.Kind.undefined)
            return;

        if (PatternScript.GetElementsCount(biggest) < 5)
            return;

        if (APLocationManager.IsChecked(APLocations.ACTIVATE_ABSTRACT_PAINTING))
            return;

        APLocationManager.Complete(APLocations.ACTIVATE_ABSTRACT_PAINTING);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Abstract Painting activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("Trigger_Pareidolia")]
    public static void Postfix_Trigger_Pareidolia()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (SlotMachineScript.GetBiggestPatternScored() != PatternScript.Kind.eye)
            return;

        if (!SlotMachineScript.IsAllSamePattern())
            return;

        if (APLocationManager.IsChecked(APLocations.ACTIVATE_PAREIDOLIA))
            return;

        APLocationManager.Complete(APLocations.ACTIVATE_PAREIDOLIA);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Pareidolia activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("Trigger_Hourglass")]
    public static void Postfix_Trigger_Hourglass()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (APLocationManager.IsChecked(APLocations.ACTIVATE_HOURGLASS))
            return;

        APLocationManager.Complete(APLocations.ACTIVATE_HOURGLASS);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Hourglass activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("Trigger_7SinsStone")]
    public static void Postfix_Trigger_7SinsStone()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (SlotMachineScript.SymbolsCount(SymbolScript.Kind.seven) < 7)
            return;

        if (APLocationManager.IsChecked(APLocations.ACTIVATE_7_SINS_STONE))
            return;

        APLocationManager.Complete(APLocations.ACTIVATE_7_SINS_STONE);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Seven Sins Stone activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("Trigger_CloverBell")]
    public static void Postfix_Trigger_CloverBell()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (SlotMachineScript.GetPatternsCount_BySymbol(SymbolScript.Kind.clover) <= 0)
            return;

        if (SlotMachineScript.GetPatternsCount_BySymbol(SymbolScript.Kind.bell) <= 0)
            return;

        if (APLocationManager.IsChecked(APLocations.ACTIVATE_CLOVER_BELL))
            return;

        APLocationManager.Complete(APLocations.ACTIVATE_CLOVER_BELL);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Clover Bell activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("PFunc_Trigger_SpicyPepper_Red")]
    public static void Postfix_Trigger_SpicyPepper_Red()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (GameplayData.RndActivationFailsafe_RedPepper != 0)
            return;

        if (APLocationManager.IsChecked(APLocations.ACTIVATE_RED_PEPPER))
            return;

        APLocationManager.Complete(APLocations.ACTIVATE_RED_PEPPER);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Red Pepper activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("PFunc_Trigger_SpicyPepper_Green")]
    public static void Postfix_Trigger_SpicyPepper_Green()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (GameplayData.RndActivationFailsafe_GreenPepper != 0)
            return;

        if (APLocationManager.IsChecked(APLocations.ACTIVATE_GREEN_PEPPER))
            return;

        APLocationManager.Complete(APLocations.ACTIVATE_GREEN_PEPPER);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Green Pepper activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("TriggerRottenPepper")]
    public static void Postfix_Trigger_RottenPepper()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (GameplayData.Powerup_RottenPepper_LuckBonusGet() <= 0f)
            return;

        if (GameplayData.RndActivationFailsafe_RottenPepper != 0)
            return;

        if (APLocationManager.IsChecked(APLocations.ACTIVATE_ROTTEN_PEPPER))
            return;

        APLocationManager.Complete(APLocations.ACTIVATE_ROTTEN_PEPPER);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Rotten Pepper activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("TriggerBellPepper")]
    public static void Postfix_Trigger_BellPepper()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (GameplayData.Powerup_BellPepper_LuckBonusGet() <= 0f)
            return;

        if (GameplayData.RndActivationFailsafe_BellPepper != 0)
            return;

        if (APLocationManager.IsChecked(APLocations.ACTIVATE_BELL_PEPPER))
            return;

        APLocationManager.Complete(APLocations.ACTIVATE_BELL_PEPPER);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Bell Pepper activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("TriggerGoldenPepper")]
    public static void Postfix_Trigger_GoldenPepper()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (GameplayData.Powerup_GoldenPepper_LuckBonusGet() <= 0f)
            return;

        if (GameplayData.RndActivationFailsafe_GoldenPepper != 0)
            return;

        if (APLocationManager.IsChecked(APLocations.ACTIVATE_GOLDEN_PEPPER))
            return;

        APLocationManager.Complete(APLocations.ACTIVATE_GOLDEN_PEPPER);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Golden Pepper activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("Trigger_DevilHorn")]
    public static void Postfix_Trigger_DevilHorn()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (!SlotMachineScript.HasShown666())
            return;

        if (APLocationManager.IsChecked(APLocations.ACTIVATE_DEVIL_HORN))
            return;

        APLocationManager.Complete(APLocations.ACTIVATE_DEVIL_HORN);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Demon's Horn activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("Trigger_HolyBible")]
    public static void Postfix_Trigger_HolyBible()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (!SlotMachineScript.Has666())
            return;

        if (APLocationManager.IsChecked(APLocations.ACTIVATE_HOLY_BIBLE))
            return;

        APLocationManager.Complete(APLocations.ACTIVATE_HOLY_BIBLE);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Holy Bible activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("Trigger_Baphomet")]
    public static void Postfix_Trigger_Baphomet()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (!SlotMachineScript.HasShown666())
            return;

        if (APLocationManager.IsChecked(APLocations.ACTIVATE_BAPHOMET))
            return;

        APLocationManager.Complete(APLocations.ACTIVATE_BAPHOMET);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Baphomet activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("Trigger_Rosary")]
    public static void Postfix_Trigger_Rosary()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (!SlotMachineScript.Has666())
            return;

        if (GameplayData.RndActivationFailsafe_Rosary != 0)
            return;

        if (APLocationManager.IsChecked(APLocations.ACTIVATE_ROSARY))
            return;

        APLocationManager.Complete(APLocations.ACTIVATE_ROSARY);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Rosary activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("Trigger_BookOfShadows")]
    public static void Postfix_Trigger_BookOfShadows()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (!SlotMachineScript.HasShown666())
            return;

        if (APLocationManager.IsChecked(APLocations.ACTIVATE_BOOK_OF_SHADOWS))
            return;

        APLocationManager.Complete(APLocations.ACTIVATE_BOOK_OF_SHADOWS);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Book of Shadows activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("Trigger_Gabibbh")]
    public static void Postfix_Trigger_Gabibbh()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (!SlotMachineScript.HasShown666())
            return;

        if (APLocationManager.IsChecked(APLocations.ACTIVATE_GABIBBH))
            return;

        APLocationManager.Complete(APLocations.ACTIVATE_GABIBBH);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Gabibbh activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("Trigger_MysticalTomato")]
    public static void Postfix_Trigger_MysticalTomato()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (!SlotMachineScript.HasShown666())
            return;

        if (APLocationManager.IsChecked(APLocations.ACTIVATE_MYSTICAL_TOMATO))
            return;

        APLocationManager.Complete(APLocations.ACTIVATE_MYSTICAL_TOMATO);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Magical Tomato activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("Trigger_RitualBell")]
    public static void Postfix_Trigger_RitualBell()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (!SlotMachineScript.HasShown666())
            return;

        if (APLocationManager.IsChecked(APLocations.ACTIVATE_RITUAL_BELL))
            return;

        APLocationManager.Complete(APLocations.ACTIVATE_RITUAL_BELL);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Ritual Bell activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("Trigger_CrystalSkull")]
    public static void Postfix_Trigger_CrystalSkull()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (!SlotMachineScript.HasShown666())
            return;

        if (APLocationManager.IsChecked(APLocations.ACTIVATE_CRYSTAL_SKULL))
            return;

        APLocationManager.Complete(APLocations.ACTIVATE_CRYSTAL_SKULL);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Crystal Skull activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("Trigger_AceOfHearts")]
    public static void Postfix_Trigger_AceOfHearts()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (SlotMachineScript.GetPatternsCount() < 3)
            return;

        if (APLocationManager.IsChecked(APLocations.ACTIVATE_ACE_HEARTS))
            return;

        APLocationManager.Complete(APLocations.ACTIVATE_ACE_HEARTS);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Ace of Hearts activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("Trigger_AceOfDiamonds")]
    public static void Postfix_Trigger_AceOfDiamonds()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        PatternScript.Kind biggest = SlotMachineScript.GetBiggestPatternScored();
        if (biggest == PatternScript.Kind.undefined ||
            PatternScript.GetElementsCount(biggest) < 4)
            return;

        if (APLocationManager.IsChecked(APLocations.ACTIVATE_ACE_DIAMONDS))
            return;

        APLocationManager.Complete(APLocations.ACTIVATE_ACE_DIAMONDS);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Ace of Diamonds activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("EyeOfGodTrigger")]
    public static void Postfix_EyeOfGod()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (APLocationManager.IsChecked(APLocations.ACTIVATE_EYE_OF_GOD))
            return;

        APLocationManager.Complete(APLocations.ACTIVATE_EYE_OF_GOD);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Eye Of God activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("HolySpiritTrigger")]
    public static void Postfix_HolySpirit()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (APLocationManager.IsChecked(APLocations.ACTIVATE_HOLY_SPIRIT))
            return;

        APLocationManager.Complete(APLocations.ACTIVATE_HOLY_SPIRIT);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Holy Spirit activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("EternityTrigger")]
    public static void Postfix_Eternity()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (APLocationManager.IsChecked(APLocations.ACTIVATE_ETERNITY))
            return;

        APLocationManager.Complete(APLocations.ACTIVATE_ETERNITY);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Eternity activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("AdamsRibCageTrigger")]
    public static void Postfix_AdamsRibcage()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (APLocationManager.IsChecked(APLocations.ACTIVATE_ADAMS_RIBCAGE))
            return;

        APLocationManager.Complete(APLocations.ACTIVATE_ADAMS_RIBCAGE);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Adam's Ribcage activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("OphanimWheelsTrigger")]
    public static void Postfix_OphanimWheels()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (APLocationManager.IsChecked(APLocations.ACTIVATE_OPHANIM_WHEELS))
            return;

        APLocationManager.Complete(APLocations.ACTIVATE_OPHANIM_WHEELS);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Ophanim Wheels activated");
    }

    private static long SkeletonToLocation(PowerupScript.Identifier id)
    {
        switch (id)
        {
            case PowerupScript.Identifier.Skeleton_Arm1:
                return APLocations.SKELETON_ARM_1;

            case PowerupScript.Identifier.Skeleton_Arm2:
                return APLocations.SKELETON_ARM_2;

            case PowerupScript.Identifier.Skeleton_Head:
                return APLocations.SKELETON_HEAD;

            case PowerupScript.Identifier.Skeleton_Leg1:
                return APLocations.SKELETON_LEG_1;

            case PowerupScript.Identifier.Skeleton_Leg2:
                return APLocations.SKELETON_LEG_2;

            default:
                return -1;
        }
    }

    [HarmonyPostfix]
    [HarmonyPatch("Equip")]
    public static void Postfix_Equip(
        PowerupScript.Identifier identifier,
        bool isInitializationCall,
        bool putInNotBoughtListIfFull,
        bool __result)
    {
        if (!__result)
            return;

        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        long location = SkeletonToLocation(identifier);
        if (location < 0)
            return;

        if (APLocationManager.IsChecked(location))
            return;

        APLocationManager.Complete(location);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Skeleton equipped: " + identifier);
    }

    [HarmonyPostfix]
    [HarmonyPatch("D20_Trigger")]
    public static void Postfix_D20_Trigger()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (APLocationManager.IsChecked(APLocations.ACTIVATE_DICE_20))
            return;

        APLocationManager.Complete(APLocations.ACTIVATE_DICE_20);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Dice 20 triggered");
    }

    [HarmonyPostfix]
    [HarmonyPatch("Dice6_TriggerTry")]
    public static void Postfix_Dice6_TriggerTry()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (APLocationManager.IsChecked(APLocations.ACTIVATE_DICE_6))
            return;

        APLocationManager.Complete(APLocations.ACTIVATE_DICE_6);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Dice 6 triggered");
    }

    [HarmonyPostfix]
    [HarmonyPatch("Dice4_TriggerTry")]
    public static void Postfix_Dice4_TriggerTry(bool __result)
    {
        if (!__result)
            return;

        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (APLocationManager.IsChecked(APLocations.ACTIVATE_DICE_4))
            return;

        APLocationManager.Complete(APLocations.ACTIVATE_DICE_4);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Dice 4 triggered");
    }

}
