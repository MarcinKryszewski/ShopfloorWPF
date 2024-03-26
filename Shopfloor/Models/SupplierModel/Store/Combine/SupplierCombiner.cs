using Shopfloor.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Models.SupplierModel.Store.Combine
{
    internal sealed class SupplierCombiner : ICombiner<Supplier>
    {
        public Task Combine(List<Supplier> data)
        {
            return Task.CompletedTask;
        }
    }
}