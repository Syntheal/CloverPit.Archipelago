using Panik;
using UnityEngine.SceneManagement;

public static class APDrawerProgression
{
    private const int MAX_DRAWERS = 4;

    public static bool IsDrawerUnlocked(int index)
    {
        return index < APState.UnlockedDrawers;
    }

    public static void GrantNext(string fromPlayer)
    {
        if (APState.UnlockedDrawers >= MAX_DRAWERS)
            return;

        int index = APState.UnlockedDrawers;
        APState.UnlockedDrawers++;

        Data.game.drawersUnlocked[index] = true;
        APSaveManager.Save();

        if (IsSafeToApplyVisualUnlock())
        {
            DrawersScript.Unlock(index);
        }

        Plugin.Log.LogInfo(
            "[AP] Drawer " + (index + 1) + " unlocked by " + fromPlayer
        );
    }

    private static bool IsSafeToApplyVisualUnlock()
    {
        return
            DrawersScript.instance != null &&
            !ScreenMenuScript.IsEnabled() &&
            !DialogueScript.IsEnabled() &&
            !PowerupTriggerAnimController.HasAnimations() &&
            SceneManager.GetActiveScene().buildIndex == (int)Level.SceneIndex.Game;
    }
}
