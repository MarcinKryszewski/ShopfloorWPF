using Shopfloor.Models.MachineModel;
using Shopfloor.Models.PartModel;

namespace Shopfloor.Models.MachinePartModel
{
    internal sealed class MachinePart
    {
        private readonly Part? _part;
        private readonly Machine? _machine;
        private readonly MachinePartDTO _data = new();
        public Part? Part => _part;
        public int? PartId => _data.PartId;
        public Machine? Machine => _machine;
        public int? MachineId => _data.MachineId;
        public double? Amount => _data.Amount;
        public MachinePart(Part part, Machine machine, double? amount = 1)
        {
            _part = part;
            _data.PartId = part.Id;
            _machine = machine;
            _data.MachineId = machine.Id;
            _data.Amount = amount ?? 1;
        }
        public MachinePart(int partId, int machineId, double? amount = 1)
        {
            _data.PartId = partId;
            _data.MachineId = machineId;
            _data.Amount = amount ?? 1;
        }
    }
}