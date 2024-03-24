using Shopfloor.Interfaces;
using Shopfloor.Models.UserModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandPartModel.Store.Combine
{
    internal sealed class ErrandPartToUser : ICombiner
    {
        private readonly ErrandPartStore _errandPartStore;
        private readonly UserStore _userStore;
        public ErrandPartToUser(ErrandPartStore errandPartStore, UserStore userStore)
        {
            _errandPartStore = errandPartStore;
            _userStore = userStore;
        }
        public Task Combine()
        {
            List<User> users = GetUsers();
            List<ErrandPart> errandsPart = GetErrandParts();

            foreach (ErrandPart errandPart in errandsPart)
            {
                errandPart.OrderedByUser = users.FirstOrDefault(user => user.Id == errandPart.OrderedById);
            }
            return Task.CompletedTask;
        }
        private List<User> GetUsers() => _userStore.GetData();
        private List<ErrandPart> GetErrandParts() => _errandPartStore.GetData();
    }
}