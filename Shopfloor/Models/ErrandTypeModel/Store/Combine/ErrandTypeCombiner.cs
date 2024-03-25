using System.Threading.Tasks;
using Shopfloor.Interfaces;

namespace Shopfloor.Models.ErrandTypeModel.Store.Combine
{
    public class ErrandTypeCombiner : ICombiner
    {
        public Task Combine()
        {
            //throw new NotImplementedException();
            return Task.CompletedTask;
        }
    }
}