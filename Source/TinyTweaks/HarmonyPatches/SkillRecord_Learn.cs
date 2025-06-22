using HarmonyLib;
using RimWorld;
using Verse;

namespace TinyTweaks;

[HarmonyPatch(typeof(SkillRecord), nameof(SkillRecord.Learn))]
public static class SkillRecord_Learn
{
    public static void Postfix(SkillRecord __instance, Pawn ___pawn, float xp)
    {
        // Update the pawn's CompSkillTrackerCache
        if (!(xp >= CompSkillRecordCache.MinExpToDelaySkillDecay) && (!(xp > 0) ||
                                                                      !(__instance.xpSinceMidnight >=
                                                                        CompSkillRecordCache
                                                                            .MinExpToDelaySkillDecay)))
        {
            return;
        }

        var skillRecordCache = ___pawn.GetComp<CompSkillRecordCache>();
        if (skillRecordCache != null)
        {
            skillRecordCache.NotifySubstantialExperienceGainedFor(__instance.def);
            return;
        }

        if (Prefs.DevMode)
        {
            Log.WarningOnce($"{___pawn} has null CompSkillRecordCache (def={___pawn.def.defName})",
                $"CompSkillRecordCache{___pawn.ThingID}".GetHashCode());
        }
    }
}