using Shopfloor.Interfaces;
using System;

namespace Shopfloor.Models.PartTypeModel
{
    internal sealed partial class PartType
    {
        private readonly PartTypeDTO _data = new();
        public PartType(int id, string name)
        {
            _data.Id = id;
            _data.Part_Type_Name = name;
        }
        public PartType(string name)
        {
            _data.Part_Type_Name = name;
        }
        public int? Id => _data.Id;
        public string Name => _data.Part_Type_Name;
    }
    internal sealed partial class PartType : IEquatable<PartType>
    {
        public bool Equals(PartType? other)
        {
            if (other == null) return false;
            if (_data.Id == null && other._data.Id == null) return _data.Part_Type_Name == other.Name;
            return _data.Id == other.Id;
        }
        public override bool Equals(object? obj) => obj is PartType objPartType && Equals(objPartType);
        public override int GetHashCode()
        {
            if (_data.Id != null) return _data.Id.GetHashCode();
            return _data.Part_Type_Name.GetHashCode();
        }
    }
    internal sealed partial class PartType : ISearchableModel
    {
        public string SearchValue => _data.Part_Type_Name ?? string.Empty;
    }
}