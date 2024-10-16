using System.Threading.Tasks;
using Shopfloor.Interfaces;

namespace Shopfloor.Models.SupplierModel.Store.Combine
{
    internal sealed class SupplierCombiner : ICombinerManager<Supplier>
    {
        public bool IsCombined { get; private set; }
        public Task CombineAll(bool shouldForce = false)
        {
            if (IsCombined && !shouldForce)
            {
                return Task.CompletedTask;
            }

            IsCombined = true;
            return Task.CompletedTask;
        }
        public Task CombineOne(Supplier item)
        {
            return Task.CompletedTask;
        }
    }
}