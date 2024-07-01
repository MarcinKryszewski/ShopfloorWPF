using Shopfloor.Interfaces;
using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Models.SupplierModel
{
    internal sealed class SuppliersStore : StoreBase<Supplier>
    {
        public SuppliersStore(IProvider<Supplier> provider) : base(provider)
        {
        }
    }
}