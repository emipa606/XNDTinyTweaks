using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace TinyTweaks;

[StaticConstructorOnStartup]
public static class TinyTweaksUtility
{
    public static void AddComp(this ThingDef def, Type compType)
    {
        if (def.comps.NullOrEmpty())
        {
            def.comps = [];
        }

        def.comps.Add(new CompProperties(compType));
    }

    public static List<Pawn> SortedAnimalList(List<Pawn> pawnList)
    {
        pawnList.SortBy(p => !p.HasBondRelation(), p => p.LabelShort);
        return pawnList;
    }

    public static bool PlayerColonyAnimal(this Pawn p)
    {
        return p.Faction == Faction.OfPlayer && p.RaceProps.Animal;
    }

    public static bool PlayerColonyAnimal_Alive_NoCryptosleep(this Pawn p)
    {
        return p.PlayerColonyAnimal() && !p.Dead && !p.Suspended;
    }

    public static bool HasBondRelation(this Pawn p)
    {
        return TrainableUtility.GetAllColonistBondsFor(p).Any();
    }
}