namespace Shopfloor.Models.UserModel
{
    internal sealed class UserStore : StoreBase<User>
    {
        public UserStore(IUserProvider provider) : base(provider)
        {
        }
    }
}