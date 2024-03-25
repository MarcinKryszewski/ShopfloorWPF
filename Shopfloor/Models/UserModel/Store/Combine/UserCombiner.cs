using System.Threading.Tasks;
using Shopfloor.Interfaces;

namespace Shopfloor.Models.UserModel.Store.Combine
{
    public class UserCombiner : ICombiner
    {
        public Task Combine()
        {
            return Task.CompletedTask;
        }
    }
}