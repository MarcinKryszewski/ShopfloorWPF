using Shopfloor.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Models.UserModel.Store.Combine
{
    internal sealed class UserCombiner : ICombinerManager<User>
    {
        public Task Combine()
        {
            return Task.CompletedTask;
        }
    }
}