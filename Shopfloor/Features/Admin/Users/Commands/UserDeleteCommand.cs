using System.ComponentModel;
using Shopfloor.Features.Admin.Users.List;
using Shopfloor.Models;
using Shopfloor.Services.Providers;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.Admin.UsersList.Commands
{
    public class UserDeleteCommand : CommandBase
    {
        private readonly UsersListViewModel _viewModel;
        private readonly UserProvider _userProvider;

        public UserDeleteCommand(UsersListViewModel viewModel, UserProvider userProvider)
        {
            _viewModel = viewModel;
            _userProvider = userProvider;
        }

        public override void Execute(object? parameter)
        {
            User? user = _viewModel.SelectedUser;

            if (user is null) return;
            if (user.Username == "@dm1n") return;

            _ = _userProvider.SetUserActive(user.Id, false);
            _viewModel.UpdateUsers();
        }
    }
}