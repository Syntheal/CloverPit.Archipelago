using Panik;
using System.Collections.Generic;
using UnityEngine;

public static class APDrawerStateApplier
{
    public static void Apply()
    {
        if (!APState.IsConnected)
            return;

        if (DrawersScript.instance == null)
        {
            Plugin.Log.LogDebug("[AP] Drawer apply skipped: DrawersScript not ready");
            return;
        }

        int unlocked = APState.UnlockedDrawers;

        for (int i = 0; i < unlocked && i < DrawersScript.MAX_DRAWERS; i++)
        {
            DrawersScript.Unlock(i);
            Data.game.drawersUnlocked[i] = true;
        }

        Plugin.Log.LogInfo(
            $"[AP] Applied drawer state ({unlocked}/{DrawersScript.MAX_DRAWERS})"
        );
    }
}
