using Shopfloor.Features.Admin.Users.Stores;
using Shopfloor.Interfaces;
using Shopfloor.Models.RoleModel;
using Shopfloor.Models.RoleUserModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models.UserModel.Store.Combine
{
    internal sealed class UserCombiner : ICombinerManager<User>
    {
        private readonly UserToRole _role;
        public UserCombiner(UserToRole role)
        {
            _role = role;
        }
        public bool IsCombined { get; private set; }
        public async Task Combine(bool shouldForce = false)
        {
            if (IsCombined || !shouldForce) return;

            List<Task> tasks = [];

            tasks.Add(_role.Combine());

            await Task.WhenAll(tasks);
        }
    }

    internal sealed class UserToRole : ICombiner<User>
    {
        private readonly UserStore _userStore;
        private readonly RoleStore _roleStore;
        private readonly RoleUserStore _roleUserStore;
        public UserToRole(UserStore userStore, RoleStore roleStore, RoleUserStore roleUserStore)
        {
            _userStore = userStore;
            _roleStore = roleStore;
            _roleUserStore = roleUserStore;
        }
        public Task Combine()
        {
            List<Role> roles = GetRoles();
            List<User> users = GetUsers();
            List<RoleUser> roleUsers = GetRoleUsers();

            foreach (User user in users)
            {
                user.ClearRoles();
                foreach (RoleUser roleUser in roleUsers)
                {
                    Role? role = roles.FirstOrDefault(r => r.Id == roleUser.RoleId);
                    if (role == null) continue;
                    user.AddRole(role);
                }
            }

            return Task.CompletedTask;
        }
        List<User> GetUsers() => _userStore.Data;
        List<Role> GetRoles() => _roleStore.Data;
        List<RoleUser> GetRoleUsers() => _roleUserStore.Data;
    }
}