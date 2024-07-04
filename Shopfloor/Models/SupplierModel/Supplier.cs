using System;
using Shopfloor.Interfaces;
using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Models.SupplierModel
{
    internal sealed class Supplier : DataModel, IEquatable<Supplier>, ISearchableModel
    {
        private readonly SupplierDto _data;
        public Supplier(int id, string name, bool isActive)
        {
            _data = new()
            {
                Id = id,
                Name = name,
                Active = isActive,
            };
        }
        public Supplier(string name, bool isActive)
        {
            _data = new()
            {
                Name = name,
                Active = isActive,
            };
        }
        public int? Id => _data.Id;
        public bool IsActive => _data.Active;
        public string Name => _data.Name;
        public string SearchValue => _data.Name;
        public bool Equals(Supplier? other)
        {
            if (other == null)
            {
                return false;
            }

            if (Id == null && other.Id == null)
            {
                return Name == other.Name;
            }

            return Id == other.Id;
        }
        public override bool Equals(object? obj) => obj is Supplier objSupplier && Equals(objSupplier);
        public override int GetHashCode()
        {
            if (Id != null)
            {
                return Id.GetHashCode();
            }

            return Name.GetHashCode();
        }
    }
}