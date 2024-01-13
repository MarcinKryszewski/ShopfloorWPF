using Shopfloor.Features.Admin.Users.List;
using Shopfloor.Models;
using Shopfloor.Services.Providers;
using Shopfloor.Shared.Commands;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Features.Admin.UsersList.Commands
{
    public class UserSetActivityCommand : AsyncCommandBase
    {
        private readonly UsersListViewModel _viewModel;
        private readonly UserProvider _userProvider;

        public UserSetActivityCommand(UsersListViewModel viewModel, UserProvider userProvider)
        {
            _viewModel = viewModel;
            _userProvider = userProvider;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            User? user = _viewModel.SelectedUser;

            if (user is null) return;
            if (user.Username == "@dm1n") return;
            if (parameter is null) return;

            bool isActive = bool.Parse((string)parameter);
            int userId = user.Id;

            User modifiedUser = _viewModel.Users.SourceCollection.OfType<User>().First(p => p.Id == userId);
            modifiedUser.SetActive(isActive);
            await _userProvider.SetUserActive(userId, isActive);
            _ = _viewModel.UpdateUsers();
        }
    }
}