using Shopfloor.Interfaces;
using Shopfloor.Shared.BaseClasses;
using System;

namespace Shopfloor.Models.SupplierModel
{
    internal sealed class Supplier : DataModel, IEquatable<Supplier>, ISearchableModel
    {
        private readonly SupplierDTO _data;
        public int? Id => _data.Id;
        public string Name => _data.Name;
        public bool IsActive => _data.Active;
        public string SearchValue => _data.Name;
        public Supplier(int id, string name, bool isActive)
        {
            _data = new()
            {
                Id = id,
                Name = name,
                Active = isActive
            };
        }
        public Supplier(string name, bool isActive)
        {
            _data = new()
            {
                Name = name,
                Active = isActive
            };
        }
        public bool Equals(Supplier? other)
        {
            if (other == null) return false;
            if (Id == null && other.Id == null) return Name == other.Name;
            return Id == other.Id;
        }
        public override bool Equals(object? obj) => obj is Supplier objSupplier && Equals(objSupplier);
        public override int GetHashCode()
        {
            if (Id != null) return Id.GetHashCode();
            return Name.GetHashCode();
        }
    }
}