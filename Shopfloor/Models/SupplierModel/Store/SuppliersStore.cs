using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Models.SupplierModel
{
    internal sealed class SuppliersStore : StoreBase<Supplier>
    {
        public SuppliersStore(SupplierProvider provider) : base(provider)
        {
        }
    }
}