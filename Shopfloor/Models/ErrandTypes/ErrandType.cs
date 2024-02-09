using System;

namespace Shopfloor.Models.ErrandTypeModel
{
    internal sealed partial class ErrandType
    {
        private readonly int? _id;
        private readonly string _name;
        private readonly string? _description;
        public ErrandType(string name, string? description)
        {
            _name = name;
            _description = description;
        }
        public ErrandType(int id, string name, string? description)
        {
            _id = id;
            _name = name;
            _description = description;
        }
        public int? Id => _id;
        public string Name => _name;
        public string? Description => _description;
    }
    internal sealed partial class ErrandType : IEquatable<ErrandType>
    {
        public bool Equals(ErrandType? other)
        {
            if (other == null) return false;
            return _id == other.Id;
        }
        public override bool Equals(object? obj) => obj is ErrandType objErrandType && Equals(objErrandType);
        public override int GetHashCode()
        {
            if (_id != null) return _id.GetHashCode();
            return _name.GetHashCode();
        }
    }
}