using HarmonyLib;
using Panik;
using System.Collections.Generic;
using System.Reflection;

[HarmonyPatch]
public static class BlockControlsWhileAPUIOpenPatch
{
    static IEnumerable<MethodBase> TargetMethods()
    {
        foreach (var m in typeof(Controls).GetMethods(BindingFlags.Public | BindingFlags.Static))
        {
            if (m.ReturnType == typeof(float) ||
                m.ReturnType == typeof(bool))
            {
                foreach (var p in m.GetParameters())
                {
                    if (p.ParameterType == typeof(Controls.InputAction))
                    {
                        yield return m;
                        break;
                    }
                }
            }
        }
    }

    static bool Prefix(MethodBase __originalMethod, ref object __result)
    {
        if (!APState.IsAPUIOpen)
            return true;

        if (__result is float)
            __result = 0f;
        else if (__result is bool)
            __result = false;

        return false;
    }
}
