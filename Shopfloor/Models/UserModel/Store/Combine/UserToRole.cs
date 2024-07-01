using Shopfloor.Interfaces;
using Shopfloor.Models.RoleModel;
using Shopfloor.Models.RoleUserModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models.UserModel.Store.Combine
{
    internal sealed class UserToRole : ICombiner<User>
    {
        private readonly IDataStore<User> _userStore;
        private readonly IDataStore<Role> _roleStore;
        private readonly IDataStore<RoleUser> _roleUserStore;
        public UserToRole(IDataStore<User> userStore, IDataStore<Role> roleStore, IDataStore<RoleUser> roleUserStore)
        {
            _userStore = userStore;
            _roleStore = roleStore;
            _roleUserStore = roleUserStore;
        }
        public Task CombineAll()
        {
            List<Role> roles = GetRoles();
            List<User> users = GetUsers();
            List<RoleUser> roleUsers = GetRoleUsers();

            foreach (User item in users)
            {
                Combine(roles, roleUsers, item);
            }

            return Task.CompletedTask;
        }
        public Task CombineOne(User item)
        {
            List<Role> roles = GetRoles();
            List<RoleUser> roleUsers = GetRoleUsers();

            Combine(roles, roleUsers, item);

            return Task.CompletedTask;
        }
        private static void Combine(List<Role> roles, List<RoleUser> roleUsers, User item)
        {
            item.ClearRoles();
            foreach (RoleUser roleUser in roleUsers)
            {
                Role? role = roles.FirstOrDefault(r => r.Id == roleUser.RoleId);
                if (role == null) continue;
                item.AddRole(role);
            }
        }
        List<User> GetUsers() => _userStore.Data;
        List<Role> GetRoles() => _roleStore.Data;
        List<RoleUser> GetRoleUsers() => _roleUserStore.Data;
    }
}