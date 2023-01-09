using HarmonyLib;
using RimWorld;

namespace TinyTweaks;

public static class Patch_Dialog_AutoSlaughter_CountPlayerAnimals
{
    [HarmonyPatch(typeof(Dialog_AutoSlaughter),
        "CountPlayerAnimals")] // new Type[] { typeof(Map), typeof(AutoSlaughterConfig), typeof(ThingDef), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int) }, new ArgumentType[] { ArgumentType.Normal, ArgumentType.Normal, ArgumentType.Normal, ArgumentType.Out, ArgumentType.Out, ArgumentType.Out, ArgumentType.Out, ArgumentType.Out, ArgumentType.Out, ArgumentType.Out, ArgumentType.Out })]
    public static class Dialog_AutoSlaughter_CountPlayerAnimals
    {
        private static void Postfix(int currentMales, int currentMalesYoung, int currentFemales,
            int currentFemalesYoung, ref int currentTotal)
        {
            if (TinyTweaksSettings.fixAnimalCount)

            {
                currentTotal = currentMales + currentMalesYoung + currentFemales + currentFemalesYoung;
            }
        }
    }
}