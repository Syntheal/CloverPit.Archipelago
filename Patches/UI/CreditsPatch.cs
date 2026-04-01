using HarmonyLib;
using System.Text;

[HarmonyPatch(typeof(MagazineUiScript), nameof(MagazineUiScript.GetCreditsString))]
public static class CreditsPatch
{
    static void Postfix(ref string __result)
    {
        var sb = new StringBuilder();

        sb.AppendLine();
        sb.AppendLine("<size=22><color=#FFD700>ARCHIPELAGO MOD</color></size>");
        sb.AppendLine("<color=#AAAAAA>───────────────────</color>");
        sb.AppendLine();
        sb.AppendLine("<color=#6EC6FF>Integration & Logic:</color> Syntheal");
        sb.AppendLine();
        sb.AppendLine("<i><color=#CCCCCC>With help from:</color></i>");
        sb.AppendLine("<color=#9FE29F>Felucia</color> & <color=#9FE29F>Dragion</color>");
        sb.AppendLine();
        sb.AppendLine("<i><color=#CCCCCC>And a special thanks to</color></i>");
        sb.AppendLine("<color=#B39DDB>All those that helped me test and find bugs</color>");
        sb.AppendLine("<color=#FFFFFF>HannahDaChamp</color>");
        sb.AppendLine("<color=#FFFFFF>Noobstrikker</color>");
        sb.AppendLine("<color=#FFFFFF>Orh Hvad</color>");
        sb.AppendLine("<color=#FFFFFF>GoodyBags</color>");
        sb.AppendLine("<color=#FFFFFF>BlakeSc</color>");
        sb.AppendLine("<color=#FFFFFF>LOLZ1190</color>");
        sb.AppendLine("<color=#FFFFFF>Rhythm</color>");
        sb.AppendLine("<color=#FFFFFF>Sogeki</color>");

        __result = sb.ToString();
    }
}
