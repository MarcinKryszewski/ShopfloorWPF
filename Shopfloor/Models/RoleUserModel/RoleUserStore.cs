using Shopfloor.Interfaces;
using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Models.RoleUserModel
{
    internal sealed class RoleUserStore : StoreBase<RoleUser>
    {
        public RoleUserStore(IProvider<RoleUser> provider) : base(provider)
        {
        }
    }
}