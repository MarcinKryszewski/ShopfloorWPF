using Shopfloor.Interfaces;
using Shopfloor.Models.MachinePartModel;
using System;
using System.Collections.Generic;

namespace Shopfloor.Models.MachineModel
{
    internal sealed partial class Machine
    {
        private readonly MachineDTO _data = new();
        public int? Id => _data.Id;
        public string Name => _data.Name;
        public string Number => _data.Number ?? string.Empty;
        public string SapNumber => _data.SapNumber ?? string.Empty;
        public bool IsActive => _data.Active;
        public int? ParentId => _data.Parent;
        public Machine(int id, string name, string? number, string? sapNumber, int? parent, bool isActive)
        {
            _data.Id = id;
            _data.Name = name;
            _data.Number = number;
            _data.SapNumber = sapNumber;
            _data.Parent = parent;
            _path = Name;
            _data.Active = isActive;
        }
        public Machine(string name, string number, string? sapNumber, int? parent, bool isActive)
        {
            _data.Name = name;
            _data.Name = name;
            _data.Number = number;
            _data.SapNumber = sapNumber;
            _data.Parent = parent;
            _path = Name;
            _data.Active = isActive;
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
            _path = $@"{parent.Path}\{_path}";
            _parent = parent;
            _data.Parent = parent.Id;
        }
    }
    internal sealed partial class Machine : ISearchableModel
    {
        private string _path;
        public string Path => _path;
        public string SearchValue => _path.Replace(@"\", string.Empty);
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
            return _path.GetHashCode();
        }
    }
}