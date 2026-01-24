using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[HarmonyPatch(typeof(PowerupScript))]
internal static class RedButtonCharms
{
    [HarmonyPostfix]
    [HarmonyPatch("GoldenHorseShoe_RedButtonCallback_OnPress")]
    private static void Postfix_GoldenHorseShoe(PowerupScript powerup)
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (APLocationManager.IsChecked(APLocations.RED_BUTTON_GOLDEN_HORSESHOE))
            return;

        APLocationManager.Complete(APLocations.RED_BUTTON_GOLDEN_HORSESHOE);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Red Button Golden Horse Shoe activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("RedCrystal_RedButtonCallback_OnPress")]
    private static void Postfix_RedShinyRock(PowerupScript powerup)
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (APLocationManager.IsChecked(APLocations.RED_BUTTON_RED_CRYSTAL))
            return;

        APLocationManager.Complete(APLocations.RED_BUTTON_RED_CRYSTAL);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Red Button Red Shiny Rock activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("GoldenHandMidaTouch_RedButtonCallback_OnPress")]
    private static void Postfix_MidasTouch(PowerupScript powerup)
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (APLocationManager.IsChecked(APLocations.RED_BUTTON_MIDAS_TOUCH))
            return;

        APLocationManager.Complete(APLocations.RED_BUTTON_MIDAS_TOUCH);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Red Button Midas Touch activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("PoopJar_RedButtonCallback_OnPress")]
    private static void Postfix_Number2(PowerupScript powerup)
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (APLocationManager.IsChecked(APLocations.RED_BUTTON_NUMBER2))
            return;

        APLocationManager.Complete(APLocations.RED_BUTTON_NUMBER2);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Red Button Number 2 activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("PissJar_RedButtonCallback_OnPress")]
    private static void Postfix_Number1(PowerupScript powerup)
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (APLocationManager.IsChecked(APLocations.RED_BUTTON_NUMBER1))
            return;

        APLocationManager.Complete(APLocations.RED_BUTTON_NUMBER1);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Red Button Number 1 activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("RingBell_RedButtonCallback_OnPress")]
    private static void Postfix_RingBell(PowerupScript powerup)
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (APLocationManager.IsChecked(APLocations.RED_BUTTON_RING_BELL))
            return;

        APLocationManager.Complete(APLocations.RED_BUTTON_RING_BELL);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Red Button Ring Bell activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("WeirdClock_RedButtonCallback_OnPress")]
    private static void Postfix_WierdClock(PowerupScript powerup)
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (APLocationManager.IsChecked(APLocations.RED_BUTTON_WEIRD_CLOCK))
            return;

        APLocationManager.Complete(APLocations.RED_BUTTON_WEIRD_CLOCK);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Red Button Weird Clock activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("AncientCoin_RedButtonCallback_OnPress")]
    private static void Postfix_AncientCoin(PowerupScript powerup)
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (APLocationManager.IsChecked(APLocations.RED_BUTTON_ANCIENT_COIN))
            return;

        APLocationManager.Complete(APLocations.RED_BUTTON_ANCIENT_COIN);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Red Button Ancient Coin activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("Cross_RedButtonCallback_OnPress")]
    private static void Postfix_Cross(PowerupScript powerup)
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (APLocationManager.IsChecked(APLocations.RED_BUTTON_CROSS))
            return;

        APLocationManager.Complete(APLocations.RED_BUTTON_CROSS);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Red Button Cross activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("PossessedPhone_RedButtonCallback_OnPress")]
    private static void Postfix_PossesedPhone(PowerupScript powerup)
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (APLocationManager.IsChecked(APLocations.RED_BUTTON_PHONE))
            return;

        APLocationManager.Complete(APLocations.RED_BUTTON_PHONE);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Red Button Possessed Cell Phone activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("LemonPicture_RedButtonCallback_OnPress")]
    private static void Postfix_LemonPicture(PowerupScript powerup)
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (APLocationManager.IsChecked(APLocations.RED_BUTTON_LEMON_PICTURE))
            return;

        APLocationManager.Complete(APLocations.RED_BUTTON_LEMON_PICTURE);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Red Button Lemon Picture activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("CherryPicture_RedButtonCallback_OnPress")]
    private static void Postfix_CherryPicture(PowerupScript powerup)
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (APLocationManager.IsChecked(APLocations.RED_BUTTON_CHERRY_PICTURE))
            return;

        APLocationManager.Complete(APLocations.RED_BUTTON_CHERRY_PICTURE);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Red Button Cherry Picture activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("CloverPicture_RedButtonCallback_OnPress")]
    private static void Postfix_CloverPicture(PowerupScript powerup)
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (APLocationManager.IsChecked(APLocations.RED_BUTTON_CLOVER_PICTURE))
            return;

        APLocationManager.Complete(APLocations.RED_BUTTON_CLOVER_PICTURE);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Red Button Clover Picture activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("BellPicture_RedButtonCallback_OnPress")]
    private static void Postfix_BellPicture(PowerupScript powerup)
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (APLocationManager.IsChecked(APLocations.RED_BUTTON_BELL_PICTURE))
            return;

        APLocationManager.Complete(APLocations.RED_BUTTON_BELL_PICTURE);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Red Button Bell Picture activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("DiamondPicture_RedButtonCallback_OnPress")]
    private static void Postfix_DiamondPicture(PowerupScript powerup)
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (APLocationManager.IsChecked(APLocations.RED_BUTTON_DIAMOND_PICTURE))
            return;

        APLocationManager.Complete(APLocations.RED_BUTTON_DIAMOND_PICTURE);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Red Button Diamond Picture activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("CoinsPicture_RedButtonCallback_OnPress")]
    private static void Postfix_TreasurePicture(PowerupScript powerup)
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (APLocationManager.IsChecked(APLocations.RED_BUTTON_TREASURE_PICTURE))
            return;

        APLocationManager.Complete(APLocations.RED_BUTTON_TREASURE_PICTURE);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Red Button Treasure Picture activated");
    }

    [HarmonyPostfix]
    [HarmonyPatch("SevenPicture_RedButtonCallback_OnPress")]
    private static void Postfix_SevenPicture(PowerupScript powerup)
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (APLocationManager.IsChecked(APLocations.RED_BUTTON_SEVEN_PICTURE))
            return;

        APLocationManager.Complete(APLocations.RED_BUTTON_SEVEN_PICTURE);
        APSaveManager.Save();

        Plugin.Log.LogInfo("[AP] Red Button Seven Picture activated");
    }
}
