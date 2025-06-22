using HarmonyLib;
using RimWorld;

namespace TinyTweaks;

[HarmonyPatch(typeof(CompLaunchable), nameof(CompLaunchable.TryLaunch))]
public static class CompLaunchable_TryLaunch
{
    public static void Postfix(CompLaunchable __instance)
    {
        __instance.parent.BroadcastCompSignal(CompLaunchableAutoRebuild.AutoRebuildSignal);
    }
}