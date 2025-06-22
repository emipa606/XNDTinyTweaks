using HarmonyLib;
using RimWorld;
using Verse;

namespace TinyTweaks;

[HarmonyPatch(typeof(SkillRecord), nameof(SkillRecord.Interval))]
public static class SkillRecord_Interval
{
    public static bool Prefix(SkillRecord __instance, Pawn ___pawn)
    {
        // Delay skill decay
        return !TinyTweaksSettings.DelayedSkillDecay ||
               ___pawn.GetComp<CompSkillRecordCache>() is not { } cache ||
               cache.CanDecaySkill(__instance.def);
    }
}