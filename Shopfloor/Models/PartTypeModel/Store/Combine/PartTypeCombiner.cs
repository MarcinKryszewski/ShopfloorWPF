using System.Threading.Tasks;
using Shopfloor.Interfaces;

namespace Shopfloor.Models.PartTypeModel.Store.Combine
{
    public class PartTypeCombiner : ICombiner
    {
        public Task Combine()
        {
            return Task.CompletedTask;
        }
    }
}