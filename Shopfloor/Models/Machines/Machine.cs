using Shopfloor.Interfaces;
using Shopfloor.Models.MachinePartModel;
using Shopfloor.Models.PartTypeModel;
using System;
using System.Collections.Generic;

namespace Shopfloor.Models.MachineModel
{
    public partial class Machine
    {
        private readonly int? _id;
        private readonly string _name;
        private readonly string? _number;
        private readonly bool _isActive;
        private int? _parentId;
        private readonly string? _sapNumber;
        public int? Id => _id;
        public string Name => _name;
        public string Number => _number ?? string.Empty;
        public string SapNumber => _sapNumber ?? string.Empty;
        public bool IsActive => _isActive;
        public int? ParentId => _parentId;
        public Machine(int id, string name, string? number, string? sapNumber, int? parent, bool isActive)
        {
            _id = id;
            _name = name;
            _number = number;
            _sapNumber = sapNumber;
            _parentId = parent;
            _path = Name;
            _isActive = isActive;
        }
        public Machine(string name, string number, string? sapNumber, int? parent, bool isActive)
        {
            _name = name;
            _name = name;
            _number = number;
            _sapNumber = sapNumber;
            _parentId = parent;
            _path = Name;
            _isActive = isActive;
        }
    }
    public partial class Machine
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
            _parentId = parent.Id;
        }

    }
    public partial class Machine : ISearchableModel
    {
        private string _path;
        public string Path => _path;
        public string SearchValue => _path.Replace(@"\", string.Empty);
    }
    public partial class Machine : IEquatable<Machine>
    {
        public bool Equals(Machine? other)
        {
            if (other == null) return false;
            if (_id == null && other._id == null)
            {
                return _name == other.Name && _number == other.Number;
            }

            return _id == other.Id;
        }
        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (obj is not PartType) return false;
            return Equals(obj);
        }
        public override int GetHashCode()
        {
            if (_id != null) return _id.GetHashCode();
            return _name.GetHashCode();
        }
    }
}