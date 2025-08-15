using HarmonyLib;
using Verse;

namespace TinyTweaks;

[HarmonyPatch(typeof(Thing), nameof(Thing.SpawnSetup))]
public static class Thing_SpawnSetup
{
    public static void Postfix(Thing __instance, bool respawningAfterLoad)
    {
        if (!TinyTweaksSettings.AutoOwl || respawningAfterLoad)
        {
            return;
        }

        if (__instance is not Pawn pawn)
        {
            return;
        }

        TinyTweaks.SetNightOwl(pawn);
    }
}