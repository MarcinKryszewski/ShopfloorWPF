using Shopfloor.Interfaces;
using Shopfloor.Models.UserModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandPartModel.Store.Combine
{
    internal sealed class ErrandPartToUser : ICombiner<ErrandPart>
    {
        private readonly IDataStore<User> _userStore;
        public ErrandPartToUser(IDataStore<User> userStore)
        {
            _userStore = userStore;
        }
        public Task Combine(List<ErrandPart> data)
        {
            List<User> users = GetUsers();

            foreach (ErrandPart errandPart in data)
            {
                errandPart.OrderedByUser = users.FirstOrDefault(user => user.Id == errandPart.OrderedById);
            }
            return Task.CompletedTask;
        }
        private List<User> GetUsers() => _userStore.GetData();
    }
}