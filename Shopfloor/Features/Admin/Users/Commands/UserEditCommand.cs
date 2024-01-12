using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Features.Admin.Users.Edit;
using Shopfloor.Features.Admin.Users.Stores;
using Shopfloor.Models;
using Shopfloor.Services.Providers;
using Shopfloor.Shared.Commands;

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
            AddRoles();
            RemoveRoles();
        }

        private void EditUser()
        {

            _ = _userProvider.Update(new User(
                _userId,
                _viewModel.Username,
                _viewModel.Name,
                _viewModel.Surname,
                _imagePath,
                _isActive
            ));
        }
        private void AddRoles()
        {
            ICollection<Role> roles = _rolesStore.GetAllAssignedRoles();

            foreach (Role role in roles)
            {
                _ = _roleUserProvider.Create(role.Id, _userId);
            }
        }
        private void RemoveRoles()
        {
            ICollection<Role> roles = _rolesStore.GetAllRevokedRoles();

            foreach (Role role in roles)
            {
                _ = _roleUserProvider.Delete(role.Id, _userId);
            }
        }
    }
}