using HarmonyLib;
using System;
using System.Numerics;
using System.Reflection;
using UnityEngine;

public static class APTrapExecutor
{
    private static readonly FieldInfo CloverField = AccessTools.Field(typeof(GameplayData), "cloverTickets");
    private static readonly FieldInfo CoinsField = AccessTools.Field(typeof(GameplayData), "coins");

    public static void TriggerCloverTrap(string senderName)
    {
        var gd = GameplayData.Instance;
        if (gd == null)
            return;

        if (!APState.GameStarted)
            return;

        long current = (long)CloverField.GetValue(gd);
        if (current <= 0)
            return;

        long removed = CalculatePercentLoss(current, APState.CloverTrapPercent);

        if (removed > 0)
        {
            GameplayData.CloverTicketsSet(current - removed);
            Plugin.Log.LogWarning(
                $"[AP][TRAP] Clover trap! Lost {removed} clovers ({APState.CloverTrapPercent:P0})"
            );

            APUITrapPopup.Instance.ShowTrapInfo($"{removed} Clover Tickets", senderName);
        }
        else
        {
            Plugin.Log.LogWarning("[AP][TRAP] Clover trap did not remove any clovers due to small loss.");
        }
    }

    public static void TriggerCoinTrap(string senderName)
    {
        var gd = GameplayData.Instance;
        if (gd == null)
            return;

        if (!APState.GameStarted)
            return;

        object currentObj = CoinsField.GetValue(gd);
        if (currentObj == null)
            return;

        BigInteger current = (BigInteger)currentObj;
        if (current <= 0)
            return;

        BigInteger removed = CalculatePercentLoss(current, APState.CoinTrapPercent);

        if (removed > 0)
        {
            GameplayData.CoinsSet(current - removed);

            string formattedRemoved = removed.ToString("N0");

            Plugin.Log.LogWarning(
                $"[AP][TRAP] Coin trap! Lost {formattedRemoved} coins ({APState.CoinTrapPercent:P0})"
            );

            APUITrapPopup.Instance.ShowTrapInfo($"{formattedRemoved} Coins", senderName);
        }
        else
        {
            Plugin.Log.LogWarning("[AP][TRAP] Coin trap did not remove any coins due to small loss.");
        }
    }

    private static long CalculatePercentLoss(long value, float percent)
    {
        if (percent <= 0f)
            return 0;

        decimal removed = value * (decimal)percent;
        removed = Math.Round(removed);

        if (removed < 1)
            removed = 1;

        return (long)removed;
    }
    private static BigInteger CalculatePercentLoss(BigInteger value, float percent)
    {
        if (percent <= 0f)
            return 0;

        decimal removed = (decimal)value * (decimal)percent;
        removed = Math.Round(removed);

        if (removed < 1)
            removed = 1;

        return (BigInteger)removed;
    }
}
