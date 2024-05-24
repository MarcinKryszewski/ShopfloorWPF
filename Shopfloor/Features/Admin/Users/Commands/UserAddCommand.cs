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
        private readonly IUserProvider _IUserProvider;
        private readonly IRoleIUserProvider _roleIUserProvider;

        public UserAddCommand(UsersAddViewModel viewModel, RolesStore rolesStore, IUserProvider IUserProvider, IRoleIUserProvider roleIUserProvider)
        {
            _viewModel = viewModel;
            _rolesStore = rolesStore;

            _IUserProvider = IUserProvider;
            _roleIUserProvider = roleIUserProvider;
        }

        public override void Execute(object? parameter)
        {
            User newUser = new()
            {
                Username = _viewModel.Username.ToLower(),
                Name = _viewModel.Name,
                Surname = _viewModel.Surname,
                Image = string.Empty,
                IsActive = true
            };

            if (!_viewModel.IsDataValidate) return;
            //TODO: To move to validation on _viewModel
            int newUserId = _IUserProvider.Create(newUser).Result;
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
                tasks.Add(Task.Run(() => _roleIUserProvider.Create((int)role.Id, userId)));
            }
            Task.WaitAll(tasks.ToArray());
        }
    }
}