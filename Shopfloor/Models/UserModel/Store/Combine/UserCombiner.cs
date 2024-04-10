using Shopfloor.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task Combine(bool shouldForce = false)
        {
            if (IsCombined || !shouldForce) return;

            List<Task> tasks = [];

            tasks.Add(_role.Combine());

            await Task.WhenAll(tasks);
            IsCombined = true;
        }
    }
}