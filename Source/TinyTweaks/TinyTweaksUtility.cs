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

    extension(Pawn p)
    {
        public bool PlayerColonyAnimal()
        {
            return p.Faction == Faction.OfPlayer && p.RaceProps.Animal;
        }

        public bool PlayerColonyAnimal_Alive_NoCryptosleep()
        {
            return p.PlayerColonyAnimal() && !p.Dead && !p.Suspended;
        }

        public bool HasBondRelation()
        {
            return TrainableUtility.GetAllColonistBondsFor(p).Any();
        }
    }
}