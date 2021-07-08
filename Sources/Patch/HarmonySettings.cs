using HarmonyLib;
using Verse;

// ReSharper disable UnusedType.Global

namespace OrderOfBirth
{
    [StaticConstructorOnStartup]
    internal static class HarmonySettings
    {
        static HarmonySettings()
        {
            var harmony = new Harmony("d6e48f98-daee-1954-af49-7e91bdf977d5");
            harmony.PatchAll();
        }
    }
}