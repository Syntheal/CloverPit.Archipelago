//using HarmonyLib;
//using System.Collections.Generic;
//using System.Linq;

//[HarmonyPatch(typeof(AbilityScript))]
//public static class APAbilityInitializeAllPatch
//{
//    private static Dictionary<AbilityScript.Category, List<AbilityScript>> removedAbilities = new Dictionary<AbilityScript.Category, List<AbilityScript>>();

//    [HarmonyPostfix]
//    [HarmonyPatch(nameof(AbilityScript.InitializeAll))]
//    static void Postfix()
//    {
//        if (!APState.IsConnected || !APState.APSaveLoaded)
//            return;

//        foreach (var kvp in AbilityScript.dict_ListsByCategory)
//        {
//            var list = kvp.Value;

//            for (int i = list.Count - 1; i >= 0; i--)
//            {
//                var ability = list[i];
//                var id = ability.IdentifierGet();

//                if (!APPhoneAbilityMapping.IsManaged(id))
//                    continue;

//                if (!APState.UnlockedPhoneAbilities.Contains(id))
//                {
//                    if (!removedAbilities.ContainsKey(kvp.Key))
//                        removedAbilities[kvp.Key] = new List<AbilityScript>();

//                    removedAbilities[kvp.Key].Add(ability);

//                    list.RemoveAt(i);
//                }
//            }
//        }

//    }

//    public static void AddAbilityBack(AbilityScript.Identifier abilityIdentifier)
//    {
//        foreach (var kvp in removedAbilities)
//        {
//            var category = kvp.Key;
//            var removedList = kvp.Value;

//            var abilityToRestore = removedList.FirstOrDefault(ability => ability.IdentifierGet() == abilityIdentifier);
//            if (abilityToRestore != null)
//            {
//                if (!AbilityScript.dict_ListsByCategory[category].Any(a => a.IdentifierGet() == abilityToRestore.IdentifierGet()))
//                {
//                    AbilityScript.dict_ListsByCategory[category].Add(abilityToRestore);

//                    removedList.Remove(abilityToRestore);
//                }
//                break;
//            }
//        }
//    }

//}
