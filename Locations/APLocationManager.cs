public static class APLocationManager
{
    public static bool IsChecked(long locationId)
    {
        return APSaveManager.IsLocationChecked(locationId);
    }

    public static void Complete(long locationId)
    {
        if (IsChecked(locationId))
            return;

        APSaveManager.MarkLocationChecked(locationId);

        if (APState.IsConnected)
        {
            APClient.SendLocationCheck(locationId);

            Plugin.Log.LogInfo(
                $"[AP] Location checked: {locationId}"
            );
        }
        else
        {
            Plugin.Log.LogDebug(
                $"[AP] Cached location (offline): {locationId}"
            );
        }
    }

    public static void SyncCheckedLocations()
    {
        if (!APState.IsConnected || APState.LocationsResynced)
            return;

        foreach (var loc in APSaveManager.data.CheckedLocations)
            APClient.SendLocationCheck(loc);

        APState.LocationsResynced = true;

        Plugin.Log.LogInfo(
            $"[AP] Resent {APSaveManager.data.CheckedLocations.Count} cached locations"
        );
    }

}
