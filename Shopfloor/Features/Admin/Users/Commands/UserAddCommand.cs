using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Features.Admin.Users.Add;
using Shopfloor.Features.Admin.Users.Stores;
using Shopfloor.Services.Providers;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.Admin.UsersList.Commands
{
    public class UserAddCommand : CommandBase
    {
        private UsersAddViewModel _viewModel;
        private RolesStore _rolesStore;

        private UserProvider _userProvider;
        private RoleUserProvider _roleUserProvider;
        private RoleProvider _roleProvider;

        public UserAddCommand(UsersAddViewModel viewModel, RolesStore rolesStore, UserProvider userProvider, RoleUserProvider roleUserProvider, RoleProvider roleProvider)
        {
            _viewModel = viewModel;
            _rolesStore = rolesStore;

            _userProvider = userProvider;
            _roleUserProvider = roleUserProvider;
            _roleProvider = roleProvider;
        }

        public override void Execute(object? parameter)
        {
            //throw new NotImplementedException();
        }
    }
}