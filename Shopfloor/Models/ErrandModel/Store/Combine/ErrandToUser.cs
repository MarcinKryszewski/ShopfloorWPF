using Shopfloor.Interfaces;
using Shopfloor.Models.UserModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandModel.Store.Combine
{
    internal sealed class ErrandToUser : ICombiner
    {
        private readonly ErrandStore _errandStore;
        private readonly UserStore _userStore;
        public ErrandToUser(ErrandStore errandStore, UserStore userStore)
        {
            _errandStore = errandStore;
            _userStore = userStore;
        }
        public Task Combine()
        {
            List<User> users = GetUsers();
            List<Errand> errands = GetErrands();

            foreach (Errand errand in errands)
            {
                errand.CreatedByUser = users.FirstOrDefault(user => user.Id == errand.CreatedById);
                errand.Responsible = users.FirstOrDefault(user => user.Id == errand.OwnerId);
            }
            return Task.CompletedTask;
        }
        private List<User> GetUsers() => _userStore.GetData();
        private List<Errand> GetErrands() => _errandStore.GetData();
    }
}