using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

[BepInPlugin("cloverpit.archipelago", "CloverPit Archipelago", "1.0.3")]
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

        var trap = new GameObject("APTrapUI");
        DontDestroyOnLoad(trap);
        trap.AddComponent<APUITrapPopup>();

        Log = Logger;
        Log.LogInfo("CloverPit Archipelago loaded");

        harmony = new Harmony("cloverpit.archipelago");
        harmony.PatchAll();

        APLocations.PopulateLocationNames();

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

                    if (!APState.UnlockedPhoneAbilities.Contains(abilityId))
                    {
                        if (APState.UnlockedPhoneAbilities.Add(abilityId))
                        {
                            //APAbilityInitializeAllPatch.AddAbilityBack(abilityId);
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
                else if (item.ItemName.StartsWith("Progressive Drawer"))
                {
                    APState.SuppressDrawerUnlockQuestion = true;
                    APDrawerProgression.GrantNext(item.PlayerName);
                    APState.SuppressDrawerUnlockQuestion = false;
                }
                else if (item.ItemName.StartsWith("Coin Trap"))
                    APTrapExecutor.TriggerCoinTrap(item.PlayerName);
                else if (item.ItemName.StartsWith("Clover Trap"))
                    APTrapExecutor.TriggerCloverTrap(item.PlayerName);
                else if (item.ItemName.StartsWith("10x Clover"))
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

