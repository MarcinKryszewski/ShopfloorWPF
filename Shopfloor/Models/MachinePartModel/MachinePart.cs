using System.Diagnostics.CodeAnalysis;
using Shopfloor.Models.MachineModel;
using Shopfloor.Models.PartModel;
using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Models.MachinePartModel
{
    internal sealed class MachinePart : DataModel
    {
        private readonly MachinePartDto _data;
        private readonly Machine? _machine;

        [SetsRequiredMembers]
        public MachinePart(Part part, Machine machine)
        {
            _data = new();
            Part = part;
            PartId = part.Id;
            _machine = machine;
            MachineId = machine.Id;
        }
        public MachinePart()
        {
            _data = new();
        }
        public double? Amount
        {
            get => _data.Amount;
            init => _data.Amount = value ?? 1;
        }
        public Machine? Machine => _machine;
        required public int? MachineId
        {
            get => _data.MachineId;
            init => _data.MachineId = value;
        }
        public Part? Part { get; set; }
        required public int? PartId
        {
            get => _data.PartId;
            init => _data.PartId = value;
        }
    }
}