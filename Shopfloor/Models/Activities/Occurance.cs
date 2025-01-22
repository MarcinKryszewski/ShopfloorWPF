using System;

namespace Shopfloor.Models.Activities
{
    internal class Occurance : IEquatable<Occurance>
    {
        public string OccuranceUnitText => Unit.ToString();
        public OccuranceUnit Unit { get; set; } = OccuranceUnit.M;
        public bool Equals(Occurance? other)
        {
            if (other == null)
            {
                return false;
            }

            return Unit == other.Unit;
        }
        public override bool Equals(object? obj)
        {
            if (obj is not Occurance occurance)
            {
                return false;
            }

            return Equals(occurance);
        }
        public override int GetHashCode()
        {
            return Unit.GetHashCode();
        }
    }
}