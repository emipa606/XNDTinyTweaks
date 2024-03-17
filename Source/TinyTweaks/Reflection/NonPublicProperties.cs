using System;
using System.Reflection;
using RimWorld.Planet;
using Verse;

namespace TinyTweaks;

[StaticConstructorOnStartup]
public static class NonPublicProperties
{
    public static readonly Func<WITab, Caravan> WITab_get_SelCaravan = (Func<WITab, Caravan>)
        Delegate.CreateDelegate(typeof(Func<WITab, Caravan>), null,
            typeof(WITab).GetProperty("SelCaravan", BindingFlags.Instance | BindingFlags.NonPublic)
                ?.GetGetMethod(true)!);
}