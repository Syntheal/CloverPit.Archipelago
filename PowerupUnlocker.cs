public static class PowerupUnlocker
{
    public static bool Unlock(PowerupScript.Identifier id)
    {
        APState.IsGrantingItem = true;

        bool result = PowerupScript.UnlockExt(
            id,
            notifyPlayer: true,
            saveGame: true
        );

        APState.IsGrantingItem = false;

        if (!result)
            return false;

        AddToShopPoolIfNeeded(id);

        return true;
    }

    private static void AddToShopPoolIfNeeded(PowerupScript.Identifier id)
    {
        var powerup = PowerupScript.GetPowerup_Quick(id);
        if (powerup == null)
            return;

        if (PowerupScript.IsEquipped_Quick(id) || PowerupScript.IsInDrawer_Quick(id))
            return;

        if (!PowerupScript.list_NotBought.Contains(powerup))
        {
            PowerupScript.list_NotBought.Add(powerup);
            Plugin.Log.LogInfo($"[AP] Added {id} to shop pool");
        }
    }
}
