using Verse;

namespace TinyTweaks;

[StaticConstructorOnStartup]
public static class ModCompatibilityCheck
{
    public static bool DubsBadHygiene;

    static ModCompatibilityCheck()
    {
        DubsBadHygiene = ModLister.GetActiveModWithIdentifier("Dubwise.DubsBadHygiene", true) != null;
    }
}