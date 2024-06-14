using System.Diagnostics.CodeAnalysis;
using Shopfloor.Interfaces;
using Shopfloor.Models.MachineModel;
using Shopfloor.Models.PartModel;
using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Models.MachinePartModel
{
    internal sealed class MachinePart : DataModel
    {
        private Part? _part;
        private Machine? _machine;
        private readonly MachinePartDTO _data;
        public Part? Part
        {
            get => _part;
            set => _part = value;
        }
        public required int? PartId
        {
            get => _data.PartId;
            init => _data.PartId = value;
        }
        public Machine? Machine => _machine;
        public required int? MachineId
        {
            get => _data.MachineId;
            init => _data.MachineId = value;
        }
        public double? Amount
        {
            get => _data.Amount;
            init => _data.Amount = value ?? 1;
        }
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
    }
}