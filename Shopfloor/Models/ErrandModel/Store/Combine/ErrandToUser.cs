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
        public Task CombineOne(Errand item)
        {
            List<User> users = GetUsers();

            Combine(item, users);

            return Task.CompletedTask;
        }
        public Task CombineAll()
        {
            List<Errand> errands = GetErrands();
            List<User> users = GetUsers();

            foreach (Errand item in errands)
            {
                Combine(item, users);
            }
            return Task.CompletedTask;
        }
        private static void Combine(Errand errand, List<User> users)
        {
            errand.CreatedByUser = users.FirstOrDefault(user => user.Id == errand.CreatedById);
            errand.Responsible = users.FirstOrDefault(user => user.Id == errand.OwnerId);
        }
        private List<Errand> GetErrands() => _errandStore.Data;
        private List<User> GetUsers() => _userStore.Data;
    }
}