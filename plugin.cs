using BepInEx;
using BepInEx.Logging;
using CloverPit.Archipelago.APUI;
using HarmonyLib;
using UnityEngine;

[BepInPlugin("cloverpit.archipelago", "CloverPit Archipelago", "1.2.0")]
public class Plugin : BaseUnityPlugin
{
    internal static ManualLogSource Log;
    private Harmony harmony;

    private void Awake()
    {
        var go = new GameObject("APConnectUI");
        DontDestroyOnLoad(go);
        go.AddComponent<APConnectUI>();

        var console = new GameObject("APConsoleUI");
        DontDestroyOnLoad(console);
        console.AddComponent<APConsoleUI>();

        var goal = new GameObject("APGoalUI");
        DontDestroyOnLoad(goal);
        goal.AddComponent<APGoalUI>();

        var trap = new GameObject("APTrapUI");
        DontDestroyOnLoad(trap);
        trap.AddComponent<APUITrapPopup>();

        var legend = new GameObject("APSuffixLegend");
        DontDestroyOnLoad(legend);
        legend.AddComponent<APSuffixLegendUI>();

        var checklist = new GameObject("APChecklist");
        DontDestroyOnLoad(checklist);
        checklist.AddComponent<APCallChecklistUI>();

        Log = Logger;
        Log.LogInfo("CloverPit Archipelago loaded");

        harmony = new Harmony("cloverpit.archipelago");
        harmony.PatchAll();

        APLocations.PopulateLocationNames();
        APClient.ItemReceived += item =>
        {
            string receiveKey = item.ReceiveKey;
            Log.LogDebug($"[AP] Received: {item.ItemName}");
            if (APSaveManager.HasReceived(receiveKey) && !item.ItemName.StartsWith("Progressive") && !item.ItemName.EndsWith("Trap") && !item.ItemName.Contains(" Tickets") && !item.ItemName.Contains("Card Pack"))
            {
                Log.LogDebug($"[AP] Skipped duplicate item: {item.ItemName} ({receiveKey})");
                return;
            }

            APState.IsGrantingItem = true;

            try
            {
                if (APPhoneAbilityMapping.IsPhoneAbility(item.ItemName))
                {
                    var abilityId = APPhoneAbilityMapping.ToAbility(item.ItemName);

                    if (!APState.UnlockedPhoneAbilities.Contains(abilityId))
                    {
                        if (APState.UnlockedPhoneAbilities.Add(abilityId))
                        {
                            Log.LogInfo($"[AP] Unlocked phone ability: {abilityId}");
                        }
                    }
                    else
                    {
                        Log.LogWarning($"[AP] Ability {abilityId} is already unlocked.");
                    }
                }
                else if (APItemMapping.IsPowerupItem(item.ItemName))
                {
                    var powerupId = APItemMapping.ToPowerup(item.ItemName);
                    if (APState.UnlockedPowerups.Add(powerupId))
                    {
                        PowerupUnlocker.Unlock(powerupId);
                        Log.LogInfo($"[AP] Unlocked powerup: {powerupId}");
                    }
                }
                else if (item.ItemName.StartsWith("Progressive Skeleton"))
                    APSkeletonProgression.GrantNext();
                else if (item.ItemName.StartsWith("Progressive Luck"))
                {
                    APState.LuckReceived++;
                    if (APState.LuckReceived > APState.LuckSaved && APState.LuckSaved < 5)
                        APState.LuckSaved++;
                }
                else if (item.ItemName.StartsWith("Progressive Drawer"))
                {
                    APState.SuppressDrawerUnlockQuestion = true;
                    APDrawerProgression.GrantNext(item.PlayerName);
                    APState.SuppressDrawerUnlockQuestion = false;
                }
                else if (item.ItemName.StartsWith("Coin Trap"))
                {
                    APState.CoinTrapReceived++;
                    if (APState.CoinTrapReceived > APState.CoinTrapSaved)
                    {
                        APTrapExecutor.TriggerCoinTrap(item.PlayerName);
                        APState.CoinTrapSaved++;
                    }
                }
                else if (item.ItemName.StartsWith("Clover Trap"))
                {
                    APState.CloverTrapReceived++;
                    if (APState.CloverTrapReceived > APState.CloverTrapSaved)
                    {
                        APTrapExecutor.TriggerCloverTrap(item.PlayerName);
                        APState.CloverTrapSaved++;
                    }
                }
                else if (item.ItemName.StartsWith("5x Clover"))
                {
                    APState.FillersReceived++;
                    if (APState.FillersReceived > APState.FillersSaved)
                    {
                        GameplayData.CloverTicketsAdd(5, addToStats: true);
                        APConsoleLog.Log("5 Clover Tickets from " + item.PlayerName, APConsoleLineType.ItemReceived);
                        Log.LogInfo("[AP] Granted 5 Clover Tickets for item: " + item.ItemName);
                        APState.FillersSaved++;
                    }
                }
                else if (item.ItemName.StartsWith("Bonus Deadline"))
                {
                    APState.BonusTicketsReceived++;
                    if (APState.BonusTicketsReceived > APState.BonusTicketsSaved)
                    {
                        APConsoleLog.Log("Bonus Deadline Tickets from " + item.PlayerName, APConsoleLineType.ItemReceived);
                        APState.BonusTicketsSaved++;
                    }
                }
                else if (item.ItemName.StartsWith("Bonus Starting"))
                {
                    APState.StartTicektsReceived++;
                    if (APState.StartTicektsReceived > APState.StartTicketsSaved)
                    {
                        APConsoleLog.Log("Bonus Starting Tickets from " + item.PlayerName, APConsoleLineType.ItemReceived);
                        APState.StartTicketsSaved++;
                    }
                }
                else if (item.ItemName.Contains("Card Pack"))
                {
                    APState.PacksReceived++;
                    if (APState.PacksReceived > APState.PacksSaved)
                    {
                        APCardUnlocker.GrantPack(item.PlayerName,item.ItemName);
                        APState.PacksSaved++;
                    }
                }
                else
                    Log.LogWarning($"[AP] Item received: {item.ItemName} does nothing");

                APSaveManager.MarkReceived(receiveKey);
            }
            finally
            {
                APState.IsGrantingItem = false;
            }
        };
        APClient.Connected += GreyOutNewRunButtonsPatch.OnAPConnected;
    }
}

