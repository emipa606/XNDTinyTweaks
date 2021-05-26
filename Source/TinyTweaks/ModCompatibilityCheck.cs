using System;
using System.Linq;
using Verse;

namespace TinyTweaks
{
    [StaticConstructorOnStartup]
    public static class ModCompatibilityCheck
    {
        public static bool DubsBadHygiene;

        static ModCompatibilityCheck()
        {
            var loadedMods = ModsConfig.ActiveModsInLoadOrder.ToList();

            for (var i = 0; i < loadedMods.Count; i++)
            {
                var curMod = loadedMods[i];

                if (curMod.PackageId.Equals("Dubwise.DubsBadHygiene", StringComparison.CurrentCultureIgnoreCase))
                {
                    DubsBadHygiene = true;
                }
            }
        }
    }
}