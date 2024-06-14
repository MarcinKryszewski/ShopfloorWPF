using Shopfloor.Interfaces;
using Shopfloor.Models.UserModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandModel.Store.Combine
{
    internal sealed class ErrandToUser : ICombiner<Errand>
    {
        private readonly ErrandStore _errandStore;
        private readonly UserStore _userStore;
        public ErrandToUser(UserStore userStore, ErrandStore errandStore)
        {
            _userStore = userStore;
            _errandStore = errandStore;
        }
        public Task Combine()
        {
            List<Errand> errands = GetErrands();
            List<User> users = GetUsers();

            foreach (Errand errand in errands)
            {
                errand.CreatedByUser = users.FirstOrDefault(user => user.Id == errand.CreatedById);
                errand.Responsible = users.FirstOrDefault(user => user.Id == errand.OwnerId);
            }
            return Task.CompletedTask;
        }
        private List<Errand> GetErrands() => _errandStore.Data;
        private List<User> GetUsers() => _userStore.Data;
    }
}