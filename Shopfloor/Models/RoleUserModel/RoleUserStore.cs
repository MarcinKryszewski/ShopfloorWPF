using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Models.RoleUserModel
{
    internal sealed class RoleUserStore : StoreBase<RoleUser>
    {
        public RoleUserStore(RoleIUserProvider provider) : base(provider)
        {
        }
    }
}