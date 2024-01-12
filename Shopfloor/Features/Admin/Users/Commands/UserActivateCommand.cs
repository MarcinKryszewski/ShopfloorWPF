using Shopfloor.Features.Admin.Users.List;
using Shopfloor.Services.Providers;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.Admin.Users.Commands
{
    public class UserActivateCommand : CommandBase
    {
        private readonly UsersListViewModel _viewModel;
        private readonly UserProvider _userProvider;

        public UserActivateCommand(UsersListViewModel viewModel, UserProvider userProvider)
        {
            _viewModel = viewModel;
            _userProvider = userProvider;
        }

        public override void Execute(object? parameter)
        {
            if (_viewModel.SelectedUser is not null)
            {
                _ = _userProvider.SetUserActive(_viewModel.SelectedUser.Id, true);
                _viewModel.UpdateUsers();
            }
        }
    }
}