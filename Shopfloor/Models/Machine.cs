using System.Collections.Generic;

namespace Shopfloor.Models
{
    public class Machine
    {
        private readonly List<Machine> _children;
        private string _path;
        public int Id { get; }
        public string Name { get; }
        public string Number { get; }
        public int? ParentId { get; }
        public IList<Machine> Children
        {
            get
            {
                return _children.AsReadOnly();
            }
        }
        public string Path => _path;

        public Machine(int id, string name, string number, int? parent)
        {
            Id = id;
            Name = name;
            Number = number;
            ParentId = parent;
            _children = new();
            _path = Name;
        }

        public Machine(string name, string number, int? parent)
        {
            Name = name;
            Number = number;
            ParentId = parent;
            _children = new();
            _path = Name;
        }

        public void AddChild(Machine machine)
        {
            _children.Add(machine);
            machine.SetParent(this);
        }

        public void SetParent(Machine machine)
        {
            _path = $@"{machine.Name}\{_path}";
        }
    }
}