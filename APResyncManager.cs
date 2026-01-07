using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class APResyncManager
{
    private static bool hasAutoResynced = false;

    public static void TryAutoResync()
    {
        if (hasAutoResynced)
            return;

        if (!APState.IsConnected)
            return;

        if (!APState.APSaveLoaded)
            return;

        hasAutoResynced = true;

        Plugin.Log.LogInfo("[AP] Auto-resyncing items on new run");
        APClient.RequestResync();
    }

    public static void Reset()
    {
        hasAutoResynced = false;
    }
}