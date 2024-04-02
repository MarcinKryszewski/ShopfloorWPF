using Shopfloor.Features.Admin.Users;
using Shopfloor.Features.Admin.Users.Stores;
using Shopfloor.Interfaces;
using Shopfloor.Models.RoleModel;
using Shopfloor.Models.RoleUserModel;
using Shopfloor.Models.UserModel;
using Shopfloor.Shared.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Shopfloor.Features.Admin.UsersList.Commands
{
    internal sealed class UserAddCommand : CommandBase
    {
        private readonly UsersAddViewModel _viewModel;
        private readonly RolesStore _rolesStore;
        private readonly UserProvider _userProvider;
        private readonly IRoleUserProvider _roleUserProvider;

        public UserAddCommand(UsersAddViewModel viewModel, RolesStore rolesStore, UserProvider userProvider, IRoleUserProvider roleUserProvider)
        {
            _viewModel = viewModel;
            _rolesStore = rolesStore;

            _userProvider = userProvider;
            _roleUserProvider = roleUserProvider;
        }

        public override void Execute(object? parameter)
        {
            User newUser = new(_viewModel.Username.ToLower(), _viewModel.Name, _viewModel.Surname, string.Empty, true);
            if (!_viewModel.IsDataValidate) return;
            //TODO: To move to validation on _viewModel
            int newUserId = _userProvider.Create(newUser).Result;
            if (newUserId < 0)
            {
                //_viewModel.ErrorMassage = $"Użytkownik o loginie {_viewModel.Username} istnieje";
                return;
            }
            CreateRoleUserTasks(newUserId, _rolesStore.GetAllAssignedRoles());

            //_viewModel.ErrorMassage = "Utworzono nowego użytkownika!";
            _viewModel.CleanForm();
        }

        private void CreateRoleUserTasks(int userId, ObservableCollection<Role> roles)
        {
            List<Task<int>> tasks = [];

            foreach (Role role in roles)
            {
                if (role.Id == null) continue;
                tasks.Add(Task.Run(() => _roleUserProvider.Create((int)role.Id, userId)));
            }
            Task.WaitAll(tasks.ToArray());
        }
    }
}