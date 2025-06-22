using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace TinyTweaks;

public class TinyTweaksSettings : ModSettings
{
    private const float PageButtonWidth = 150;
    private const float PageButtonHeight = 35;
    private const float PageButtonPosOffsetFromCentre = 60;

    private const int QoLPageIndex = 1;
    private const int BalancePageIndex = 2;
    private const int AdditionsPageIndex = 3;

    private const int MaxPageIndex = AdditionsPageIndex;
    private static bool meleeArmourPenetrationFix = true;
    public static bool FixAnimalCount = true;

    public static bool RandomStartingSeason = true;
    private static int pageIndex = 1;

    public static bool AnimalMedicalAlerts = true;
    public static bool CaravanFoodRestrictions = true;
    public static bool AutoAssignAnimalFollowSettings = true;
    public static bool AutoRemoveMoisturePumps = true;
    public static bool AutoOwl = true;
    public static bool MedBedMedicalAlert = true;
    public static bool AlphabeticalBillList = true;
    public static bool ShowGenderAgeCaravanFormDialog = true;

    // Restart
    public static bool ChangeDefLabels = true;
    public static bool ChangeBuildableDefDesignationCategories = true;

    public static bool ChangeQualityDistribution;
    public static bool BloodPumpingAffectsBleeding;
    public static bool DelayedSkillDecay;

    private static int PageIndex
    {
        get => pageIndex;
        set => pageIndex = Mathf.Clamp(value, 1, MaxPageIndex);
    }

    private static void doHeading(Listing_Standard listing, GameFont font)
    {
        listing.Gap(10);
        var headingTranslationKey = "TinyTweaks.";
        switch (PageIndex)
        {
            case QoLPageIndex:
                headingTranslationKey += "QualityOfLifeChangesHeading";
                goto WriteHeader;
            case BalancePageIndex:
                headingTranslationKey += "BalanceChangesHeading";
                goto WriteHeader;
            case AdditionsPageIndex:
                headingTranslationKey += "TinyAdditionsHeading";
                goto WriteHeader;
        }

        WriteHeader:
        Text.Font = font + 1;
        listing.Label(headingTranslationKey.Translate());
        Text.Font = font;
        listing.GapLine(24);
    }

    private static void gameRestartNotRequired(Listing_Standard listing)
    {
        listing.Gap(10);
        listing.Label("TinyTweaks.GameRestartNotRequired".Translate());
    }

    private static void gameRestartRequired(Listing_Standard listing)
    {
        listing.Gap(10);
        listing.Label("TinyTweaks.GameRestartRequired".Translate());
    }

    private static void doQualityOfLifeChanges(Listing_Standard options)
    {
        // 'Game restart not required' note
        gameRestartNotRequired(options);

        // Animal medical alerts
        options.Gap(10);
        options.CheckboxLabeled("TinyTweaks.QoLChanges.AnimalMedicalAlerts".Translate(), ref AnimalMedicalAlerts,
            "TinyTweaks.QoLChanges.AnimalMedicalAlerts_ToolTip".Translate());

        // Assign food restrictions for caravans
        options.Gap(10);
        options.CheckboxLabeled("TinyTweaks.QoLChanges.CaravanFoodRestrictions".Translate(),
            ref CaravanFoodRestrictions, "TinyTweaks.QoLChanges.CaravanFoodRestrictions_ToolTip".Translate());

        // Automatically assign animals to follow their master
        options.Gap(10);
        options.CheckboxLabeled("TinyTweaks.QoLChanges.AutoAssignAnimalFollowSettings".Translate(),
            ref AutoAssignAnimalFollowSettings,
            "TinyTweaks.QoLChanges.AutoAssignAnimalFollowSettings_ToolTip".Translate());

        // Automatically remove finished moisture pumps
        options.Gap(10);
        options.CheckboxLabeled("TinyTweaks.QoLChanges.AutoRemoveTerrainPumpDry".Translate(),
            ref AutoRemoveMoisturePumps, "TinyTweaks.QoLChanges.AutoRemoveTerrainPumpDry_ToolTip".Translate());

        // Automatically set night owl timetables
        options.Gap(10);
        options.CheckboxLabeled("TinyTweaks.QoLChanges.AutoOwl".Translate(), ref AutoOwl,
            "TinyTweaks.QoLChanges.AutoOwl_ToolTip".Translate());

        // Show 'colonist needs treatment' alerts for pawns in medical beds
        options.Gap(10);
        options.CheckboxLabeled("TinyTweaks.QoLChanges.MedBedMedicalAlert".Translate(), ref MedBedMedicalAlert,
            "TinyTweaks.QoLChanges.MedBedMedicalAlert_ToolTip".Translate());

        // Sort workbench bill list alphabetically
        options.Gap(10);
        options.CheckboxLabeled("TinyTweaks.QoLChanges.AlphabeticalBillList".Translate(), ref AlphabeticalBillList,
            "TinyTweaks.QoLChanges.AlphabeticalBillList_ToolTip".Translate());

        // Show gender and age in caravan form dialog
        options.Gap(10);
        options.CheckboxLabeled("TinyTweaks.QoLChanges.ShowGenderAgeCaravanFormDialog".Translate(),
            ref ShowGenderAgeCaravanFormDialog,
            "TinyTweaks.QoLChanges.ShowGenderAgeCaravanFormDialog_ToolTip".Translate());


        // 'Game restart required' note
        options.GapLine(24);
        gameRestartRequired(options);

        if (ModLister.GetActiveModWithIdentifier("LWM.DeepStorage", true) == null)
        {
            // Change architect menu tabs
            options.Gap(10);
            options.CheckboxLabeled("TinyTweaks.QoLChanges.ChangeBuildableDefDesignationCategories".Translate(),
                ref ChangeBuildableDefDesignationCategories,
                "TinyTweaks.QoLChanges.ChangeBuildableDefDesignationCategories_ToolTip".Translate());
        }

        // Consistent label casing
        options.Gap(10);
        options.CheckboxLabeled("TinyTweaks.QoLChanges.ChangeDefLabels".Translate(), ref ChangeDefLabels,
            "TinyTweaks.QoLChanges.ChangeDefLabels_ToolTip".Translate());

        // Fix animal count on autoslaughter window
        options.Gap(10);
        options.CheckboxLabeled("TinyTweaks.BugFixes.FixAnimalCount".Translate(), ref FixAnimalCount,
            "TinyTweaks.BugFixes.FixAnimalCount_ToolTip".Translate());
    }

    private static void doBalanceChanges(Listing_Standard options)
    {
        // 'Game restart not required' note
        gameRestartNotRequired(options);

        // Blood pumping affects bleeding
        options.Gap(10);
        options.CheckboxLabeled("TinyTweaks.BalanceChanges.BloodPumpingAffectsBleeding".Translate(),
            ref BloodPumpingAffectsBleeding,
            "TinyTweaks.BalanceChanges.BloodPumpingAffectsBleeding_ToolTip".Translate());

        // Change quality distribution
        options.Gap(10);
        options.CheckboxLabeled("TinyTweaks.BalanceChanges.ChangeQualityDistribution".Translate(),
            ref ChangeQualityDistribution,
            "TinyTweaks.BalanceChanges.ChangeQualityDistribution_ToolTip".Translate());

        // Delayed skill decay
        options.Gap(10);
        options.CheckboxLabeled("TinyTweaks.BalanceChanges.DelayedSkillDecay".Translate(), ref DelayedSkillDecay,
            "TinyTweaks.BalanceChanges.DelayedSkillDecay_ToolTip".Translate());
    }

    private static void doAdditions(Listing_Standard options)
    {
        // 'Game restart not required' note
        gameRestartNotRequired(options);

        // Random season button
        options.Gap(10);
        options.CheckboxLabeled("TinyTweaks.TinyAdditions.RandomStartingSeason".Translate(),
            ref RandomStartingSeason, "TinyTweaks.TinyAdditions.RandomStartingSeason_ToolTip".Translate());
    }

    private static void doPageButtons(Rect wrect)
    {
        var halfRectWidth = wrect.width / 2;
        var xOffset = (halfRectWidth - PageButtonWidth) / 2;
        var leftButtonRect = new Rect(xOffset + PageButtonPosOffsetFromCentre, wrect.height - PageButtonHeight,
            PageButtonWidth, PageButtonHeight);
        if (Widgets.ButtonText(leftButtonRect, "TinyTweaks.PreviousPage".Translate()))
        {
            SoundDefOf.Click.PlayOneShot(null);
            PageIndex--;
        }

        var rightButtonRect = new Rect(halfRectWidth + xOffset - PageButtonPosOffsetFromCentre,
            wrect.height - PageButtonHeight, PageButtonWidth, PageButtonHeight);

        if (Widgets.ButtonText(rightButtonRect, "TinyTweaks.NextPage".Translate()))
        {
            SoundDefOf.Click.PlayOneShot(null);
            PageIndex++;
        }

        Text.Anchor = TextAnchor.MiddleCenter;
        var pageNumberRect = new Rect(0, wrect.height - PageButtonHeight, wrect.width, PageButtonHeight);
        Widgets.Label(pageNumberRect, $"{PageIndex} / {MaxPageIndex}");
        Text.Anchor = TextAnchor.UpperLeft;
    }

    public static void DoWindowContents(Rect wrect)
    {
        var options = new Listing_Standard();
        var defaultColor = GUI.color;
        options.Begin(wrect);
        GUI.color = defaultColor;
        Text.Font = GameFont.Small;
        Text.Anchor = TextAnchor.UpperLeft;

        doHeading(options, Text.Font);

        switch (PageIndex)
        {
            case QoLPageIndex:
                doQualityOfLifeChanges(options);
                break;
            case BalancePageIndex:
                doBalanceChanges(options);
                break;
            case AdditionsPageIndex:
                doAdditions(options);
                break;
        }

        doPageButtons(wrect);
        if (TinyTweaks.CurrentVersion != null)
        {
            options.Gap(10);
            GUI.contentColor = Color.gray;
            options.Label("TinyTweaks.CurrentModVersion".Translate(TinyTweaks.CurrentVersion));
            GUI.contentColor = Color.white;
        }

        // Finish
        options.End();
        //Mod.GetSettings<TinyTweaksSettings>().Write();
    }

    public override void ExposeData()
    {
        Scribe_Values.Look(ref AnimalMedicalAlerts, "animalMedicalAlerts", true);
        Scribe_Values.Look(ref CaravanFoodRestrictions, "caravanFoodRestrictions", true);
        Scribe_Values.Look(ref AutoAssignAnimalFollowSettings, "autoAssignAnimalFollowSettings", true);
        Scribe_Values.Look(ref AutoRemoveMoisturePumps, "autoRemoveMoisturePumps", true);
        Scribe_Values.Look(ref AutoOwl, "autoOwl", true);
        Scribe_Values.Look(ref MedBedMedicalAlert, "medBedMedicalAlert", true);
        Scribe_Values.Look(ref AlphabeticalBillList, "alphabeticalBillList", true);
        Scribe_Values.Look(ref ShowGenderAgeCaravanFormDialog, "ShowGenderAgeCaravanFormDialog", true);

        // Restart
        Scribe_Values.Look(ref ChangeDefLabels, "changeDefLabels", true);
        Scribe_Values.Look(ref ChangeBuildableDefDesignationCategories, "changeBuildableDefDesignationCategories",
            true);
        Scribe_Values.Look(ref FixAnimalCount, "fixAnimalCount", true);
        Scribe_Values.Look(ref meleeArmourPenetrationFix, "meleeArmourPenetrationFix", true);

        Scribe_Values.Look(ref ChangeQualityDistribution, "changeQualityDistribution");
        Scribe_Values.Look(ref BloodPumpingAffectsBleeding, "bloodPumpingAffectsBleeding");
        Scribe_Values.Look(ref DelayedSkillDecay, "delayedSkillDecay");

        Scribe_Values.Look(ref RandomStartingSeason, "randomStartingSeason", true);
    }
}