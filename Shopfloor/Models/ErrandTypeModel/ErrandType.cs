using System;
using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Models.ErrandTypeModel
{
    internal sealed partial class ErrandType : DataModel
    {
        private readonly ErrandTypeDto _data;
        public ErrandType()
        {
            _data = new();
        }
        required public string? Description
        {
            get => _data.Description;
            init => _data.Description = value;
        }
        required public int? Id
        {
            get => _data.Id;
            init => _data.Id = value;
        }
        required public string Name
        {
            get => _data.Name;
            init => _data.Name = value;
        }
    }
    internal sealed partial class ErrandType : IEquatable<ErrandType>
    {
        public bool Equals(ErrandType? other)
        {
            if (other == null)
            {
                return false;
            }

            return _data.Id == other.Id;
        }
        public override bool Equals(object? obj) => obj is ErrandType objErrandType && Equals(objErrandType);
        public override int GetHashCode()
        {
            if (_data.Id != null)
            {
                return _data.Id.GetHashCode();
            }

            return _data.Name.GetHashCode();
        }
    }
}