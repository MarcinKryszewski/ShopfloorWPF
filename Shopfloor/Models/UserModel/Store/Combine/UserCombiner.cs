using Shopfloor.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Models.UserModel.Store.Combine
{
    internal sealed class UserCombiner : ICombiner<User>
    {
        public Task Combine(List<User> data)
        {
            return Task.CompletedTask;
        }
    }
}