using HarmonyLib;
using RimWorld;
using Verse;

namespace TinyTweaks;

[HarmonyPatch(typeof(Pawn_PlayerSettings), MethodType.Constructor, typeof(Pawn))]
public static class Pawn_PlayerSettings_Constructor
{
    public static void Postfix(Pawn_PlayerSettings __instance, Pawn pawn)
    {
        // If the pawn is an animal, automatically assign them to follow when drafted/doing fieldwork
        if (!TinyTweaksSettings.AutoAssignAnimalFollowSettings || !pawn.RaceProps.Animal)
        {
            return;
        }

        __instance.followDrafted = true;
        __instance.followFieldwork = true;
    }
}