using Archipelago.MultiClient.Net.BounceFeatures.DeathLink;
using System.Collections.Generic;

public static class APState
{
    public enum APDeathCause
    {
        None,
        Gameplay,
        Restart,
        DeathLink
    }
    public static bool DeathLinkKillPending = false;
    public static class APDeathState
    {
        public static APDeathCause PendingDeathCause = APDeathCause.None;
        public static bool DeathLinkKillPending = false;

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
    public static int deadlineAmount = 0;
    public static int deadlinesCompleted = 0;
    public static bool goalCompleted = false;

    public static bool Deathlink = false;
    public static bool DeathLinkRestart = false;

    public static int DeadlinesSentToAP = 0;

    public static int RequiredKeyEnding = 0;
    public static int KeysCompleted = 0;
    public static bool DoorKeyUsed = false;

    public static int UnlockedDrawers = 0;
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

        SlotName = "";
        Seed = "";

        UnlockedDrawers = 0;
        UnlockedPowerups.Clear();
        UnlockedPhoneAbilities.Clear();
    }
}
