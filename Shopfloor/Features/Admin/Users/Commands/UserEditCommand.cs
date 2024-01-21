using Shopfloor.Features.Admin.Users.Edit;
using Shopfloor.Features.Admin.Users.Stores;
using Shopfloor.Models;
using Shopfloor.Services.Providers;
using Shopfloor.Shared.Commands;
using System.Collections.Generic;

namespace Shopfloor.Features.Admin.UsersList.Commands
{
    public class UserEditCommand : CommandBase
    {
        private readonly UserProvider _userProvider;
        private readonly RoleUserProvider _roleUserProvider;
        private readonly RolesStore _rolesStore;
        private readonly UsersEditViewModel _viewModel;
        private readonly int _userId;
        private readonly string _imagePath;
        private readonly bool _isActive;

        public UserEditCommand(
            UsersEditViewModel viewModel,
            UserProvider userProvider,
            RoleUserProvider roleUserProvider,
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

        private void EditUser()
        {
            User user = new(
                _userId,
                _viewModel.Username.ToLower(),
                _viewModel.Name,
                _viewModel.Surname,
                _imagePath,
                _isActive
            );
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
                _ = _roleUserProvider.Create((int)role.Id, _userId);
            }
        }

        private void RemoveRoles()
        {
            ICollection<Role> roles = _rolesStore.GetAllRevokedRoles();

            foreach (Role role in roles)
            {
                if (role.Id is null) continue;
                _ = _roleUserProvider.Delete((int)role.Id, _userId);
            }
        }
    }
}