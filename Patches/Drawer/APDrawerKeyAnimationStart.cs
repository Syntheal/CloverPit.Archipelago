using HarmonyLib;
using Panik;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[HarmonyPatch(typeof(DrawersScript))]
public static class APDrawerKeyAnimationStart
{
    [HarmonyPrefix]
    [HarmonyPatch("KeyAnimationStart")]
    static bool Prefix(int keyIndex)
    {
        return false;
    }
}