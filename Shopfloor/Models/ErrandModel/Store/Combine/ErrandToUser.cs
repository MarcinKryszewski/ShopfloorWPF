using Shopfloor.Interfaces;
using Shopfloor.Models.UserModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandModel.Store.Combine
{
    internal sealed class ErrandToUser : ICombiner<Errand>
    {
        private readonly UserStore _userStore;
        public ErrandToUser(UserStore userStore)
        {
            _userStore = userStore;
        }
        public Task Combine(List<Errand> data)
        {
            List<User> users = GetUsers();

            foreach (Errand errand in data)
            {
                errand.CreatedByUser = users.FirstOrDefault(user => user.Id == errand.CreatedById);
                errand.Responsible = users.FirstOrDefault(user => user.Id == errand.OwnerId);
            }
            return Task.CompletedTask;
        }
        private List<User> GetUsers() => _userStore.GetData();
    }
}