using HarmonyLib;
using System.Reflection;
using UnityEngine;

[HarmonyPatch(typeof(RewardBoxScript), nameof(RewardBoxScript.Initialize))]
static class APRewardBoxInitializePatch
{
    static readonly FieldInfo rewardKindField =
        AccessTools.Field(typeof(RewardBoxScript), "rewardKind");

    static readonly FieldInfo prizeOfGameField =
        AccessTools.Field(typeof(RewardBoxScript), "prizeOfGame");

    static readonly FieldInfo prizeDefaultMaterialField =
        AccessTools.Field(typeof(RewardBoxScript), "prizeDefaultMaterial");

    static readonly FieldInfo openedField =
        AccessTools.Field(typeof(RewardBoxScript), "_opened");

    static bool Prefix(bool isNewGame)
    {
        if (!APState.IsConnected)
            return true;

        var inst = RewardBoxScript.instance;
        if (inst == null)
            return false;

        int keys = Mathf.Clamp(APState.KeysCompleted, 0, 4);

        RewardBoxScript.RewardKind kind =
            keys < 4
                ? RewardBoxScript.RewardKind.DrawerKey0 + keys
                : RewardBoxScript.RewardKind.DoorKey;

        GameplayData.RewardKind = kind;
        rewardKindField.SetValue(inst, kind);

        foreach (var prize in inst.prizes)
            prize.SetActive(false);

        GameObject prizeObj;
        switch (kind)
        {
            case RewardBoxScript.RewardKind.DemoPrize:
                prizeObj = inst.prizes[0];
                break;

            case RewardBoxScript.RewardKind.DrawerKey0:
            case RewardBoxScript.RewardKind.DrawerKey1:
            case RewardBoxScript.RewardKind.DrawerKey2:
            case RewardBoxScript.RewardKind.DrawerKey3:
                prizeObj = inst.prizes[1];
                break;

            case RewardBoxScript.RewardKind.DoorKey:
                prizeObj = inst.prizes[2];
                break;

            default:
                return false;
        }

        prizeOfGameField.SetValue(inst, prizeObj);

        inst.prizeMeshRenderer =
            prizeObj.GetComponentInChildren<MeshRenderer>(true);

        prizeDefaultMaterialField.SetValue(
            inst,
            inst.prizeMeshRenderer.sharedMaterial
        );

        prizeObj.SetActive(GameplayData.RewardBoxHasPrize());

        bool opened = GameplayData.RewardBoxIsOpened();
        openedField.SetValue(inst, opened);
        if (opened)
            inst.SetModelAsOpened();

        RewardBoxScript.RefreshText_ToDeadlineDebtToReach();

        return false;
    }
}
