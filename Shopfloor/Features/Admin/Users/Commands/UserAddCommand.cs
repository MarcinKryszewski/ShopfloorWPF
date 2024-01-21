﻿using Shopfloor.Features.Admin.Users.Add;
using Shopfloor.Features.Admin.Users.Stores;
using Shopfloor.Models;
using Shopfloor.Services.Providers;
using Shopfloor.Shared.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Shopfloor.Features.Admin.UsersList.Commands
{
    public class UserAddCommand : CommandBase
    {
        private readonly UsersAddViewModel _viewModel;
        private readonly RolesStore _rolesStore;

        private readonly UserProvider _userProvider;
        private readonly RoleUserProvider _roleUserProvider;

        public UserAddCommand(UsersAddViewModel viewModel, RolesStore rolesStore, UserProvider userProvider, RoleUserProvider roleUserProvider)
        {
            _viewModel = viewModel;
            _rolesStore = rolesStore;

            _userProvider = userProvider;
            _roleUserProvider = roleUserProvider;
        }

        public override void Execute(object? parameter)
        {
            User newUser = new(_viewModel.Username.ToLower(), _viewModel.Name, _viewModel.Surname, "", true);
            if (!_viewModel.IsDataValidate()) return;
            //TODO: To move to validation on _viewModel
            int newUserId = newUser.Add(_userProvider).Result;
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
            List<Task<int>> tasks = new();

            foreach (Role role in roles)
            {
                tasks.Add(Task.Run(() => _roleUserProvider.Create(role.Id, userId)));
            }
            Task.WaitAll(tasks.ToArray());
        }
    }
}