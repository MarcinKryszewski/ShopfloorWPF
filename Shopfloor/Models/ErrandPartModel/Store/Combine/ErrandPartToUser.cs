using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Interfaces;
using Shopfloor.Models.UserModel;

namespace Shopfloor.Models.ErrandPartModel.Store.Combine
{
    internal sealed class ErrandPartToUser : ICombiner<ErrandPart>
    {
        private readonly IDataStore<ErrandPart> _errandPartStore;
        private readonly IDataStore<User> _userStore;
        public ErrandPartToUser(IDataStore<User> userStore, IDataStore<ErrandPart> errandPartStore)
        {
            _userStore = userStore;
            _errandPartStore = errandPartStore;
        }
        public Task CombineAll()
        {
            List<User> users = GetUsers();

            foreach (ErrandPart item in _errandPartStore.Data)
            {
                Combine(users, item);
            }
            return Task.CompletedTask;
        }

        public Task CombineOne(ErrandPart item)
        {
            List<User> users = GetUsers();

            Combine(users, item);

            return Task.CompletedTask;
        }
        private static void Combine(List<User> users, ErrandPart item)
        {
            item.OrderedByUser = users.Find(user => user.Id == item.OrderedById);
        }
        private List<ErrandPart> GetErrandParts() => _errandPartStore.Data;
        private List<User> GetUsers() => _userStore.Data;
    }
}