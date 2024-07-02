using Shopfloor.Features.Admin.Users;
using Shopfloor.Features.Admin.Users.Stores;
using Shopfloor.Interfaces;
using Shopfloor.Models.RoleModel;
using Shopfloor.Models.RoleUserModel;
using Shopfloor.Models.UserModel;
using Shopfloor.Shared.Commands;
using System.Collections.Generic;

namespace Shopfloor.Features.Admin.UsersList.Commands
{
    internal sealed class UserEditCommand : CommandBase
    {
        private readonly IProvider<User> _userProvider;
        private readonly IProvider<RoleUser> _roleIUserProvider;
        private readonly RolesStore _rolesStore;
        private readonly UsersEditViewModel _viewModel;
        private readonly int _userId;
        private readonly string _imagePath;
        private readonly bool _isActive;

        public UserEditCommand(
            UsersEditViewModel viewModel,
            IProvider<User> userProvider,
            IProvider<RoleUser> roleIUserProvider,
            RolesStore rolesStore,
            int userId,
            string imagePath,
            bool isActive)
        {
            _viewModel = viewModel;
            _userProvider = userProvider;
            _roleIUserProvider = roleIUserProvider;
            _rolesStore = rolesStore;
            _userId = userId;
            _imagePath = imagePath;
            _isActive = isActive;
        }

        public override void Execute(object? parameter)
        {
            EditUser();
        }

        private void EditUser()
        {
            User user = new()
            {
                Id = _userId,
                Username = _viewModel.Username.ToLower(),
                Name = _viewModel.Name,
                Surname = _viewModel.Surname,
                Image = _imagePath,
                IsActive = _isActive
            };
            if (!_viewModel.IsDataValidate) return;
            _ = _userProvider.Update(user);
            _viewModel.CleanForm();
            AddRoles();
            RemoveRoles();
        }

        private void AddRoles()
        {
            ICollection<Role> roles = _rolesStore.GetAllAssignedRoles();

            foreach (Role role in roles)
            {
                if (role.Id is null) continue;
                RoleUser roleUser = new()
                {
                    RoleId = (int)role.Id,
                    UserId = _userId
                };
                _ = _roleIUserProvider.Create(roleUser);
            }
        }

        private void RemoveRoles()
        {
            ICollection<Role> roles = _rolesStore.GetAllRevokedRoles();

            foreach (Role role in roles)
            {
                if (role.Id is null) continue;
                RoleUser roleUser = new()
                {
                    RoleId = (int)role.Id,
                    UserId = _userId
                };
                IRoleUserProvider roleUserProvider = (IRoleUserProvider)_roleIUserProvider; //fix later please
                _ = roleUserProvider.Delete((int)role.Id, _userId);
            }
        }
    }
}