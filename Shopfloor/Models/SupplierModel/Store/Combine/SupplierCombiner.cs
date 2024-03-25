using System.Threading.Tasks;
using Shopfloor.Interfaces;

namespace Shopfloor.Models.SupplierModel.Store.Combine
{
    public class SupplierCombiner : ICombiner
    {
        public Task Combine()
        {
            return Task.CompletedTask;
        }
    }
}