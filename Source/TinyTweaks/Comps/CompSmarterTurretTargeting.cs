using Verse;

namespace TinyTweaks;

public class CompSmarterTurretTargeting : ThingComp
{
    private bool attackingNonDownedPawn;

    public override void PostExposeData()
    {
        Scribe_Values.Look(ref attackingNonDownedPawn, "attackingNonDownedPawn");
    }
}