using Shopfloor.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Models.SupplierModel.Store.Combine
{
    internal sealed class SupplierCombiner : ICombinerManager<Supplier>
    {
        public Task Combine()
        {
            return Task.CompletedTask;
        }
    }
}