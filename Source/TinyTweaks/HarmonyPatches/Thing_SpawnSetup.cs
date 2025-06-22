using HarmonyLib;
using Verse;

namespace TinyTweaks;

[HarmonyPatch(typeof(Thing), nameof(Thing.SpawnSetup))]
public static class Thing_SpawnSetup
{
    public static void Postfix(Thing __instance)
    {
        if (!TinyTweaksSettings.AutoOwl)
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