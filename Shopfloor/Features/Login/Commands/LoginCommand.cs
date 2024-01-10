using Shopfloor.Models;
using Shopfloor.Services.Providers;
using Shopfloor.Shared.Commands;
using Shopfloor.Stores;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Shopfloor.Features.Login.Commands
{
    internal class LoginCommand : CommandBase
    {
        private readonly UserProvider _userProvider;
        private readonly RoleProvider _roleProvider;
        private readonly RoleUserProvider _roleUserProvider;
        private readonly UserStore _store;
        private readonly LoginViewModel _viewModel;
        private readonly ICommand _naviagateCommand;

        public LoginCommand(
            UserProvider userProvider,
            RoleProvider roleProvider,
            RoleUserProvider roleUserProvider,
            UserStore store,
            LoginViewModel viewModel,
            ICommand naviagateCommand
            )
        {
            _userProvider = userProvider;
            _roleProvider = roleProvider;
            _roleUserProvider = roleUserProvider;
            _store = store;
            _viewModel = viewModel;
            _naviagateCommand = naviagateCommand;
        }

        public override void Execute(object? parameter)
        {
            _store.Login(_viewModel.Username, _userProvider);

            if (_store.IsUserLoggedIn)
            {
                _ = SetUserRoles(_store.User);
                _naviagateCommand.Execute(this);
            };
        }

        private async Task SetUserRoles(User user)
        {
            IEnumerable<Role> roles = GetRoles();
            IEnumerable<RoleUser> roleUsers = GetRoleUsers();

            var roleTasks = roleUsers.Select(async roleUser =>
            {
                Role role = roles.First(r => r.Id == roleUser.RoleId);
                await Task.Run(() => user.AddRole(role));
            });

            await Task.WhenAll(roleTasks);
        }

        private IEnumerable<RoleUser> GetRoleUsers()
        {
            return _roleUserProvider.GetAllForUser(_store.User.Id).Result;
        }
        private IEnumerable<Role> GetRoles()
        {
            return _roleProvider.GetAll().Result;
        }
    }
}
