using System.Threading.Tasks;
using Shopfloor.Interfaces;

namespace Shopfloor.Models.ErrandPartStatusModel.Store.Combine
{
    public class ErrandPartStatusCombiner : ICombiner
    {
        public Task Combine()
        {
            return Task.CompletedTask;
        }
    }
}