using Shopfloor.Models.UserModel;
using Shopfloor.Shared.Commands;
using Shopfloor.Stores;
using System.Windows.Input;

namespace Shopfloor.Features.Login.Commands
{
    internal class LoginCommand : CommandBase
    {
        private readonly ICurrentUserStore _store;
        private readonly LoginViewModel _viewModel;
        private readonly ICommand _naviagateCommand;

        public LoginCommand(
            ICurrentUserStore store,
            LoginViewModel viewModel,
            ICommand naviagateCommand
            )
        {
            _store = store;
            _viewModel = viewModel;
            _naviagateCommand = naviagateCommand;
        }

        public override void Execute(object? parameter)
        {
            if (_viewModel.HasErrors) return;
            _store.Login(_viewModel.Username);
            if (_store.IsUserLoggedIn)
            {
                _naviagateCommand.Execute(this);
            };
        }
    }
}