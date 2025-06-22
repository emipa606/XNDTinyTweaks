using HarmonyLib;
using RimWorld;
using Verse;

namespace TinyTweaks;

[HarmonyPatch(typeof(HediffSet), "CalculateBleedRate")]
public static class HediffSet_CalculateBleedRate
{
    public static void Postfix(HediffSet __instance, ref float __result)
    {
        // Scale bleeding rate based on blood pumping
        if (TinyTweaksSettings.BloodPumpingAffectsBleeding)
        {
            __result *= __instance.pawn.health.capacities.GetLevel(PawnCapacityDefOf.BloodPumping);
        }
    }
}