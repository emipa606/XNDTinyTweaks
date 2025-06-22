using HarmonyLib;
using Mlie;
using RimWorld;
using UnityEngine;
using Verse;

namespace TinyTweaks;

public class TinyTweaks : Mod
{
    public static Harmony HarmonyInstance;
    public static string CurrentVersion;

    private readonly TinyTweaksSettings settings;

    public TinyTweaks(ModContentPack content) : base(content)
    {
#if DEBUG
            Log.Error("XeoNovaDan left debugging enabled in Tiny Tweaks - please let him know!");
#endif

        settings = GetSettings<TinyTweaksSettings>();
        HarmonyInstance = new Harmony("XeoNovaDan.TinyTweaks");
        CurrentVersion =
            VersionFromManifest.GetVersionFromModMetaData(content.ModMetaData);
    }

    public override string SettingsCategory()
    {
        return "TinyTweaks.SettingsCategory".Translate();
    }

    public override void DoSettingsWindowContents(Rect inRect)
    {
        TinyTweaksSettings.DoWindowContents(inRect);
    }

    public override void WriteSettings()
    {
        base.WriteSettings();

        StartupPatches.SortThingDefRecipes();
    }

    public static void SetNightOwl(Pawn pawn)
    {
        if (pawn.Faction?.IsPlayer != true)
        {
            return;
        }

        if (pawn.def?.race?.Humanlike != true)
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
            if (i is >= 11 and <= 18)
            {
                pawn.timetable.times[i] = TimeAssignmentDefOf.Sleep;
            }
            else
            {
                if (pawn.timetable.times[i] == TimeAssignmentDefOf.Sleep)
                {
                    pawn.timetable.times[i] = TimeAssignmentDefOf.Anything;
                }
            }
        }
    }
}