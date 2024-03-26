namespace Shopfloor.Models.UserModel
{
    internal sealed class UserStore : StoreBase<User>
    {
        public UserStore(UserProvider provider) : base(provider)
        {
        }
    }
}