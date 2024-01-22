namespace Shopfloor.Models
{
    public class MachinePart
    {
        public const string DefaultUnit = "szt.";
        private readonly Part _part;
        private readonly double _amount;
        private readonly string _unit;
        public Part Part => _part;
        public double Amount => _amount;
        public string Unit => _unit;
        public MachinePart(Part part, double? amount = 1, string? unit = DefaultUnit)
        {
            _part = part;
            _amount = amount ?? 1;
            if (unit is null || unit.Trim().Length == 0) _unit = DefaultUnit;
            else _unit = unit.Trim();
        }
    }
}