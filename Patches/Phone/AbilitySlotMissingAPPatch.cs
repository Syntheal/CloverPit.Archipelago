using HarmonyLib;
using UnityEngine;

[HarmonyPatch(typeof(PhoneAbilityUiScript))]
public static class AbilitySlotMissingAPPatch
{
    [HarmonyPostfix]
    [HarmonyPatch("SetAbility", new[] { typeof(AbilityScript) })]
    static void SetAbilityPostfix(
        MonoBehaviour __instance,
        AbilityScript ability
    )
    {
        if (ability != null)
            return;

        var titleText = AccessTools.Field(__instance.GetType(), "titleText")
            ?.GetValue(__instance) as TMPro.TMP_Text;

        var descriptionText = AccessTools.Field(__instance.GetType(), "descriptionText")
            ?.GetValue(__instance) as TMPro.TMP_Text;

        var iconRenderer = AccessTools.Field(__instance.GetType(), "iconRenderer")
            ?.GetValue(__instance) as SpriteRenderer;

        var lastAbilityImage = AccessTools.Field(__instance.GetType(), "lastAbilityImage")
            ?.GetValue(__instance) as UnityEngine.UI.Image;

        if (titleText != null)
        {
            titleText.text = "Missing Item";
            titleText.color = new Color(0.2f, 0.5f, 1f);
        }

        if (descriptionText != null)
        {
            descriptionText.text = "Waiting for AP Item";
        }

        if (iconRenderer != null)
        {
            iconRenderer.sprite = null;
            iconRenderer.enabled = false;
        }

        if (lastAbilityImage != null)
        {
            lastAbilityImage.enabled = false;
        }
    }
}
