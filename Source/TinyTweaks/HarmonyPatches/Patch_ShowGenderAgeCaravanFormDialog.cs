using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using HarmonyLib;
using System;

namespace TinyTweaks;

public static class ShowGenderAgeCaravanFormDialog_Patches
{

    [HarmonyPatch(typeof(TransferableOneWay), "Label", MethodType.Getter)]
    public static class TransferableOneWay_Label
    {
        static void Postfix(ref string __result, TransferableOneWay __instance)
        {
            if (TinyTweaksSettings.ShowGenderAgeCaravanFormDialog)
            {


                if (__instance.AnyThing is Pawn pawn)
                {
                    if (pawn.Name != null && !pawn.Name.Numerical && !pawn.RaceProps.Humanlike)
                    {
                        __result += ", " + pawn.def.label;
                    }

                    __result += " (" + pawn.GetGenderLabel() + ", " + Mathf.FloorToInt(pawn.ageTracker.AgeBiologicalYearsFloat) + ")";

                }
            }
        }
    }
}



