using Shopfloor.Interfaces;
using Shopfloor.Models.MachinePartModel;
using Shopfloor.Shared.BaseClasses;
using System;
using System.Collections.Generic;

namespace Shopfloor.Models.MachineModel
{
    internal sealed partial class Machine : DataModel
    {
        private readonly MachineDTO _data;
        public int? Id
        {
            get => _data.Id;
            init => _data.Id = value;
        }
        public required string Name
        {
            get => _data.Name;
            init
            {
                _data.Name = value;
                Path = value;
            }
        }
        public string? Number
        {
            get => _data.Number ?? string.Empty;
            init => _data.Number = value;
        }
        public string? SapNumber
        {
            get => _data.SapNumber ?? string.Empty;
            init => _data.SapNumber = value;
        }
        public bool IsActive
        {
            get => _data.Active;
            set => _data.Active = value;
        }
        public int? ParentId
        {
            get => _data.Parent;
            init => _data.Parent = value;
        }
        public Machine()
        {
            _data = new();
        }
    }
    internal sealed partial class Machine
    {
        private readonly List<Machine> _children = [];
        private readonly List<MachinePart> _parts = [];
        private Machine? _parent;
        public IList<Machine> Children => _children.AsReadOnly();
        public IList<MachinePart> Parts => _parts;
        public void AddChild(Machine machine)
        {
            _children.Add(machine);
            machine.SetParent(this);
        }
        public void SetParent(Machine parent)
        {
            Path = $@"{parent.Path}\{Path}";
            _parent = parent;
            _data.Parent = parent.Id;
        }
    }
    internal sealed partial class Machine : ISearchableModel
    {
        //private string _path;
        public string Path { get; private set; } = string.Empty;
        public string SearchValue => Path.Replace(@"\", string.Empty);
    }
    internal sealed partial class Machine : IEquatable<Machine>
    {
        public bool Equals(Machine? other)
        {
            if (other == null) return false;
            if (_data.Id == null && other._data.Id == null)
            {
                return _data.Name == other.Name && _data.Number == other.Number;
            }

            return _data.Id == other.Id;
        }
        public override bool Equals(object? obj) => obj is Machine objMachine && Equals(objMachine);
        public override int GetHashCode()
        {
            if (_data.Id != null) return _data.Id.GetHashCode();
            return Path.GetHashCode();
        }
    }
}