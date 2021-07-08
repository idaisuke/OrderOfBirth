using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;

// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Local
// ReSharper disable UnusedParameter.Local
// ReSharper disable UnusedType.Global

namespace OrderOfBirth
{
    internal static class SocialCardUtilityPatch
    {
        private static readonly FieldInfo OtherPawnField = AccessTools.Field(
            AccessTools.Inner(typeof(SocialCardUtility), "CachedSocialTabEntry"),
            "otherPawn");

        private static readonly FieldInfo RelationsField = AccessTools.Field(
            AccessTools.Inner(typeof(SocialCardUtility), "CachedSocialTabEntry"),
            "relations");

        [HarmonyPatch(typeof(SocialCardUtility), "GetRelationsString")]
        internal static class GetRelationsString
        {
            private static bool Prefix(ref string __result, ref object entry, Pawn selPawnForSocialInfo)
            {
                var otherPawn = (Pawn) OtherPawnField.GetValue(entry);
                var relations = (List<PawnRelationDef>) RelationsField.GetValue(entry);

                if (relations.Count == 0)
                {
                    return true;
                }

                var relationString = selPawnForSocialInfo.GetStringForRelationTo(otherPawn);
                if (relationString == null)
                {
                    __result = selPawnForSocialInfo.GetMostImportantRelation(otherPawn)
                        .GetGenderSpecificLabelCap(otherPawn);
                    return false;
                }

                __result = relationString.CapitalizeFirst();
                return false;
            }
        }
    }
}