using System.Windows.Input;
using Shopfloor.Services.Providers;
using Shopfloor.Shared.Commands;
using Shopfloor.Stores;

namespace Shopfloor.Features.Login.Commands
{
    internal class LoginCommand : CommandBase
    {
        private readonly UserProvider _provider;
        private readonly UserStore _store;
        private readonly LoginViewModel _viewModel;
        private readonly ICommand _naviagateCommand;

        public LoginCommand(UserProvider provider, UserStore store, LoginViewModel viewModel, ICommand naviagateCommand)
        {
            _provider = provider;
            _store = store;
            _viewModel = viewModel;
            _naviagateCommand = naviagateCommand;
        }

        public override void Execute(object? parameter)
        {
            _store.Login(_viewModel.Username, _provider);
            if (_store.IsUserLoggedIn) _naviagateCommand.Execute(this);
        }
    }
}
