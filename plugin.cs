using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

[BepInPlugin("cloverpit.archipelago", "CloverPit Archipelago", "0.1.0")]
public class Plugin : BaseUnityPlugin
{
    internal static ManualLogSource Log;
    private Harmony harmony;

    private void Awake()
    {
        var go = new GameObject("APConnectUI");
        DontDestroyOnLoad(go);
        go.AddComponent<APConnectUI>();

        var dlui = new GameObject("APDeathLinkUI");
        DontDestroyOnLoad(dlui);
        dlui.AddComponent<APDeathLinkUI>();

        var console = new GameObject("APConsoleUI");
        DontDestroyOnLoad(console);
        console.AddComponent<APConsoleUI>();

        Log = Logger;
        Log.LogInfo("CloverPit Archipelago loaded");

        harmony = new Harmony("cloverpit.archipelago");
        harmony.PatchAll();

        APClient.ItemReceived += item =>
        {
            string receiveKey = item.ReceiveKey;

            if (APSaveManager.HasReceived(receiveKey))
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
                    if (APState.UnlockedPhoneAbilities.Add(abilityId))
                        Log.LogInfo($"[AP] Unlocked phone ability: {abilityId}");
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
                else if (item.ItemName.StartsWith("Progressive Drawer"))
                {
                    APState.SuppressDrawerUnlockQuestion = true;
                    APDrawerProgression.GrantNext(item.PlayerName);
                    APState.SuppressDrawerUnlockQuestion = false;
                }
                else if (item.ItemName.StartsWith("Coin Trap") || item.ItemId.ToString().StartsWith("1236"))
                    APTrapExecutor.TriggerCoinTrap();
                else if (item.ItemName.StartsWith("Clover Trap") || item.ItemId.ToString().StartsWith("1235"))
                    APTrapExecutor.TriggerCloverTrap();
                else if (item.ItemName.StartsWith("10x Clover") || item.ItemId.ToString().StartsWith("1236"))
                {
                    GameplayData.CloverTicketsAdd(10, addToStats: true);
                    Log.LogInfo("[AP] Granted 10 Clover Tickets for item: " + item.ItemName);
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

