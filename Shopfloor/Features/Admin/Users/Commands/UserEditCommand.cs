using System.Collections.Generic;
using Shopfloor.Features.Admin.Users;
using Shopfloor.Features.Admin.Users.Stores;
using Shopfloor.Interfaces;
using Shopfloor.Models.RoleModel;
using Shopfloor.Models.RoleUserModel;
using Shopfloor.Models.UserModel;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.Admin.UsersList.Commands
{
    internal sealed class UserEditCommand : CommandBase
    {
        private readonly string _imagePath;
        private readonly bool _isActive;
        private readonly IProvider<RoleUser> _roleUserProvider;
        private readonly RolesStore _rolesStore;
        private readonly int _userId;
        private readonly IProvider<User> _userProvider;
        private readonly UsersEditViewModel _viewModel;
        public UserEditCommand(
            UsersEditViewModel viewModel,
            IProvider<User> userProvider,
            IProvider<RoleUser> roleUserProvider,
            RolesStore rolesStore,
            int userId,
            string imagePath,
            bool isActive)
        {
            _viewModel = viewModel;
            _userProvider = userProvider;
            _roleUserProvider = roleUserProvider;
            _rolesStore = rolesStore;
            _userId = userId;
            _imagePath = imagePath;
            _isActive = isActive;
        }

        public override void Execute(object? parameter)
        {
            EditUser();
        }

        private void AddRoles()
        {
            ICollection<Role> roles = _rolesStore.GetAllAssignedRoles();

            foreach (Role role in roles)
            {
                if (role.Id is null)
                {
                    continue;
                }

                RoleUser roleUser = new()
                {
                    RoleId = (int)role.Id,
                    UserId = _userId,
                };
                _ = _roleUserProvider.Create(roleUser);
            }
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
                IsActive = _isActive,
            };
            if (!_viewModel.IsDataValidate)
            {
                return;
            }

            _ = _userProvider.Update(user);
            _viewModel.CleanForm();
            AddRoles();
            RemoveRoles();
        }
        private void RemoveRoles()
        {
            ICollection<Role> roles = _rolesStore.GetAllRevokedRoles();

            foreach (Role role in roles)
            {
                if (role.Id is null)
                {
                    continue;
                }

                RoleUser roleUser = new()
                {
                    RoleId = (int)role.Id,
                    UserId = _userId,
                };
                IRoleUserProvider roleUserProvider = (IRoleUserProvider)_roleUserProvider; //fix later please
                _ = roleUserProvider.Delete((int)role.Id, _userId);
            }
        }
    }
}