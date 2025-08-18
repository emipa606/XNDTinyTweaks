﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;

namespace TinyTweaks;

[StaticConstructorOnStartup]
public static class StartupPatches
{
    static StartupPatches()
    {
        if (TinyTweaksSettings.ChangeDefLabels)
        {
            changeDefLabels();
        }

        if (ModLister.GetActiveModWithIdentifier("LWM.DeepStorage", true) == null &&
            TinyTweaksSettings.ChangeBuildableDefDesignationCategories)
        {
            updateDesignationCategories();
        }

        // Patch defs
        patchThingDefs();
        if (TinyTweaksSettings.AlphabeticalBillList)
        {
            SortThingDefRecipes();
        }
    }

    private static IEnumerable<DesignationCategoryDef> CategoriesToRemove
    {
        get
        {
            if (DesignationCategoryDefOf.ANON2MF != null)
            {
                yield return DesignationCategoryDefOf.ANON2MF;
            }

            if (DesignationCategoryDefOf.MoreFloors != null)
            {
                yield return DesignationCategoryDefOf.MoreFloors;
            }

            if (DesignationCategoryDefOf.HygieneMisc != null)
            {
                yield return DesignationCategoryDefOf.HygieneMisc;
            }

            if (DesignationCategoryDefOf.DefensesExpanded_CustomCategory != null)
            {
                yield return DesignationCategoryDefOf.DefensesExpanded_CustomCategory;
            }
        }
    }

    public static void SortThingDefRecipes()
    {
        var thingDefs = DefDatabase<ThingDef>.AllDefsListForReading;
        if (TinyTweaksSettings.AlphabeticalBillList)
        {
            //Log.Message($"[XNDTinyTweaks]: Sorting recipes for {thingDefs.Count} thingDefs in alphabetical order");
            foreach (var thingDef in thingDefs)
            {
                var defAllRecipes = thingDef.AllRecipes;
                if (defAllRecipes == null || !defAllRecipes.Any())
                {
                    continue;
                }

                //Log.Message($"Recipes found for {thingDef}");
                defAllRecipes.SortBy(r => r.label);
                Traverse.Create(thingDef).Field("allRecipesCached").SetValue(defAllRecipes);
            }

            return;
        }

        //Log.Message($"[XNDTinyTweaks]: Restoring vanilla order of recipes for {thingDefs.Count} thingDefs");
        foreach (var thingDef in thingDefs)
        {
            Traverse.Create(thingDef).Field("allRecipesCached").SetValue(null);
            _ = thingDef.AllRecipes;
        }
    }

    private static void patchThingDefs()
    {
        var allThingDefs = DefDatabase<ThingDef>.AllDefsListForReading;
        foreach (var tDef in allThingDefs)
        {
            // If the def has CompLaunchable, add CompLaunchableAutoRebuild to it
            if (tDef.HasComp(typeof(CompLaunchable)))
            {
                tDef.AddComp(typeof(CompLaunchableAutoRebuild));
            }

            // If the def has RaceProps and RaceProps are humanlike, add CompSkillTrackerCache to it
            if (tDef.race is { Humanlike: true })
            {
                tDef.AddComp(typeof(CompSkillRecordCache));
            }

            // If the def is a turret but not a mortar, add CompSmarterTurretTargeting to it
            if (tDef.IsBuildingArtificial && tDef.building.IsTurret && !tDef.building.IsMortar)
            {
                tDef.AddComp(typeof(CompSmarterTurretTargeting));
            }
        }
    }

    private static void updateDesignationCategories()
    {
        // Change the DesignationCategoryDefs of appropriate defs
        changeDesignationCategories();

        // Update all appropriate categories
        if (CategoriesToRemove.Any())
        {
            var defDatabaseRemove =
                typeof(DefDatabase<DesignationCategoryDef>).GetMethod("Remove",
                    BindingFlags.NonPublic | BindingFlags.Static);
            foreach (var dcDef in CategoriesToRemove)
            {
                defDatabaseRemove?.Invoke(null, [dcDef]);
            }
        }

        foreach (var dcDef in DefDatabase<DesignationCategoryDef>.AllDefs)
        {
            dcDef.ResolveReferences();
        }
    }

    private static void changeDesignationCategories()
    {
        // This method only exists in the case that other modders want their BuildableDefs to be changed, and they decide to do so via harmony
        foreach (var thDef in DefDatabase<ThingDef>.AllDefs)
        {
            changeDesignationCategory(thDef);
        }

        foreach (var trDef in DefDatabase<TerrainDef>.AllDefs)
        {
            changeDesignationCategory(trDef);
        }
    }

    private static void changeDesignationCategory(BuildableDef bDef)
    {
        if (bDef.designationCategory == null)
        {
            return;
        }

        var mod = bDef.modContentPack;

        // Furniture+ => Furniture
        if (DesignationCategoryDefOf.ANON2MF != null &&
            bDef.designationCategory == DesignationCategoryDefOf.ANON2MF)
        {
            bDef.designationCategory = DesignationCategoryDefOf.Furniture;
        }

        // More Floors => Floors
        if (DesignationCategoryDefOf.MoreFloors != null &&
            bDef.designationCategory == DesignationCategoryDefOf.MoreFloors)
        {
            bDef.designationCategory = DesignationCategoryDefOf.Floors;
        }

        if (mod != null)
        {
            // Dubs Bad Hygiene
            if (mod.PackageId.Equals("Dubwise.DubsBadHygiene", StringComparison.CurrentCultureIgnoreCase))
            {
                // Temperature stuff gets moved to Temperature category
                if (bDef.researchPrerequisites != null && bDef.researchPrerequisites.Any(r =>
                        r.defName == "CentralHeating" || r.defName == "PoweredHeating" ||
                        r.defName == "MultiSplitAirCon"))
                {
                    bDef.designationCategory = DesignationCategoryDefOf.Temperature;
                }

                // Rest gets moved from Hygiene/Misc => Hygiene
                else if (bDef.designationCategory == DesignationCategoryDefOf.HygieneMisc)
                {
                    bDef.designationCategory = DesignationCategoryDefOf.Hygiene;
                }
            }

            // Furniture => Storage (Deep Storage)
            if (mod.PackageId.Equals("LWM.DeepStorage", StringComparison.CurrentCultureIgnoreCase))
            {
                bDef.designationCategory = DesignationCategoryDefOf.Storage;
            }
        }

        // Defenses => Security
        if (DesignationCategoryDefOf.DefensesExpanded_CustomCategory != null && bDef.designationCategory ==
            DesignationCategoryDefOf.DefensesExpanded_CustomCategory)
        {
            bDef.designationCategory = DesignationCategoryDefOf.Security;
        }
    }

    private static Type XmlExtensions_SettingsMenuDef;

    private static void changeDefLabels()
    {
        XmlExtensions_SettingsMenuDef = AccessTools.TypeByName("XmlExtensions.SettingsMenuDef");

        // Go through every appropriate def that has a label
        var changeableDefTypes = GenDefDatabase.AllDefTypesWithDatabases().Where(shouldChangeDefTypeLabel)
            .ToList();
        foreach (var defType in changeableDefTypes)
        {
            var curDefs = GenDefDatabase.GetAllDefsInDatabaseForDef(defType).ToList();
            foreach (var curDef in curDefs)
            {
                if (curDef.label.NullOrEmpty())
                {
                    continue;
                }

                // Update the def's label
                adjustLabel(ref curDef.label);

                // If the def is a ThingDef...
                if (curDef is not ThingDef tDef)
                {
                    continue;
                }

                // If the ThingDef is a stuff item
                if (tDef.stuffProps is not { } stuffProps)
                {
                    continue;
                }

                // Update the stuff adjective if there is one
                if (!stuffProps.stuffAdjective.NullOrEmpty())
                {
                    adjustLabel(ref stuffProps.stuffAdjective);
                }
            }
        }
    }

    private static bool shouldChangeDefTypeLabel(Type defType)
    {
        return defType != typeof(StorytellerDef) && defType != typeof(ResearchProjectDef) &&
               defType != typeof(ResearchTabDef) && defType != typeof(ExpansionDef) &&
               defType != XmlExtensions_SettingsMenuDef;
    }

    private static void adjustLabel(ref string label)
    {
        // Split the label up by spaces
        var splitLabel = label.Split(' ');

        // Process each word within the label
        for (var i = 0; i < splitLabel.Length; i++)
        {
            // If the word contains hyphens, split at the hyphens and process each word
            if (splitLabel[i].Contains('-'))
            {
                var labelPartSplit = splitLabel[i].Split('-');
                for (var j = 0; j < labelPartSplit.Length; j++)
                {
                    adjustLabelPart(ref labelPartSplit[j], true);
                }

                splitLabel[i] = string.Join("-", labelPartSplit);
            }

            // Otherwise adjust as a whole
            else
            {
                adjustLabelPart(ref splitLabel[i], false);
            }
        }

        // Update the label
        label = string.Join(" ", splitLabel);
    }

    private static void adjustLabelPart(ref string labelPart, bool uncapitaliseSingleCharacters)
    {
        // If labelPart is only a single character, do nothing unless uncapitaliseSingleCharacters is true
        if (labelPart.Length == 1)
        {
            if (uncapitaliseSingleCharacters)
            {
                labelPart = labelPart.ToLower();
            }

            return;
        }

        // Split labelPart into its characters
        var labelPartChars = labelPart.ToCharArray();

        // Go through each character and if there are no more characters that aren't lower-cased letters, uncapitalise labelPart
        var uncapitalise = true;
        for (var j = 1; j < labelPartChars.Length; j++)
        {
            if (char.IsLower(labelPartChars[j]))
            {
                continue;
            }

            uncapitalise = false;
            break;
        }

        if (uncapitalise)
        {
            labelPart = labelPart.ToLower();
        }
    }
}