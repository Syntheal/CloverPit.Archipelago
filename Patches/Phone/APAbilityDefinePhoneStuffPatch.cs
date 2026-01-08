using HarmonyLib;
using Panik;
using System;
using System.Collections.Generic;
using UnityEngine;

[HarmonyPatch(typeof(PhoneUiScript))]
public static class APAbilityDefinePhoneStuffPatch
{
    public static Dictionary<string, int> totalPickedTimes = new Dictionary<string, int>();
    [HarmonyPostfix]
    [HarmonyPatch("DefinePhoneStuff")]
    static void Postfix(bool refreshRequestedThis)
    {
        GameplayData gameplayData = GameplayData.Instance;
        AbilityScript.Category category = gameplayData._phone_lastAbilityCategory;

        if (!AbilityScript.dict_ListsByCategory.ContainsKey(category))
        {
            return;
        }

        AggregatePickedTimes(gameplayData);

        List<AbilityScript.Identifier> filteredAbilities = new List<AbilityScript.Identifier>();
        HashSet<string> alreadyAddedAbilities = new HashSet<string>();

        if (gameplayData._phone_pickedUpOnceLastDeadline && !refreshRequestedThis)
        {
            category = gameplayData._phone_lastAbilityCategory;
        }
        else
        {
            if (gameplayData._phone_bookSpecialCall)
            {
                category = AbilityScript.Category.evil;
            }
            else
            {
                category = AbilityScript.Category.normal;
            }
        }

        foreach (var ability in gameplayData._phone_AbilitiesToPick)
        {
            string abilityName = ability.ToString();

            if (ability == AbilityScript.Identifier.undefined)
            {
                continue;
            }

            if (!alreadyAddedAbilities.Contains(abilityName) && APState.UnlockedPhoneAbilities.Contains(ability))
            {
                string baseAbilityName = abilityName.Split(' ')[0];

                UpdateAbilityData(baseAbilityName);

                var abilityScript = AbilityScript.AbilityGet(ability);
                if (abilityScript != null)
                {
                    var maxPickupTimesField = AccessTools.Field(typeof(AbilityScript), "maxPickupTimes");
                    int maxPickupTimes = (int)maxPickupTimesField.GetValue(abilityScript);
                    int currentPickedTimes = totalPickedTimes.ContainsKey(abilityName) ? totalPickedTimes[abilityName] : 0;

                    if (currentPickedTimes >= maxPickupTimes && maxPickupTimes != -1)
                    {

                        continue;
                    }
                }

                filteredAbilities.Add(ability);
                alreadyAddedAbilities.Add(abilityName);
            }
        }

        gameplayData._phone_AbilitiesToPick.RemoveAll(ability => ability == AbilityScript.Identifier.undefined);

        gameplayData._phone_AbilitiesToPick.Clear();

        int numAbilitiesToPick = GameplayData.PhoneAbilitiesNumber_Get(); 

        var rerollPreventionListField = AccessTools.Field(typeof(PhoneUiScript), "sameAbilitiesRerollPreventionList");
        if (rerollPreventionListField != null)
        {
            List<AbilityScript> rerollList = (List<AbilityScript>)rerollPreventionListField.GetValue(PhoneUiScript.instance);

            rerollList.Clear();

            foreach (var ability in filteredAbilities)
            {
                string abilityName = ability.ToString();
                var abilityScript = AbilityScript.AbilityGet(ability);

                if (abilityScript != null)
                {
                    var maxPickupTimesField = AccessTools.Field(typeof(AbilityScript), "maxPickupTimes");
                    int maxPickupTimes = (int)maxPickupTimesField.GetValue(abilityScript);
                    int currentPickedTimes = totalPickedTimes.ContainsKey(abilityName) ? totalPickedTimes[abilityName] : 0;

                    if (currentPickedTimes >= maxPickupTimes && maxPickupTimes != -1)
                    {
                        rerollList.Add(abilityScript);
                    }
                }
            }
        }

        for (int i = 0; i < numAbilitiesToPick; i++)
        {
            AbilityScript.Identifier selectedAbility = AbilityScript.Identifier.undefined;

            if (filteredAbilities.Count > 0)
            {
                selectedAbility = filteredAbilities[UnityEngine.Random.Range(0, filteredAbilities.Count)];
            }

            if (CanPickAbility(selectedAbility))
            {
                gameplayData._phone_AbilitiesToPick.Add(selectedAbility);
                filteredAbilities.Remove(selectedAbility);
            }
        }

        try
        {
            PhoneUiScript.instance.uiHolder.SetActive(false);

            for (int i = 0; i < gameplayData._phone_AbilitiesToPick.Count; i++)
            {
                AbilityScript.Identifier abilityId = gameplayData._phone_AbilitiesToPick[i];
                if (abilityId == AbilityScript.Identifier.undefined)
                {
                    PhoneAbilityUiScript.SetAbility(i, null);
                }
                else
                {
                    AbilityScript abilityScript = AbilityScript.AbilityGet(abilityId);

                    if (abilityScript == null || abilityScript.IdentifierGet() == AbilityScript.Identifier.undefined)
                    {
                        PhoneAbilityUiScript.SetAbility(i, null);
                    }
                    else
                    {
                        PhoneAbilityUiScript.SetAbility(i, abilityScript);
                    }
                }
            }

            PhoneUiScript.instance.uiHolder.SetActive(true);
        }
        catch (Exception ex)
        {
            Plugin.Log.LogError($"[APAbilityDefinePhoneStuffPatch] UI refresh failed: {ex.Message}");
        }
    }

    static void AggregatePickedTimes(GameplayData gameplayData)
    {
        totalPickedTimes.Clear();

        foreach (var ability in gameplayData._phone_AbilitiesToPick)
        {
            string abilityName = ability.ToString();

            if (ability == AbilityScript.Identifier.undefined)
            {
                continue;
            }
            var abilityScript = AbilityScript.AbilityGet(ability);
            if (abilityScript != null)
            {
                var pickedTimesField = AccessTools.Field(typeof(AbilityScript), "pickedTimes");
                int pickedTimes = (int)pickedTimesField.GetValue(abilityScript);

                if (!totalPickedTimes.ContainsKey(abilityName))
                {
                    totalPickedTimes[abilityName] = pickedTimes;
                }
                else
                {
                    totalPickedTimes[abilityName] += pickedTimes;
                }
            }
        }
    }


    static bool CanPickAbility(AbilityScript.Identifier ability)
    {
        string abilityName = ability.ToString();

        if (ability == AbilityScript.Identifier.undefined)
        {
            return true;
        }

        int currentPickedTimes = totalPickedTimes.ContainsKey(abilityName) ? totalPickedTimes[abilityName] : 0;

        var abilityScript = AbilityScript.AbilityGet(ability);
        if (abilityScript != null)
        {
            var maxPickupTimesField = AccessTools.Field(typeof(AbilityScript), "maxPickupTimes");
            int maxPickupTimes = (int)maxPickupTimesField.GetValue(abilityScript);

            if (maxPickupTimes == -1 || currentPickedTimes < maxPickupTimes)
            {
                return true;
            }
        }

        return false;
    }


    static void UpdateAbilityData(string baseAbilityName)
    {
        if (!totalPickedTimes.ContainsKey(baseAbilityName))
        {
            totalPickedTimes[baseAbilityName] = 0;
        }
    }
}
