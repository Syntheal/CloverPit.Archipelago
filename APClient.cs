using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.BounceFeatures.DeathLink;
using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Helpers;
using Archipelago.MultiClient.Net.Models;
using Archipelago.MultiClient.Net.Packets;
using HarmonyLib;
using Panik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using UnityEngine;

public static class APClient
{
    public static ArchipelagoSession Session;

    public static event Action Connected;
    public static event Action<APItem> ItemReceived;

    public static bool IsConnected => Session?.Socket.Connected ?? false;

    public static async Task Connect(string host, string slot, string password)
    {
        if (APState.IsConnected)
            return;

        APState.SuppressDrawerUnlockQuestion = true;

        APState.Host = host;
        APState.SlotName = slot;
        APState.Password = password;

        Session = ArchipelagoSessionFactory.CreateSession(host);

        Session.Socket.SocketClosed += OnSocketClosed;
        Session.Items.ItemReceived += OnItemInfoReceived;

        await Session.ConnectAsync();

        LoginResult result = await Session.LoginAsync(
            game: "CloverPit",
            name: slot,
            itemsHandlingFlags: ItemsHandlingFlags.AllItems,
            password: password
        );

        if (result is LoginFailure failure)
        {
            Plugin.Log.LogError("[AP] Login failed: " + failure.ToString());
            return;
        }

        var success = (LoginSuccessful)result;

        APState.IsConnected = true;
        ReadSlotData(success.SlotData);
        APState.DeathLink = Session.CreateDeathLinkService();

        if (APState.Deathlink)
        {
            APState.DeathLink.EnableDeathLink();
            APState.DeathLink.OnDeathLinkReceived += OnDeathLinkReceived;

            Plugin.Log.LogInfo("[AP] DeathLink enabled");
        }

        APState.Seed = Session.RoomState.Seed;

        APSaveManager.Load();
        APLocationManager.SyncCheckedLocations();

        APState.NeedsApply = true;

        Plugin.Log.LogInfo($"[AP] Connected to {host} as {slot}");
        APConsoleLog.Log($"Connected as {slot}", APConsoleLineType.Normal);

        Connected?.Invoke();
    }


    private static void OnSocketClosed(string reason)
    {
        Plugin.Log.LogWarning($"[AP] Disconnected: {reason}");
        APConsoleLog.Log($"Disconnected: {reason}", APConsoleLineType.Warning);
        APState.IsAPUIOpen = true;
        APState.Reset();
    }
    private static void ReadSlotData(IDictionary<string, object> slotData)
    {
        try
        {
            if (slotData.TryGetValue("clover_trap_percent", out var clover))
            {
                if (clover is Int64 cloverValue)
                {
                    APState.CloverTrapPercent = (float)(cloverValue / 100.0);
                }
                else
                {
                    Plugin.Log.LogWarning("[AP] Clover Trap Percent value is not an Int64.");
                }
            }
            else
            {
                APState.CloverTrapPercent = 0.20f; 
                Plugin.Log.LogWarning("[AP] Clover Trap Percent not found, using default.");
            }

            if (slotData.TryGetValue("coin_trap_percent", out var coin))
            {
                if (coin is Int64 coinValue)
                {
                    APState.CoinTrapPercent = (float)(coinValue / 100.0);
                }
                else
                {
                    Plugin.Log.LogWarning("[AP] Coin Trap Percent value is not an Int64.");
                }
            }
            else
            {
                APState.CoinTrapPercent = 0.20f; 
                Plugin.Log.LogWarning("[AP] Coin Trap Percent not found, using default.");
            }

            if (slotData.TryGetValue("goal_type", out var goalObj))
            {
                if (goalObj is Int64 goalTypeLongValue)
                {
                    APState.goalType = goalTypeLongValue == 0 ? "key" : "deadline"; 
                }
                else
                {
                    Plugin.Log.LogWarning("[AP] GoalType is neither an int nor Int64.");
                    APState.goalType = "not found";
                }
            }
            else
            {
                APState.goalType = "not found";
            }

            if (APState.goalType == "key")
            {
                if (slotData.TryGetValue("ending_type", out var endingObj))
                {
                    if (endingObj is Int64 endingTypeValue)
                    {
                        APState.RequiredKeyEnding = endingTypeValue == 1 ? 1 : 0;
                    }
                    else
                    {
                        APState.RequiredKeyEnding = 0; 
                        Plugin.Log.LogWarning("[AP] EndingType not found, using default.");
                    }
                }
                else
                {
                    APState.RequiredKeyEnding = 0;
                    Plugin.Log.LogWarning("[AP] EndingType not found, using default.");
                }
            }

            if (APState.goalType == "deadline")
            {
                APState.deadlineGoal =
                    slotData.TryGetValue("deadline_goal", out var dg) && dg is int deadlineGoal
                    ? deadlineGoal
                    : 16;

                APState.deadlineAmount =
                    slotData.TryGetValue("deadline_amount", out var da) && da is int deadlineAmount
                    ? deadlineAmount
                    : 12;
            }

            APState.Deathlink = slotData.TryGetValue("deathlink", out var deathlink) && Convert.ToString(deathlink) == "1";
            APState.DeathLinkRestart = slotData.TryGetValue("does_restart_deathlink", out var deathLinkRestart) && Convert.ToString(deathLinkRestart) == "1";

            Plugin.Log.LogInfo(
                $"[AP] SlotData loaded | Goal={APState.goalType}, Ending={APState.RequiredKeyEnding}, " +
                $"DeadlineGoal={APState.deadlineGoal}, DeadlineAmount={APState.deadlineAmount}, " +
                $"Clover={APState.CloverTrapPercent:P0}, Coin={APState.CoinTrapPercent:P0}, " +
                $"Deathlink={APState.Deathlink}, Does restarts count for deathlink={APState.DeathLinkRestart}"
            );
        }
        catch (Exception ex)
        {
            Plugin.Log.LogError("[AP] Error while reading SlotData: " + ex.Message);
        }
    }


    private static void OnItemInfoReceived(ReceivedItemsHelper helper)
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        int alreadyProcessed = APSaveManager.data.ProcessedItemCount;
        int index = 0;

        while (helper.PeekItem() != null)
        {
            ItemInfo info = helper.DequeueItem();

            if (index < alreadyProcessed)
            {
                index++;
                continue;
            }

            var item = new APItem
            {
                ItemId = info.ItemId,
                Player = info.Player.Slot,
                ItemName = info.ItemName ?? info.ItemDisplayName,
                PlayerName = Session.Players.GetPlayerName(info.Player.Slot)
            };

            GrantItem(item);
            APSaveManager.Save();

            index++;
        }
    }

    private static void GrantItem(APItem item)
    {
        APState.IsGrantingItem = true;

        try
        {
            APConsoleLog.Log(
                $"Received {item.ItemName} from {item.PlayerName}",
                APConsoleLineType.ItemReceived
            );
            ItemReceived?.Invoke(item);
            APSaveManager.data.ProcessedItemCount++;
        }
        finally
        {
            APState.IsGrantingItem = false;
        }
    }

    public static void SendLocationCheck(long locationId)
    {
        if (!APState.IsConnected)
            return;

        Session.Locations.CompleteLocationChecks(locationId);

        APConsoleLog.Log(
            $"Sent location check {APLocations.GetLocationName(locationId)}",
            APConsoleLineType.ItemSent
        );
    }

    public static void RequestResync()
    {
        if (!APState.IsConnected)
            return;

        Session.Socket.SendPacket(new SyncPacket());
        Plugin.Log.LogInfo("[AP] Requested item sync");
    }

    public static void Disconnect()
    {
        if (!APState.IsConnected)
            return;

        Session.Socket.DisconnectAsync();

        APState.Reset();
    }

    public static void SendGoalCompletion()
    {
        if (!APState.IsConnected)
            return;

        Session.Socket.SendPacket(new StatusUpdatePacket
        {
            Status = ArchipelagoClientState.ClientGoal
        });

        Plugin.Log.LogInfo("[AP] Goal completed!");
    }

    public static void SendDeathLink(string cause)
    {
        if (!IsConnected || !APState.Deathlink || APState.DeathLink == null)
            return;

        APState.DeathLink.SendDeathLink(
            new DeathLink(APState.SlotName, cause)
        );

        Plugin.Log.LogInfo($"[AP] Sent DeathLink: {cause}");
        APConsoleLog.Log(
            $"DeathLink sent: {cause}",
            APConsoleLineType.DeathLink
        );
    }

    private static void OnDeathLinkReceived(DeathLink deathLink)
    {
        if (!APState.Deathlink)
            return;

        Plugin.Log.LogWarning(
            $"[AP] DeathLink received from {deathLink.Source}: {deathLink.Cause}"
        );

        APState.APDeathState.DeathLinkSource = deathLink.Source;
        APState.APDeathState.DeathLinkCause =
            string.IsNullOrEmpty(deathLink.Cause)
                ? "DeathLink"
                : deathLink.Cause;

        APState.APDeathState.ShowDeathLinkUI = true;

        APDeathLinkKiller.KillFromDeathLink();
        APState.APDeathState.PendingDeathCause = APState.APDeathCause.DeathLink;
        APState.APDeathState.DeathLinkKillPending = true;
    }

}

public class APItem
{
    public long ItemId;
    public int Player;
    public string ItemName;
    public string PlayerName;
    public string ReceiveKey => $"{ItemId}:{Player}";
}


public static class APItems
{
    public static readonly HashSet<PowerupScript.Identifier> ManagedPowerups;

    public static readonly HashSet<AbilityScript.Identifier> ManagedAbilites;

    public static readonly HashSet<PowerupScript.Identifier> StartingPowerups =
        new HashSet<PowerupScript.Identifier>
        {
            PowerupScript.Identifier.FakeCoin,
            PowerupScript.Identifier.MoneyBriefCase,
        };

    public static readonly HashSet<AbilityScript.Identifier> StartingAbilities =
    new HashSet<AbilityScript.Identifier>
    {
            //AbilityScript.Identifier.extraSpace
    };

    public static readonly AbilityScript.Identifier AP_EMPTY =
    (AbilityScript.Identifier)(-1);

    static APItems()
    {
        ManagedPowerups = new HashSet<PowerupScript.Identifier>();

        foreach (PowerupScript.Identifier id in
            Enum.GetValues(typeof(PowerupScript.Identifier)))
        {
            if (id == PowerupScript.Identifier.undefined)
                continue;

            ManagedPowerups.Add(id);
        }

        ManagedAbilites = new HashSet<AbilityScript.Identifier>();

        foreach (AbilityScript.Identifier id in
            Enum.GetValues(typeof(AbilityScript.Identifier)))
        {
            if (id == AbilityScript.Identifier.undefined)
                continue;

            ManagedAbilites.Add(id);
        }
    }
}


public static class APFreezeHelper
{
    public static void FreezePlayer()
    {
        foreach (var p in PlayerScript.list)
        {
            if (p == null)
                continue;

            var rb = p.GetComponent<Rigidbody>();
            if (rb == null)
                continue;

            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
