using Archipelago.MultiClient.Net.BounceFeatures.DeathLink;
using System.Collections.Generic;

public static class APState
{
    public static bool ShowTrapPopup { get; set; }
    public enum APDeathCause
    {
        None,
        Gameplay,
        Restart,
        DeathLink
    }
    public static class APDeathState
    {
        public static APDeathCause PendingDeathCause = APDeathCause.None;

        public static bool ShowDeathLinkUI = false;
        public static string DeathLinkSource = "";
        public static string DeathLinkCause = "";
    }

    public static DeathLinkService DeathLink;

    public static bool IsConnected = false;
    public static bool IsGrantingItem = false;
    public static bool NeedsApply = false;
    public static bool IsAPUIOpen = false;
    public static bool GameStarted = false;

    //Idk why I decided to change the case for these but I cannot be bothered to change it
    public static string goalType = "key";
    public static int deadlineGoal = 12;
    public static int deadlineAmount = 5;
    public static int deadlinesCompleted = 0;
    public static bool goalCompleted = false;

    public static bool Deathlink = false;
    public static bool DeathLinkRestart = false;

    public static int DeadlinesSentToAP = 0;

    public static int RequiredKeyEnding = 0;
    public static int KeysCompleted = 0;
    public static bool DoorKeyUsed = false;

    public static int UnlockedDrawers = 0;
    public static int DrawersReceived = 0;
    public static int UnlockedSkeleton = 0;
    public static int SkeletonsReceived = 0;
    public static int FillersSaved = 0;
    public static int FillersReceived = 0;
    public static int CloverTrapSaved = 0;
    public static int CloverTrapReceived = 0;
    public static int CoinTrapSaved = 0;
    public static int CoinTrapReceived = 0;
    public static int LuckSaved = 0;
    public static int LuckReceived = 0;

    public static bool SuppressDrawerUnlockQuestion = false;

    public static float CloverTrapPercent = 0.20f;
    public static float CoinTrapPercent = 0.20f;

    public static bool APSaveLoaded = false;

    public static string Host;
    public static string SlotName;
    public static string Password;

    public static string Seed;

    public static int ProcessedItemCount = 0;
    public static bool LocationsResynced = false;


    public static readonly HashSet<PowerupScript.Identifier> UnlockedPowerups
        = new HashSet<PowerupScript.Identifier>();

    public static readonly HashSet<AbilityScript.Identifier> UnlockedPhoneAbilities
        = new HashSet<AbilityScript.Identifier>();

    public static void Reset()
    {
        IsConnected = false;
        APSaveLoaded = false;
        NeedsApply = false;
        IsGrantingItem = false;
        LocationsResynced = false;

        APState.DrawersReceived = 0;
        APState.SkeletonsReceived = 0;
        APState.CloverTrapReceived = 0;
        APState.CoinTrapReceived = 0;
        APState.FillersReceived = 0;
        APState.LuckReceived = 0;

        SlotName = "";
        Seed = "";

        UnlockedDrawers = 0;
        UnlockedPowerups.Clear();
        UnlockedPhoneAbilities.Clear();
    }
}
