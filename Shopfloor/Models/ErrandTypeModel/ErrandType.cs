using System;
using Shopfloor.Interfaces;
using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Models.ErrandTypeModel
{
    internal sealed partial class ErrandType : DataModel
    {
        private readonly ErrandTypeDTO _data;
        public ErrandType()
        {
            _data = new();
        }
        public int? Id
        {
            get => _data.Id;
            init => _data.Id = value;
        }
        public required string Name
        {
            get => _data.Name;
            init => _data.Name = value;
        }
        public required string? Description
        {
            get => _data.Description;
            init => _data.Description = value;
        }
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