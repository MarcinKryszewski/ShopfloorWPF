namespace Shopfloor.Models
{
    public class MachinePart
    {
        public const string DefaultUnit = "szt.";
        private readonly Part? _part;
        private readonly Machine? _machine;
        private readonly int? _machineId;
        private readonly int? _partId;
        private readonly double _amount;
        private readonly string _unit;
        public Part? Part => _part;
        public int? PartId => _partId;
        public Machine? Machine => _machine;
        public int? MachineId => _machineId;
        public double Amount => _amount;
        public string Unit => _unit;
        public MachinePart(Part part, Machine machine, double? amount = 1, string? unit = DefaultUnit)
        {
            _part = part;
            _partId = part.Id;
            _machine = machine;
            _machineId = machine.Id;
            _amount = amount ?? 1;
            if (unit is null || unit.Trim().Length == 0) _unit = DefaultUnit;
            else _unit = unit.Trim();
        }
        public MachinePart(int partId, int machineId, double? amount = 1, string? unit = DefaultUnit)
        {
            _partId = partId;
            _machineId = machineId;
            _amount = amount ?? 1;
            if (unit is null || unit.Trim().Length == 0) _unit = DefaultUnit;
            else _unit = unit.Trim();
        }
    }
}