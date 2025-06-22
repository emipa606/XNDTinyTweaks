using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace TinyTweaks;

[HarmonyPatch(typeof(TransferableOneWay), nameof(TransferableOneWay.Label), MethodType.Getter)]
public static class TransferableOneWay_Label
{
    private static void Postfix(ref string __result, TransferableOneWay __instance)
    {
        if (!TinyTweaksSettings.ShowGenderAgeCaravanFormDialog)
        {
            return;
        }

        if (__instance.AnyThing is not Pawn pawn)
        {
            return;
        }

        if (pawn.Name is { Numerical: false } && !pawn.RaceProps.Humanlike)
        {
            __result += $", {pawn.def.label}";
        }

        __result += $" ({pawn.GetGenderLabel()}, {Mathf.FloorToInt(pawn.ageTracker.AgeBiologicalYearsFloat)})";
    }
}