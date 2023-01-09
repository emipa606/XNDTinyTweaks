using Verse;
#if DEBUG
using HarmonyLib;
#endif
namespace TinyTweaks;

[StaticConstructorOnStartup]
public static class HarmonyPatches
{
    static HarmonyPatches()
    {
#if DEBUG
            Harmony.DEBUG = true;
#endif

        TinyTweaks.harmonyInstance.PatchAll();
    }
}