using Shopfloor.Interfaces;
using System;

namespace Shopfloor.Models
{
    public class Supplier : IEquatable<Supplier>, ISearchableModel
    {
        private readonly int? _id;
        private readonly string _name;
        private readonly bool _isActive;
        public int? Id => _id;
        public string Name => _name;
        public bool IsActive => _isActive;
        public string SearchValue => _name;
        public Supplier(int id, string name, bool isActive)
        {
            _id = id;
            _name = name;
            _isActive = isActive;
        }
        public Supplier(string name, bool isActive)
        {
            _name = name;
            _isActive = isActive;
        }
        public bool Equals(Supplier? other)
        {
            if (other == null) return false;
            if (_id == null && other._id == null) return _name == other.Name;
            return _id == other.Id;
        }
        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (obj is not Supplier) return false;
            return Equals(obj);
        }
        public override int GetHashCode()
        {
            if (_id != null) return _id.GetHashCode();
            return _name.GetHashCode();
        }
    }
}