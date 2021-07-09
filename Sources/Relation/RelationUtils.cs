using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace OrderOfBirth
{
    public static class RelationUtils
    {
        public static string? GetStringForRelationTo(this Pawn pawn, Pawn otherPawn) =>
            pawn.GetStringForRelationTo(otherPawn, pawn.GetMostImportantRelation(otherPawn));

        private static string? GetStringForRelationTo(this Pawn pawn, Pawn otherPawn, PawnRelationDef? pawnRelationDef)
        {
            if (pawnRelationDef == null)
            {
                return null;
            }

            if (pawnRelationDef == PawnRelationDefOf.Sibling)
            {
                if (otherPawn.gender == Gender.Female)
                {
                    return pawn.IsYoungerThan(otherPawn)
                        ? "OlderSister.Label".Translate()
                        : "YoungerSister.Label".Translate();
                }

                return pawn.IsYoungerThan(otherPawn)
                    ? "OlderBrother.Label".Translate()
                    : "YoungerBrother.Label".Translate();
            }

            if (pawnRelationDef == PawnRelationDefOf.HalfSibling)
            {
                if (pawn.IsSameMotherAs(otherPawn))
                {
                    if (otherPawn.gender == Gender.Female)
                    {
                        return pawn.IsYoungerThan(otherPawn)
                            ? "OlderSisterWithDifferentFathers.Label".Translate()
                            : "YoungerSisterWithDifferentFathers.Label".Translate();
                    }

                    return pawn.IsYoungerThan(otherPawn)
                        ? "OlderBrotherWithDifferentFathers.Label".Translate()
                        : "YoungerBrotherWithDifferentFathers.Label".Translate();
                }

                if (otherPawn.gender == Gender.Female)
                {
                    return pawn.IsYoungerThan(otherPawn)
                        ? "OlderSisterWithDifferentMothers.Label".Translate()
                        : "YoungerSisterWithDifferentMothers.Label".Translate();
                }

                return pawn.IsYoungerThan(otherPawn)
                    ? "OlderBrotherWithDifferentMothers.Label".Translate()
                    : "YoungerBrotherWithDifferentMothers.Label".Translate();
            }

            if (pawnRelationDef == PawnRelationDefOf.Cousin)
            {
                if (otherPawn.gender == Gender.Female)
                {
                    return pawn.IsYoungerThan(otherPawn)
                        ? "OlderFemaleCousin.Label".Translate()
                        : "YoungerFemaleCousin.Label".Translate();
                }

                return pawn.IsYoungerThan(otherPawn)
                    ? "OlderMaleCousin.Label".Translate()
                    : "YoungerMaleCousin.Label".Translate();
            }

            if (pawnRelationDef == DefDatabase<PawnRelationDef>.GetNamed("SecondCousin"))
            {
                if (otherPawn.gender == Gender.Female)
                {
                    return pawn.IsYoungerThan(otherPawn)
                        ? "OlderFemaleSecondCousin.Label".Translate()
                        : "YoungerFemaleSecondCousin.Label".Translate();
                }

                return pawn.IsYoungerThan(otherPawn)
                    ? "OlderMaleSecondCousin.Label".Translate()
                    : "YoungerMaleSecondCousin.Label".Translate();
            }

            if (pawnRelationDef == PawnRelationDefOf.UncleOrAunt)
            {
                if (otherPawn.gender == Gender.Female)
                {
                    return pawn.MyParentWhichIsSiblingTo(otherPawn)?.IsYoungerThan(otherPawn) == true
                        ? "OlderSisterOfMyParent.Label".Translate()
                        : "YoungerSisterOfMyParent.Label".Translate();
                }

                return pawn.MyParentWhichIsSiblingTo(otherPawn)?.IsYoungerThan(otherPawn) == true
                    ? "OlderBrotherOfMyParent.Label".Translate()
                    : "YoungerBrotherOfMyParent.Label".Translate();
            }

            if (pawnRelationDef == PawnRelationDefOf.GranduncleOrGrandaunt)
            {
                if (otherPawn.gender == Gender.Female)
                {
                    return pawn.MyGrandparentWhichIsSiblingTo(otherPawn)?.IsYoungerThan(otherPawn) == true
                        ? "OlderSisterOfMyGrandparent.Label".Translate()
                        : "YoungerSisterOfMyGrandparent.Label".Translate();
                }

                return pawn.MyGrandparentWhichIsSiblingTo(otherPawn)?.IsYoungerThan(otherPawn) == true
                    ? "OlderBrotherOfMyGrandparent.Label".Translate()
                    : "YoungerBrotherOfMyGrandparent.Label".Translate();
            }

            return null;
        }

        private static bool IsYoungerThan(this Pawn pawn, Pawn otherPawn) =>
            pawn.ageTracker.BirthAbsTicks > otherPawn.ageTracker.BirthAbsTicks ||
            pawn.ageTracker.BirthAbsTicks == otherPawn.ageTracker.BirthAbsTicks &&
            pawn.thingIDNumber > otherPawn.thingIDNumber;

        private static bool IsSameFatherAs(this Pawn pawn, Pawn otherPawn) =>
            pawn.GetFather() != null && pawn.GetFather() == otherPawn.GetFather();

        private static bool IsSameMotherAs(this Pawn pawn, Pawn otherPawn) =>
            pawn.GetMother() != null && pawn.GetMother() == otherPawn.GetMother();

        private static Pawn? MyParentWhichIsSiblingTo(this Pawn pawn, Pawn otherPawn) =>
            pawn.Parents().FirstOrDefault(it => it.IsSiblingTo(otherPawn));

        private static Pawn? MyGrandparentWhichIsSiblingTo(this Pawn pawn, Pawn otherPawn) =>
            pawn.Grandparents().FirstOrDefault(it => it.IsSiblingTo(otherPawn));

        private static bool IsSiblingTo(this Pawn? pawn, Pawn? otherPawn) =>
            pawn != null && otherPawn != null && (pawn.IsSameFatherAs(otherPawn) || pawn.IsSameMotherAs(otherPawn));

        private static IEnumerable<Pawn> Parents(this Pawn pawn)
        {
            if (pawn.GetFather() != null)
            {
                yield return pawn.GetFather();
            }

            if (pawn.GetMother() != null)
            {
                yield return pawn.GetMother();
            }
        }

        private static IEnumerable<Pawn> Grandparents(this Pawn pawn) => pawn.Parents().SelectMany(it => it.Parents());
    }
}