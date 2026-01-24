using Archipelago;
using Panik;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public static class APSaveManager
{
    public static APSaveData data = new APSaveData();

    private static string SaveDirectory =>
        Path.Combine(BepInEx.Paths.ConfigPath, "AP_SAVES");

    private static string SavePath
    {
        get
        {
            if (!Directory.Exists(SaveDirectory))
                Directory.CreateDirectory(SaveDirectory);

            return Path.Combine(
                SaveDirectory,
                $"ap_{APState.SlotName}_{APState.Seed}.txt"
            );
        }
    }

    public static void Load()
    {
        data = new APSaveData();

        if (string.IsNullOrEmpty(APState.SlotName))
        {
            Plugin.Log.LogError("[AP] Cannot load save: SlotName is empty");
            return;
        }

        if (!File.Exists(SavePath))
        {
            Plugin.Log.LogInfo("[AP] No AP save found, starting fresh");

            data.UnlockedDrawers = 0;
            APState.UnlockedDrawers = 0;

            ApplyToState();
            EnsureFallbackCall();
            EnsureStartingPowerups();
            Save();

            APState.APSaveLoaded = true;
            APState.SuppressDrawerUnlockQuestion = false;
            return;
        }

        string section = "";

        foreach (string raw in File.ReadAllLines(SavePath))
        {
            string line = raw.Trim();

            if (line.Length == 0 || line.StartsWith("#"))
                continue;

            if (line.StartsWith("[") && line.EndsWith("]"))
            {
                section = line;
                continue;
            }

            switch (section)
            {
                case "[RECEIVED]":
                    data.ReceivedItems.Add(line);
                    break;

                case "[POWERUPS]":
                    data.UnlockedPowerups.Add(line);
                    break;

                case "[PHONE_ABILITIES]":
                        data.UnlockedPhoneAbilities.Add(line);
                    break;
                case "[DRAWERS]":
                    if (int.TryParse(line, out int d))
                        data.UnlockedDrawers = d;
                    break;
                case "[SKELETON]":
                    if (int.TryParse(line, out int s))
                        data.UnlockedSkeleton = s;
                    break;
                case "[FILLERS]":
                    if (int.TryParse(line, out int f))
                        data.FillersSaved = f;
                    break;
                case "[CLT]":
                    if (int.TryParse(line, out int clt))
                        data.CloverTrapSaved = clt;
                    break;
                case "[CT]":
                    if (int.TryParse(line, out int ct))
                        data.CoinTrapSaved = ct;
                    break;
                case "[LUCK]":
                    if (int.TryParse(line, out int lck))
                        data.LuckSaved = lck;
                    break;
                case "[LOCATIONS]":
                    if (long.TryParse(line, out long loc))
                        data.CheckedLocations.Add(loc);
                    break;
                case "[ITEM_INDEX]":
                    if (int.TryParse(line, out int idx))
                        data.ProcessedItemCount = idx;
                    break;
                case "[GOAL]":
                    if (data.deadlinesCompleted == 0 &&
                        int.TryParse(line, out int dc))
                    {
                        data.deadlinesCompleted = dc;
                    }
                    else
                    {
                        data.GoalCompleted = line == "1";
                    }
                    break;
                case "[KEYS]":
                    if (int.TryParse(line, out int k))
                        data.KeysCompleted = k;
                    break;

                case "[DOOR]":
                    data.DoorKeyUsed = line == "1";
                    break;
            }
        }

        APState.UnlockedDrawers = data.UnlockedDrawers;
        APState.ProcessedItemCount = data.ProcessedItemCount;

        ApplyToState();
        EnsureFallbackCall();
        EnsureStartingPowerups();
        EnsureStartingAbilities();

        APState.APSaveLoaded = true;
        APState.SuppressDrawerUnlockQuestion = false;
        Plugin.Log.LogInfo("[AP] AP save loaded");
    }
    private static void EnsureStartingAbilities()
    {
        foreach (var p in APItems.StartingAbilities)
        {
            string name = p.ToString();

            if (!data.UnlockedPhoneAbilities.Contains(name))
            {
                data.UnlockedPhoneAbilities.Add(name);
            }
        }
    }

    private static void EnsureStartingPowerups()
    {
        foreach (var p in APItems.StartingPowerups)
        {
            string name = p.ToString();

            if (!data.UnlockedPowerups.Contains(name))
            {
                data.UnlockedPowerups.Add(name);
            }
        }
    }


    private static void EnsureFallbackCall()
    {
        if (data.UnlockedPhoneAbilities.Count == 0)
            data.UnlockedPhoneAbilities.Add(APItems.AP_EMPTY.ToString());
    }


    public static void Save()
    {
        if (!APState.APSaveLoaded)
            return;

        SyncFromState();

        var sb = new StringBuilder();
        sb.AppendLine("# Clover Pit Archipelago Save");

        sb.AppendLine("[RECEIVED]");
        foreach (string s in data.ReceivedItems)
            sb.AppendLine(s);

        sb.AppendLine();
        sb.AppendLine("[POWERUPS]");
        foreach (string s in data.UnlockedPowerups)
            sb.AppendLine(s);

        sb.AppendLine();
        sb.AppendLine("[PHONE_ABILITIES]");
        foreach (string s in data.UnlockedPhoneAbilities)
            sb.AppendLine(s);

        sb.AppendLine();
        sb.AppendLine("[DRAWERS]");
        sb.AppendLine(data.UnlockedDrawers.ToString());

        sb.AppendLine();
        sb.AppendLine("[SKELETON]");
        sb.AppendLine(data.UnlockedSkeleton.ToString());

        sb.AppendLine();
        sb.AppendLine("[FILLERS]");
        sb.AppendLine(data.FillersSaved.ToString());

        sb.AppendLine();
        sb.AppendLine("[CLT]");
        sb.AppendLine(data.CloverTrapSaved.ToString());

        sb.AppendLine();
        sb.AppendLine("[CT]");
        sb.AppendLine(data.CoinTrapSaved.ToString());

        sb.AppendLine();
        sb.AppendLine("[LUCK]");
        sb.AppendLine(data.LuckSaved.ToString());

        sb.AppendLine();
        sb.AppendLine("[LOCATIONS]");
        foreach (long loc in data.CheckedLocations)
            sb.AppendLine(loc.ToString());

        sb.AppendLine();
        sb.AppendLine("[ITEM_INDEX]");
        sb.AppendLine(data.ProcessedItemCount.ToString());

        sb.AppendLine();
        sb.AppendLine("[GOAL]");
        sb.AppendLine(data.deadlinesCompleted.ToString());
        sb.AppendLine(data.GoalCompleted ? "1" : "0");

        sb.AppendLine();
        sb.AppendLine("[KEYS]");
        sb.AppendLine(data.KeysCompleted.ToString());

        sb.AppendLine();
        sb.AppendLine("[DOOR]");
        sb.AppendLine(data.DoorKeyUsed ? "1" : "0");

        File.WriteAllText(SavePath, sb.ToString());
    }

    private static void ApplyToState()
    {
        APState.UnlockedPowerups.Clear();
        APState.UnlockedPhoneAbilities.Clear();

        foreach (string s in data.UnlockedPowerups)
            if (Enum.TryParse(s, out PowerupScript.Identifier p))
                if (!APState.UnlockedPowerups.Contains(p))
                    APState.UnlockedPowerups.Add(p);

        foreach (string s in data.UnlockedPhoneAbilities)
            if (Enum.TryParse(s, out AbilityScript.Identifier a))
                    if (!APState.UnlockedPhoneAbilities.Contains(a))
                        APState.UnlockedPhoneAbilities.Add(a);

        APState.UnlockedDrawers = data.UnlockedDrawers;
        APState.UnlockedSkeleton = data.UnlockedSkeleton;
        APState.FillersSaved = data.FillersSaved;
        APState.CoinTrapSaved = data.CoinTrapSaved;
        APState.CloverTrapSaved = data.CloverTrapSaved;
        APState.LuckSaved = data.LuckSaved;

        APState.deadlinesCompleted = data.deadlinesCompleted;
        APState.goalCompleted = data.GoalCompleted;

        APState.KeysCompleted = data.KeysCompleted;
        APState.DoorKeyUsed = data.DoorKeyUsed;
    }


    private static void SyncFromState()
    {
        data.UnlockedPowerups.Clear();
        data.UnlockedPhoneAbilities.Clear();

        foreach (var p in APState.UnlockedPowerups)
            data.UnlockedPowerups.Add(p.ToString());

        foreach (var a in APState.UnlockedPhoneAbilities)
            data.UnlockedPhoneAbilities.Add(a.ToString());

        EnsureStartingPowerups();
        EnsureStartingAbilities();

        data.UnlockedDrawers = APState.UnlockedDrawers;
        data.UnlockedSkeleton = APState.UnlockedSkeleton;
        data.FillersSaved = APState.FillersSaved;
        data.CoinTrapSaved = APState.CoinTrapSaved;
        data.CloverTrapSaved = APState.CloverTrapSaved;
        data.LuckSaved = APState.LuckSaved;

        data.deadlinesCompleted = APState.deadlinesCompleted;
        data.GoalCompleted = APState.goalCompleted;

        data.ProcessedItemCount = APState.ProcessedItemCount;
        data.KeysCompleted = APState.KeysCompleted;
        data.DoorKeyUsed = APState.DoorKeyUsed;

    }

    public static bool HasReceived(string itemName)
    {
        return data.ReceivedItems.Contains(itemName);
    }

    public static void MarkReceived(string itemName)
    {
        if (!data.ReceivedItems.Add(itemName))
            return;

        Save();
    }

    public static bool IsLocationChecked(long locationId)
    => data.CheckedLocations.Contains(locationId);

    public static void MarkLocationChecked(long locationId)
    {
        if (data.CheckedLocations.Add(locationId))
            Save();
    }
}
