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
        private Part? _part;
        [SetsRequiredMembers]
        public MachinePart(Part part, Machine machine)
        {
            _data = new();
            _part = part;
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
        public Part? Part
        {
            get => _part;
            set => _part = value;
        }
        required public int? PartId
        {
            get => _data.PartId;
            init => _data.PartId = value;
        }
    }
}