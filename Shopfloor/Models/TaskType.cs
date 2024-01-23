using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models
{
    public class TaskType
    {
        private readonly int? _id;
        private readonly string _name;
        private readonly string? _description;
        public TaskType(string name, string? description)
        {
            _name = name;
            _description = description;
        }
        public TaskType(int id, string name, string? description)
        {
            _id = id;
            _name = name;
            _description = description;
        }
        public int? Id => _id;
        public string Name => _name;
        public string? Description => _description;
    }
}