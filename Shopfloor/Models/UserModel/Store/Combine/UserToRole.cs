using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Interfaces;
using Shopfloor.Models.RoleModel;
using Shopfloor.Models.RoleUserModel;

namespace Shopfloor.Models.UserModel.Store.Combine
{
    internal sealed class UserToRole : ICombiner<User>
    {
        private readonly IDataStore<Role> _roleStore;
        private readonly IDataStore<RoleUser> _roleUserStore;
        private readonly IDataStore<User> _userStore;
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
                Role? role = roles.Find(r => r.Id == roleUser.RoleId);
                if (role == null)
                {
                    continue;
                }

                item.AddRole(role);
            }
        }
        private List<Role> GetRoles() => _roleStore.Data;
        private List<RoleUser> GetRoleUsers() => _roleUserStore.Data;
        private List<User> GetUsers() => _userStore.Data;
    }
}