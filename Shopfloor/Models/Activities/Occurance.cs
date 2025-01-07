namespace Shopfloor.Models.Activities
{
    internal class Occurance
    {
        public string OccuranceUnitText => Unit.ToString();
        public OccuranceUnit Unit { get; set; } = OccuranceUnit.M;
    }
}