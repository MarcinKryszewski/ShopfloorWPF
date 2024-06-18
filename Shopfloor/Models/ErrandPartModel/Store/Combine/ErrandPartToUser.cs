using Shopfloor.Interfaces;
using Shopfloor.Models.UserModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandPartModel.Store.Combine
{
    internal sealed class ErrandPartToUser : ICombiner<ErrandPart>
    {
        private readonly ErrandPartStore _errandPartStore;
        private readonly UserStore _userStore;
        public ErrandPartToUser(UserStore userStore, ErrandPartStore errandPartStore)
        {
            _userStore = userStore;
            _errandPartStore = errandPartStore;
        }
        public Task CombineAll()
        {
            List<User> users = GetUsers();

            foreach (ErrandPart errandPart in _errandPartStore.Data)
            {
                errandPart.OrderedByUser = users.FirstOrDefault(user => user.Id == errandPart.OrderedById);
            }
            return Task.CompletedTask;
        }
        private List<ErrandPart> GetErrandParts() => _errandPartStore.Data;
        private List<User> GetUsers() => _userStore.Data;
    }
}