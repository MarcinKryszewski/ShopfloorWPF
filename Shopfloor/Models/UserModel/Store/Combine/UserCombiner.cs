using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Interfaces;

namespace Shopfloor.Models.UserModel.Store.Combine
{
    internal sealed class UserCombiner : ICombinerManager<User>
    {
        private readonly UserToRole _role;
        public UserCombiner(UserToRole role)
        {
            _role = role;
        }
        public bool IsCombined { get; private set; }
        public async Task CombineAll(bool shouldForce = false)
        {
            if (IsCombined && !shouldForce)
            {
                return;
            }

            List<Task> tasks = [];

            tasks.Add(_role.CombineAll());

            await Task.WhenAll(tasks);
            IsCombined = true;
        }
        public async Task CombineOne(User item)
        {
            List<Task> tasks = [];

            tasks.Add(_role.CombineAll());

            await Task.WhenAll(tasks);
        }
    }
}