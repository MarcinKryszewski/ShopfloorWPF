using System.Collections.Generic;

namespace Shopfloor.Models
{
    public class Machine
    {
        public int Id { get; }
        public string Name { get; }
        public string Number { get; }
        public int? ParentId { get; }
        public List<Machine> Children { get; }

        public Machine(int id, string name, string number, int? parent)
        {
            Id = id;
            Name = name;
            Number = number;
            ParentId = parent;
            Children = new List<Machine>();
        }

        public Machine(string name, string number, int parent)
        {
            Name = name;
            Number = number;
            ParentId = parent;
            Children = new List<Machine>();
        }
    }
}