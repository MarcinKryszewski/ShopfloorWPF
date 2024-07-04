using System.Windows.Input;
using Shopfloor.Shared.Commands;
using Shopfloor.Stores;

namespace Shopfloor.Features.Login.Commands
{
    internal class LoginCommand : CommandBase
    {
        private readonly ICommand _naviagateCommand;
        private readonly ICurrentUserStore _store;
        private readonly LoginViewModel _viewModel;
        public LoginCommand(
            ICurrentUserStore store,
            LoginViewModel viewModel,
            ICommand naviagateCommand)
        {
            _store = store;
            _viewModel = viewModel;
            _naviagateCommand = naviagateCommand;
        }

        public override void Execute(object? parameter)
        {
            if (_viewModel.HasErrors)
            {
                return;
            }

            _store.Login(_viewModel.Username);
            if (_store.IsUserLoggedIn)
            {
                _naviagateCommand.Execute(this);
            }
        }
    }
}