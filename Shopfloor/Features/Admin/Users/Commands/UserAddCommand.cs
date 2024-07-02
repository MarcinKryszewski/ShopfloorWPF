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
        private readonly IProvider<User> _userProvider;
        private readonly IProvider<RoleUser> _roleIUserProvider;

        public UserAddCommand(UsersAddViewModel viewModel, RolesStore rolesStore, IProvider<User> userProvider, IProvider<RoleUser> roleIUserProvider)
        {
            _viewModel = viewModel;
            _rolesStore = rolesStore;

            _userProvider = userProvider;
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
                RoleUser roleUser = new()
                {
                    RoleId = (int)role.Id,
                    UserId = userId
                };
                tasks.Add(Task.Run(() => _roleIUserProvider.Create(roleUser)));
            }
            Task.WaitAll(tasks.ToArray());
        }
    }
}