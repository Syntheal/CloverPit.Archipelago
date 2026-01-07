using HarmonyLib;
using Panik;
using System;
using System.Reflection;
using UnityEngine;
using static SlotMachineScript;

[HarmonyPatch(typeof(SlotMachineScript))]
public static class SlotMachinePatch
{
    [HarmonyPrefix]
    [HarmonyPatch(nameof(SlotMachineScript.HasJackpot))]
    public static void Prefix_HasJackpot(SlotMachineScript __instance)
    {
        if (HasJackpot(__instance))
        {
            SymbolScript.Kind jackpotKind = __instance.Symbol_GetAtPosition(0, 0);

            long jackpotLocationId = APLocations.GetJackpotLocation(jackpotKind);

            if (jackpotLocationId > 0)
            {
                APClient.SendLocationCheck(jackpotLocationId);
                Plugin.Log.LogInfo($"[AP] Sent GET JACKPOT location: {jackpotLocationId} for symbol {jackpotKind}!");
            }
        }
    }

    private static bool HasJackpot(SlotMachineScript instance)
    {
        SymbolScript.Kind firstSymbol = instance.Symbol_GetAtPosition(0, 0);

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (instance.Symbol_GetAtPosition(j, i) != firstSymbol)
                {
                    return false;
                }
            }
        }

        return true;
    }

    [HarmonyPrefix]
    [HarmonyPatch(nameof(SlotMachineScript.Has666))]
    public static void Prefix_Has666(SlotMachineScript __instance)
    {
        if (Has666(__instance))
        {
            SymbolScript.Kind jackpotKind = __instance.Symbol_GetAtPosition(1, 1);

            long jackpotLocationId = APLocations.GetJackpotLocation(jackpotKind);

            if (jackpotLocationId > 0)
            {
                APClient.SendLocationCheck(jackpotLocationId);
                Plugin.Log.LogInfo($"[AP] Sent 666 jackpot location: {jackpotLocationId} for symbol {jackpotKind}!");
            }
        }
    }

    [HarmonyPrefix]
    [HarmonyPatch(nameof(SlotMachineScript.Has999))]
    public static void Prefix_Has999(SlotMachineScript __instance)
    {
        if (Has999(__instance))
        {
            SymbolScript.Kind jackpotKind = __instance.Symbol_GetAtPosition(1, 1); 

            long jackpotLocationId = APLocations.GetJackpotLocation(jackpotKind);

            if (jackpotLocationId > 0)
            {
                APClient.SendLocationCheck(jackpotLocationId);
                Plugin.Log.LogInfo($"[AP] Sent 999 jackpot location: {jackpotLocationId} for symbol {jackpotKind}!");
            }
        }
    }

    private static bool Has666(SlotMachineScript instance)
    {
        if (instance == null) return false;
        return instance.Symbol_GetAtPosition(1,1) == SymbolScript.Kind.six &&
               instance.Symbol_GetAtPosition(1,2) == SymbolScript.Kind.six &&
               instance.Symbol_GetAtPosition(1,3) == SymbolScript.Kind.six;
    }

    private static bool Has999(SlotMachineScript instance)
    {
        if (instance == null) return false;
        return instance.Symbol_GetAtPosition(1,1) == SymbolScript.Kind.nine &&
               instance.Symbol_GetAtPosition(1,2) == SymbolScript.Kind.nine &&
               instance.Symbol_GetAtPosition(1,3) == SymbolScript.Kind.nine;
    }

    [HarmonyPrefix]
    [HarmonyPatch("Awake")]
    private static void Prefix_Awake(SlotMachineScript __instance)
    {
        EventInfo onModifierScoredEvent = typeof(SlotMachineScript).GetEvent("OnModifierScored");
        if (onModifierScoredEvent != null)
        {
            MethodInfo handlerMethod = typeof(SlotMachinePatch)
                .GetMethod(nameof(HandleOnModifierScored), BindingFlags.NonPublic | BindingFlags.Static);

            Delegate handler = Delegate.CreateDelegate(onModifierScoredEvent.EventHandlerType, null, handlerMethod);
            onModifierScoredEvent.AddEventHandler(__instance, handler);
        }
    }

    private static void HandleOnModifierScored(PatternInfos pattern)
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        foreach (SymbolScript symbol in SymbolScript.scoringSymbols)
        {
            if (symbol == null)
                continue;

            switch (symbol.ModifierGet())
            {
                case SymbolScript.Modifier.instantReward:
                    CompleteOnce(APLocations.HIT_MODIFIER_INTEREST, "Instant Reward");
                    break;
                case SymbolScript.Modifier.cloverTicket:
                    CompleteOnce(APLocations.HIT_MODIFIER_CLOVER_TICKET, "Clover Ticket");
                    break;
                case SymbolScript.Modifier.golden:
                    CompleteOnce(APLocations.HIT_MODIFIER_GOLDEN, "Golden");
                    break;
                case SymbolScript.Modifier.repetition:
                    CompleteOnce(APLocations.HIT_MODIFIER_REPETITION, "Repetition");
                    break;
                case SymbolScript.Modifier.battery:
                    CompleteOnce(APLocations.HIT_MODIFIER_BATTERY, "Battery");
                    break;
                case SymbolScript.Modifier.chain:
                    CompleteOnce(APLocations.HIT_MODIFIER_CHAIN, "Chain");
                    break;
            }
        }
    }

    private static void CompleteOnce(long location, string name)
    {
        if (APLocationManager.IsChecked(location))
            return;

        APLocationManager.Complete(location);
        APSaveManager.Save();

        Plugin.Log.LogInfo($"[AP] Modifier hit (spin): {name}");
    }
}
