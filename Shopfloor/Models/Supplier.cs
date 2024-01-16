using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models
{
    public class Supplier
    {
        private readonly int _id;
        private readonly string? _name;
        private readonly bool _isActive;

        public int Id => _id;
        public string Name => _name ?? string.Empty;
        public bool IsActive => _isActive;

        public Supplier(int id, string? name, bool isActive)
        {
            _id = id;
            _name = name;
            _isActive = isActive;
        }
        public Supplier(string? name, bool isActive)
        {
            _name = name;
            _isActive = isActive;
        }
    }
}