using HarmonyLib;
using Panik;

[HarmonyPatch(typeof(GameplayData))]
public static class APCloverTickets
{
    private static readonly (long ticketCount, long location)[] CloverTicketMilestones = new (long, long)[]
    {
        (50, APLocations.HAVE_50_CLOVER_TICKETS),
        (100, APLocations.HAVE_100_CLOVER_TICKETS),
        (150, APLocations.HAVE_150_CLOVER_TICKETS),
        (200, APLocations.HAVE_200_CLOVER_TICKETS),
        (250, APLocations.HAVE_250_CLOVER_TICKETS)
    };

    [HarmonyPostfix]
    [HarmonyPatch("CloverTicketsSet")]
    public static void Postfix_CloverTicketsSet(long value)
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        foreach (var milestone in CloverTicketMilestones)
        {
            if (value >= milestone.ticketCount && !APLocationManager.IsChecked(milestone.location))
            {
                APLocationManager.Complete(milestone.location);
                APSaveManager.Save();

                Plugin.Log.LogInfo($"[AP] Reached {milestone.ticketCount} Clover Tickets milestone");
            }
        }
    }
}