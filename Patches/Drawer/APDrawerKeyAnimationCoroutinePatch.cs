using HarmonyLib;
using Panik;
using UnityEngine;
using System.Collections;

[HarmonyPatch(typeof(DrawersScript))]
public static class APDrawerKeyAnimationCoroutinePatch
{
    [HarmonyPrefix]
    [HarmonyPatch("KeyAnimationCoroutine")]
    static bool Prefix(ref IEnumerator __result, DrawersScript __instance, int keyIndex)
    {
        if (!APState.SuppressDrawerUnlockQuestion)
            return true;

        __result = APKeyAnimation_NoQuestion(__instance, keyIndex);
        return false;
    }

    private static IEnumerator APKeyAnimation_NoQuestion(DrawersScript inst, int keyIndex)
    {
        inst.keyTransforms[keyIndex].SetLocalZ(1.75f);
        inst.keyTransforms[keyIndex].SetLocalXAngle(-90f);

        while (inst.keyTransforms[keyIndex].GetLocalZ() > 0.8f)
        {
            inst.keyTransforms[keyIndex].AddLocalZ(
                (0.7f - inst.keyTransforms[keyIndex].GetLocalZ()) * Tick.Time * 10f
            );
            yield return null;
        }

        inst.keyTransforms[keyIndex].SetLocalZ(0.7f);
        Sound.Play3D("SoundDrawerKeyEnter", inst.keyTransforms[keyIndex].position, 10f);

        yield return new WaitForSeconds(0.25f);

        Sound.Play3D("SoundDrawerKeyTurn", inst.keyTransforms[keyIndex].position, 10f);

        while (inst.keyTransforms[keyIndex].GetLocalXAngle() < -1f)
        {
            inst.keyTransforms[keyIndex].AddLocalXAngle(
                (0f - inst.keyTransforms[keyIndex].GetLocalXAngle()) * Tick.Time * 10f
            );
            yield return null;
        }

        inst.keyTransforms[keyIndex].SetLocalXAngle(0f);

        CameraController.DisableReason_Remove("drwrUn");

        var field = AccessTools.Field(typeof(DrawersScript), "keyAnimationCoroutine");
        var arr = (Coroutine[])field.GetValue(inst);
        arr[keyIndex] = null;
    }
}
