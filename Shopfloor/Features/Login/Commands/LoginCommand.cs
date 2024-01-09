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

        public LoginCommand(UserProvider provider, UserStore store, LoginViewModel viewModel)
        {
            _provider = provider;
            _store = store;
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            _store.Login(_viewModel.Username, _provider);
        }
    }
}
