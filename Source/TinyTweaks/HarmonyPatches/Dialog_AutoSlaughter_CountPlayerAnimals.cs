using HarmonyLib;
using RimWorld;

namespace TinyTweaks;

[HarmonyPatch(typeof(Dialog_AutoSlaughter), "CountPlayerAnimals")]
public static class Dialog_AutoSlaughter_CountPlayerAnimals
{
    private static void Postfix(int currentMales, int currentMalesYoung, int currentFemales,
        int currentFemalesYoung, ref int currentTotal)
    {
        if (TinyTweaksSettings.FixAnimalCount)

        {
            currentTotal = currentMales + currentMalesYoung + currentFemales + currentFemalesYoung;
        }
    }
}