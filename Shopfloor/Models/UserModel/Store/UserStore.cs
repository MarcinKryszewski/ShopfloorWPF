using Shopfloor.Interfaces;
using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Models.UserModel
{
    internal sealed class UserStore : StoreBase<User>
    {
        public UserStore(IProvider<User> provider) : base(provider)
        {
        }
    }
}