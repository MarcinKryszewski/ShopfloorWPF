using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Features.Admin.Users;
using Shopfloor.Models.UserModel;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.Admin.UsersList.Commands
{
    internal sealed class UserSetActivityCommand : AsyncCommandBase
    {
        private readonly IUserProvider _userProvider;
        private readonly UsersListViewModel _viewModel;
        public UserSetActivityCommand(UsersListViewModel viewModel, IUserProvider userProvider)
        {
            _viewModel = viewModel;
            _userProvider = userProvider;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            //var watch = System.Diagnostics.Stopwatch.StartNew();

            User? user = _viewModel.SelectedUser;

            if (user is null)
            {
                return;
            }

            if (user.Username == "@dm1n")
            {
                return;
            }

            if (parameter is null)
            {
                return;
            }

            bool isActive = bool.Parse((string)parameter);
            int userId = user.Id == null ? 0 : (int)user.Id;

            User modifiedUser = _viewModel.Users.SourceCollection.OfType<User>().First(p => p.Id == userId);
            modifiedUser.SetActive(isActive);
            await _userProvider.SetUserActive(userId, isActive);
            _ = _viewModel.UpdateUsers();

            //watch.Stop();
            //Debug.WriteLine($"ActivitySet execution Time: {watch.ElapsedMilliseconds} ms");
        }
    }
}