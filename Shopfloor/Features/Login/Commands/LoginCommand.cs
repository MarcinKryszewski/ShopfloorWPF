
using Shopfloor.Models.UserModel;
using Shopfloor.Shared.Commands;
using Shopfloor.Stores;
using System.Windows.Input;

namespace Shopfloor.Features.Login.Commands
{
    internal class LoginCommand : CommandBase
    {
        private readonly UserProvider _userProvider;
        private readonly CurrentUserStore _store;
        private readonly LoginViewModel _viewModel;
        private readonly ICommand _naviagateCommand;

        public LoginCommand(
            UserProvider userProvider,
            CurrentUserStore store,
            LoginViewModel viewModel,
            ICommand naviagateCommand
            )
        {
            _userProvider = userProvider;
            _store = store;
            _viewModel = viewModel;
            _naviagateCommand = naviagateCommand;
        }

        public override void Execute(object? parameter)
        {
            _store.Login(_viewModel.Username, _userProvider, _viewModel);
            if (_store.IsUserLoggedIn)
            {
                _naviagateCommand.Execute(this);
            };
        }
    }
}