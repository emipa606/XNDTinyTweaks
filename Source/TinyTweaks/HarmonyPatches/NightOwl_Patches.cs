using System;
using System.Collections.Generic;
using RimWorld;
using Verse;
using HarmonyLib;

namespace TinyTweaks
{

    public static class NightOwl_Patches
    {

        [HarmonyPatch(typeof(Thing), "SpawnSetup")]
        public static class Thing_SpawnSetup
        {

            public static void Postfix(Thing __instance)
            {
                if (!TinyTweaksSettings.autoOwl)
                {
                    return;
                }
                if (!(__instance is Pawn pawn))
                {
                    return;
                }
                SetNightOwl(pawn);
            }
        }

        [HarmonyPatch(typeof(InteractionWorker_RecruitAttempt), "DoRecruit", new Type[] { typeof(Pawn), typeof(Pawn), typeof(float), typeof(bool)})]
        public static class InteractionWorker_RecruitAttempt_DoRecruit
        {
            public static void Postfix(Pawn recruiter, Pawn recruitee, float recruitChance)
            {
                if(!TinyTweaksSettings.autoOwl)
                {
                    return;
                }
                if (!(recruitee is Pawn pawn))
                {
                    return;
                }
                SetNightOwl(pawn);
            }
        }

        private static void SetNightOwl(Pawn pawn)
        {
            if ((pawn.Faction?.IsPlayer) != true)
            {
                return;
            }
            if ((pawn.def?.race?.Humanlike) != true)
            {
                return;
            }
            if (!pawn.story.traits.HasTrait(TraitDefOf.NightOwl))
            {
                return;
            }
            if (pawn.timetable == null)
            {
                return;
            }
            for (var i = 0; i < GenDate.HoursPerDay; i++)
            {
                if (i >= 11 && i <= 18)
                {
                    pawn.timetable.times[i] = TimeAssignmentDefOf.Sleep;
                }
                else
                {
                    if (pawn.timetable.times[i] == TimeAssignmentDefOf.Sleep)
                        pawn.timetable.times[i] = TimeAssignmentDefOf.Anything;
                }
            }
        }

    }

}
