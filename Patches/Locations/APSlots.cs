using HarmonyLib;
using static SlotMachineScript;

[HarmonyPatch(typeof(SlotMachineScript), "Start")]
public static class APPatternLogBootstrap
{
    static void Postfix(SlotMachineScript __instance)
    {
        __instance.OnPatternEvaluationStart += OnPatternScored;
    }

    static void OnPatternScored(PatternInfos info)
    {
        APLocationManager.Complete(APLocations.WIN_SOMETHING);
        switch (info.patternKind)
        {
            case (PatternScript.Kind.horizontal3):
                APLocationManager.Complete(APLocations.SEND_HORIZONTAL3);
                break;
            case (PatternScript.Kind.vertical3):
                APLocationManager.Complete(APLocations.SEND_VERTICAL3);
                break;
            case (PatternScript.Kind.diagonal3):
                APLocationManager.Complete(APLocations.SEND_DIAGONAL3);
                break;
            case (PatternScript.Kind.horizontal4):
                APLocationManager.Complete(APLocations.SEND_HORIZONTAL4);
                break;
            case (PatternScript.Kind.horizontal5):
                APLocationManager.Complete(APLocations.SEND_HORIZONTAL5);
                break;
            case (PatternScript.Kind.pyramid):
                APLocationManager.Complete(APLocations.SEND_PYRAMID);
                break;
            case (PatternScript.Kind.pyramidInverted):
                APLocationManager.Complete(APLocations.SEND_INVERTEDPYRAMID);
                break;
            case (PatternScript.Kind.triangle):
                APLocationManager.Complete(APLocations.SEND_TRIANGLÆ);
                break;
            case (PatternScript.Kind.triangleInverted):
                APLocationManager.Complete(APLocations.SEND_INVERTEDTRIANGLE);
                break;
            case (PatternScript.Kind.eye):
                APLocationManager.Complete(APLocations.SEND_EYE);
                break;
            case (PatternScript.Kind.jackpot):
                APLocationManager.Complete(APLocations.SEND_JACKPOT);
                break;
        }

        switch (info.symbolKind)
        {
            case (SymbolScript.Kind.lemon):
                APLocationManager.Complete(APLocations.SEND_LEMONS);
            break;
            case (SymbolScript.Kind.cherry):
                APLocationManager.Complete(APLocations.SEND_CHERRY);
            break;
            case (SymbolScript.Kind.clover):
                APLocationManager.Complete(APLocations.SEND_CLOVER);
            break;
            case (SymbolScript.Kind.bell):
                APLocationManager.Complete(APLocations.SEND_BELL);
            break;
            case (SymbolScript.Kind.diamond):
                APLocationManager.Complete(APLocations.SEND_DIAMOND);
            break;
            case (SymbolScript.Kind.coins):
                APLocationManager.Complete(APLocations.SEND_COINS);
            break;
            case (SymbolScript.Kind.seven):
                APLocationManager.Complete(APLocations.SEND_SEVEN);
            break;
        }
    }
}
