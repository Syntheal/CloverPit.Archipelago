using HarmonyLib;

[HarmonyPatch(typeof(ScreenMenuScript))]
public static class RemoveContinueButtonPatch
{
    [HarmonyPostfix]
    [HarmonyPatch(nameof(ScreenMenuScript.Open))]
    static void OpenPostfix()
    {
        var menu = ScreenMenuScript.instance;
        if (menu == null || APState.GameStarted)
            return;

        var traverse = Traverse.Create(menu);
        int optionsCount = traverse.Field("optionsCount").GetValue<int>();

        if (optionsCount < 3)
            return;

        for (int i = 1; i < optionsCount; i++)
        {
            if (menu.optionsTextArray[i - 1] != null)
                menu.optionsTextArray[i - 1].text = menu.optionsTextArray[i].text;

            menu.optionEventsArray[i - 1] = menu.optionEventsArray[i];
        }

        int lastIndex = optionsCount - 1;

        if (menu.optionsTextArray[lastIndex] != null)
        {
            menu.optionsTextArray[lastIndex].text = "";
            menu.optionsTextArray[lastIndex].gameObject.SetActive(false);
        }

        menu.optionEventsArray[lastIndex] = null;

        traverse.Field("optionsCount").SetValue(optionsCount - 1);
    }
}
