using Shopfloor.Interfaces;
using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Models.RoleModel
{
    internal sealed class RoleStore : StoreBase<Role>
    {
        public RoleStore(IProvider<Role> provider)
            : base(provider)
        {
        }
    }
}