using Shopfloor.Interfaces;
using Shopfloor.Shared.BaseClasses;
using System;

namespace Shopfloor.Models.PartTypeModel
{
    internal sealed partial class PartType : DataModel
    {
        private readonly PartTypeDTO _data;
        public PartType(int id, string name)
        {
            _data = new()
            {
                Id = id,
                Name = name
            };
        }
        public PartType(string name)
        {
            _data = new()
            {
                Name = name
            };
        }
        public int? Id => _data.Id;
        public string Name => _data.Name;
    }
    internal sealed partial class PartType : IEquatable<PartType>
    {
        public bool Equals(PartType? other)
        {
            if (other == null) return false;
            if (Id == null && other.Id == null) return Name == other.Name;
            return Id == other.Id;
        }
        public override bool Equals(object? obj) => obj is PartType objPartType && Equals(objPartType);
        public override int GetHashCode()
        {
            if (Id != null) return Id.GetHashCode();
            return Name.GetHashCode();
        }
    }
    internal sealed partial class PartType : ISearchableModel
    {
        public string SearchValue => Name ?? string.Empty;
    }
}