using HarmonyLib;
using UnityEngine;

[HarmonyPatch(typeof(ScreenMenuScript))]
public static class GreyOutNewRunButtonsPatch
{
    [HarmonyPostfix]
    [HarmonyPatch(nameof(ScreenMenuScript.Open))]
    static void OpenPostfix()
    {
        var menu = ScreenMenuScript.instance;
        if (menu == null)
            return;

        var traverse = Traverse.Create(menu);
        int optionsCount = traverse.Field("optionsCount").GetValue<int>();

        if (APState.IsConnected)
            return;

        for (int i = 0; i < optionsCount; i++)
        {
            var text = menu.optionsTextArray[i];
            if (text == null)
                continue;

            text.color = new Color(0.5f, 0.5f, 0.5f, 0.8f);
        }
    }

    public static void OnAPConnected()
    {
        var menu = ScreenMenuScript.instance;
        if (menu == null)
            return;

        var traverse = Traverse.Create(menu);
        int optionsCount = traverse.Field("optionsCount").GetValue<int>();

        for (int i = 0; i < optionsCount; i++)
        {
            var text = menu.optionsTextArray[i];
            if (text == null)
                continue;

            text.color = Color.white;
        }
    }
}
