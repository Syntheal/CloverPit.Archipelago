using HarmonyLib;
using System;
using System.Reflection;
using UnityEngine;

public static class APTrapExecutor
{
    private static readonly FieldInfo CloverField =
        AccessTools.Field(typeof(GameplayData), "cloverTickets");

    private static readonly FieldInfo CoinsField =
        AccessTools.Field(typeof(GameplayData), "coins");

    public static void TriggerCloverTrap()
    {
        var gd = GameplayData.Instance;
        if (gd == null)
            return;

        long current = (long)CloverField.GetValue(gd);
        if (current <= 0)
            return;

        long removed = CalculatePercentLoss(
            current,
            APState.CloverTrapPercent
        );

        GameplayData.CloverTicketsSet(current - removed);

        Plugin.Log.LogWarning(
            $"[AP][TRAP] Clover trap! Lost {removed} clovers ({APState.CloverTrapPercent:P0})"
        );
    }

    public static void TriggerCoinTrap()
    {
        var gd = GameplayData.Instance;
        if (gd == null)
            return;

        object currentObj = CoinsField.GetValue(gd);
        if (currentObj == null)
            return;

        dynamic current = currentObj;
        if (current <= 0)
            return;

        dynamic removed = CalculatePercentLoss(
            current,
            APState.CoinTrapPercent
        );

        GameplayData.CoinsSet(current - removed);

        Plugin.Log.LogWarning(
            $"[AP][TRAP] Coin trap! Lost {removed} coins ({APState.CoinTrapPercent:P0})"
        );
    }

    private static long CalculatePercentLoss(dynamic value, float percent)
    {
        if (percent <= 0f)
            return 0;

        dynamic removed = value * percent;

        if (removed < 1)
            return 1;

        return (long)removed;
    }
}
