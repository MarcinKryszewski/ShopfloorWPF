using System;

namespace Shopfloor.Models
{
    public class Supplier : IEquatable<Supplier>
    {
        private readonly int _id;
        private readonly string? _name;
        private readonly bool _isActive;

        public int Id => _id;
        public string Name => _name ?? string.Empty;
        public bool IsActive => _isActive;
        public string SearchValue => _name ?? string.Empty;

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

        public bool Equals(Supplier? other)
        {
            if (other == null) return false;
            return _id == other.Id;
        }
    }
}