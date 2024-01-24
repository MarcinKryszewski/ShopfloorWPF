using Shopfloor.Interfaces;
using System;

namespace Shopfloor.Models.PartTypeModel
{
    public class PartType : IEquatable<PartType>, ISearchableModel
    {
        private readonly int? _id;
        private readonly string _name;
        public PartType(int id, string name)
        {
            _id = id;
            _name = name;
        }
        public PartType(string name)
        {
            _name = name;
        }
        public int? Id => _id;
        public string Name => _name;
        public string SearchValue => _name ?? string.Empty;

        public bool Equals(PartType? other)
        {
            if (other == null) return false;
            if (_id == null && other._id == null) return _name == other.Name;
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