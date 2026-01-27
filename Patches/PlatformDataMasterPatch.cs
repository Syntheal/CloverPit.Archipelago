using Cysharp.Threading.Tasks;
using HarmonyLib;
using Panik;
using System.Collections;
using System.IO;

namespace CloverPit.Archipelago.Patches
{
    [HarmonyPatch(typeof(PlatformDataMaster))]
    [HarmonyPatch(nameof(PlatformDataMaster.PathGet_GameDataFile))]
    public static class PlatformDataMasterFilePatch
    {
        [HarmonyPrefix]
        public static bool PathGet_GameDataFile_Prefix(int gameDataIndex, string extraAppendix, ref string __result)
        {
            __result = PlatformDataMaster.GameFolderPath + "GameDataFull_Arch" + extraAppendix + ".json";
            return false;
        }
    }

    [HarmonyPatch(typeof(Data))]
    [HarmonyPatch(nameof(Data.LoadGame))]
    public static class LoadGamePatch
    {
        [HarmonyPostfix]
        public static void LoadGamePostfix(UniTask<bool> __result)
        {
            HandleTutorialFlag(__result).Forget();
        }

        private static async UniTaskVoid HandleTutorialFlag(UniTask<bool> task)
        {
            try
            {
                await task;

                if (Data.GameData.inst != null)
                {
                    Data.GameData.inst.tutorialQuestionEnabled = false;
                }
            }
            catch
            {
            }
        }
    }
    [HarmonyPatch(typeof(GameplayMaster))]
    [HarmonyPatch(nameof(GameplayMaster.DeathsSinceStartup_GetNum))]
    public static class TutorialScriptPatch
    {
        [HarmonyPostfix]
        public static void TutorialPostFix(ref int __result)
        {
            if (__result == 0)
                __result = 1;
        }
    }

    [HarmonyPatch(typeof(IntroScript))]
    [HarmonyPatch("PopUpShow")]
    public static class IntroScriptPopUpPatch
    {
        [HarmonyPrefix]
        public static bool PopUpShowPrefix(ref string title, ref bool isQuestion, ref IEnumerator __result)
        {
            if (title == Translation.Get("INTRO_POPUP_QUESTION_DO_YOU_WANT_TO_IMPORT_DEMO_DATA_TITLE"))
            {
                __result = AutoAnswerPopUpCoroutine(false);
                return false;
            }

            return true;
        }

        private static IEnumerator AutoAnswerPopUpCoroutine(bool answer)
        {
            yield return null;
            yield break;
        }
    }

    [HarmonyPatch(typeof(GameplayData))]
    [HarmonyPatch("NewGameIntroFinished_Set")]
    public static class GameplayData_NewGameIntroFinished_Patch
    {
        [HarmonyPrefix]
        public static bool Prefix(ref bool finished)
        {
            finished = true;
            return true;
        }
    }
}
