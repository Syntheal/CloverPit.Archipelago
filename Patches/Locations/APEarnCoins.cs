using HarmonyLib;
using System.Reflection;
using System;
using System.Numerics;

[HarmonyPatch(typeof(SlotMachineScript))]
public static class APEarnCoins
{
    private static readonly BigInteger TEN_K = new BigInteger(10_000);
    private static readonly BigInteger ONE_HUNDRED_K = new BigInteger(100_000);
    private static readonly BigInteger ONE_M = new BigInteger(1_000_000);
    private static readonly BigInteger TEN_M = new BigInteger(10_000_000);
    private static readonly BigInteger HUNDRED_M = new BigInteger(100_000_000);

    private static BigInteger coinsAtSpinStart;

    [HarmonyPrefix]
    [HarmonyPatch("Awake")]
    public static void Prefix_Awake(SlotMachineScript __instance)
    {
        SubscribeToEvents(__instance);
    }

    private static void SubscribeToEvents(SlotMachineScript instance)
    {
        EventInfo onSpinStartEvent = typeof(SlotMachineScript).GetEvent("OnSpinStart");
        if (onSpinStartEvent != null)
        {
            MethodInfo spinStartHandler = typeof(APEarnCoins).GetMethod("HandleOnSpinStart", BindingFlags.Static | BindingFlags.NonPublic);
            Delegate spinStartDelegate = Delegate.CreateDelegate(onSpinStartEvent.EventHandlerType, null, spinStartHandler);
            onSpinStartEvent.AddEventHandler(instance, spinStartDelegate);
        }

        EventInfo onScoreEvalEndEvent = typeof(SlotMachineScript).GetEvent("OnScoreEvaluationEnd");
        if (onScoreEvalEndEvent != null)
        {
            MethodInfo scoreEvalEndHandler = typeof(APEarnCoins).GetMethod("HandleOnScoreEvaluationEnd", BindingFlags.Static | BindingFlags.NonPublic);
            Delegate scoreEvalEndDelegate = Delegate.CreateDelegate(onScoreEvalEndEvent.EventHandlerType, null, scoreEvalEndHandler);
            onScoreEvalEndEvent.AddEventHandler(instance, scoreEvalEndDelegate);
        }
    }

    private static void HandleOnSpinStart()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        coinsAtSpinStart = GameplayData.RoundEarnedCoinsGet();
    }

    private static void HandleOnScoreEvaluationEnd()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        BigInteger coinsAfterSpin = GameplayData.RoundEarnedCoinsGet();
        BigInteger earnedThisSpin = coinsAfterSpin - coinsAtSpinStart;

        if (earnedThisSpin <= 0)
            return;

        CheckSpinMilestones(earnedThisSpin);
    }

    private static void CheckSpinMilestones(BigInteger earned)
    {
        TryComplete(earned, 10_000, APLocations.EARN_10K_SPIN, "Earn 10k coins in one spin");
        TryComplete(earned, 100_000, APLocations.EARN_100K_SPIN, "Earn 100k coins in one spin");
        TryComplete(earned, 1_000_000, APLocations.EARN_1M_SPIN, "Earn 1m coins in one spin");
        TryComplete(earned, 10_000_000, APLocations.EARN_10M_SPIN, "Earn 10m coins in one spin");
        TryComplete(earned, 100_000_000, APLocations.EARN_100M_SPIN, "Earn 100m coins in one spin");
    }

    private static void TryComplete(BigInteger earned, BigInteger threshold, long location, string name)
    {
        if (earned < threshold)
            return;

        if (APLocationManager.IsChecked(location))
            return;

        APLocationManager.Complete(location);
        APSaveManager.Save();

        Plugin.Log.LogInfo($"[AP] Spin milestone hit: {name} ({earned})");
    }

    [HarmonyPostfix]
    [HarmonyPatch("Set_NoMoreSpins")]
    public static void Postfix_OnRoundEnd()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        BigInteger earnedThisRound = GameplayData.RoundEarnedCoinsGet();

        CheckThreshold(
            earnedThisRound,
            TEN_K,
            APLocations.EARN_10K_ROUND,
            "Earned 10k coins in one round"
        );

        CheckThreshold(
            earnedThisRound,
            ONE_HUNDRED_K,
            APLocations.EARN_100K_ROUND,
            "Earned 100k coins in one round"
        );

        CheckThreshold(
            earnedThisRound,
            ONE_M,
            APLocations.EARN_1M_ROUND,
            "Earned 1m coins in one round"
        );

        CheckThreshold(
            earnedThisRound,
            TEN_M,
            APLocations.EARN_10M_ROUND,
            "Earned 10m coins in one round"
        );

        CheckThreshold(
            earnedThisRound,
            HUNDRED_M,
            APLocations.EARN_100M_ROUND,
            "Earned 100m coins in one round"
        );
    }

    private static void CheckThreshold(
        BigInteger earned,
        BigInteger threshold,
        long location,
        string logMessage)
    {
        if (earned < threshold)
            return;

        if (APLocationManager.IsChecked(location))
            return;

        APLocationManager.Complete(location);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] " + logMessage);
    }
}
