using Shopfloor.Interfaces;
using System;
using System.Collections.Generic;

namespace Shopfloor.Models
{
    public class Machine : ISearchableModel, IEquatable<Machine>
    {
        private readonly List<Machine> _children;
        private readonly List<MachinePart> _parts;
        private Machine? _parent;
        private readonly int? _id;

        private string _path;
        public int? Id => _id;
        public string Name { get; }
        public string Number { get; }
        public bool IsActive { get; }
        public int? ParentId { get; }

        public IList<Machine> Children
        {
            get
            {
                return _children.AsReadOnly();
            }
        }

        public IList<MachinePart> Parts => _parts;
        public string Path => _path;
        public string SearchValue => _path.Replace(@"\", "");

        public Machine(int id, string name, string number, int? parent, bool isActive)
        {
            _id = id;
            Name = name;
            Number = number;
            ParentId = parent;
            _children = new();
            _parts = new();
            _path = Name;
            IsActive = isActive;
        }

        public Machine(string name, string number, int? parent, bool isActive)
        {
            Name = name;
            Number = number;
            ParentId = parent;
            _children = new();
            _parts = new();
            _path = Name;
            IsActive = isActive;
        }

        public void AddChild(Machine machine)
        {
            _children.Add(machine);
            machine.SetParent(this);
        }

        public void SetParent(Machine machine)
        {
            _path = $@"{machine.Path}\{_path}";
            _parent = machine;
        }

        public bool Equals(Machine? other)
        {
            if (other == null) return false;
            return _id == other.Id;
        }
    }
}