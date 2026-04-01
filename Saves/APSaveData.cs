using System.Collections.Generic;

public class APSaveData
{
    public long Seed;
    public int ProcessedItemCount;

    public HashSet<long> CheckedLocations =
        new HashSet<long>();

    public HashSet<string> ReceivedItems =
        new HashSet<string>();

    public HashSet<string> UnlockedPowerups =
        new HashSet<string>();

    public HashSet<string> UnlockedPhoneAbilities =
        new HashSet<string>();

    public HashSet<string> unlockedModifiers = 
        new HashSet<string>();

    public HashSet<string> beatModifiers =
        new HashSet<string>();

    public int UnlockedDrawers = 0;
    public int UnlockedSkeleton = 0;
    public int FillersSaved = 0;
    public int CloverTrapSaved = 0;
    public int CoinTrapSaved = 0;
    public int LuckSaved = 0;
    public int StartSaved = 0;
    public int BonusSaved = 0;
    public int PacksSaved = 0;
    public static int CloverTrapCount;
    public static int CoinTrapCount;
    public bool GoalCompleted = false;
    public int deadlinesCompleted = 0;
    public int KeysCompleted = 0;
    public bool DoorKeyUsed = false;
}
