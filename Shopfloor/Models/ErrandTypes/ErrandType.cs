using System;

namespace Shopfloor.Models.ErrandTypeModel
{
    internal sealed partial class ErrandType
    {
        private readonly ErrandTypeDTO _data = new();
        public ErrandType(string name, string? description)
        {
            _data.Name = name;
            _data.Description = description;
        }
        public ErrandType(int id, string name, string? description)
        {
            _data.Id = id;
            _data.Name = name;
            _data.Description = description;
        }
        public int? Id => _data.Id;
        public string Name => _data.Name;
        public string? Description => _data.Description;
    }
    internal sealed partial class ErrandType : IEquatable<ErrandType>
    {
        public bool Equals(ErrandType? other)
        {
            if (other == null) return false;
            return _data.Id == other.Id;
        }
        public override bool Equals(object? obj) => obj is ErrandType objErrandType && Equals(objErrandType);
        public override int GetHashCode()
        {
            if (_data.Id != null) return _data.Id.GetHashCode();
            return _data.Name.GetHashCode();
        }
    }
}