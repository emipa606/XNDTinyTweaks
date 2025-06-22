using HarmonyLib;
using RimWorld;
using Verse;

namespace TinyTweaks;

[HarmonyPatch(typeof(Pawn), nameof(Pawn.SetFaction), typeof(Faction), typeof(Pawn))]
public static class Pawn_SetFaction

{
    public static void Postfix(Pawn __instance)
    {
        if (!TinyTweaksSettings.AutoOwl)
        {
            return;
        }

        if (__instance is null)
        {
            return;
        }

        TinyTweaks.SetNightOwl(__instance);
    }
}