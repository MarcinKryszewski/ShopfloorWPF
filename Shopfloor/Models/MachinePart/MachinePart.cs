using Shopfloor.Models.MachineModel;
using Shopfloor.Models.PartModel;

namespace Shopfloor.Models.MachinePartModel
{
    public class MachinePart
    {
        private readonly Part? _part;
        private readonly Machine? _machine;
        private readonly int? _machineId;
        private readonly int? _partId;
        private readonly double _amount;
        public Part? Part => _part;
        public int? PartId => _partId;
        public Machine? Machine => _machine;
        public int? MachineId => _machineId;
        public double Amount => _amount;
        public MachinePart(Part part, Machine machine, double? amount = 1)
        {
            _part = part;
            _partId = part.Id;
            _machine = machine;
            _machineId = machine.Id;
            _amount = amount ?? 1;
        }
        public MachinePart(int partId, int machineId, double? amount = 1)
        {
            _partId = partId;
            _machineId = machineId;
            _amount = amount ?? 1;
        }
    }
}