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
        private readonly UserStore _store;
        private readonly LoginViewModel _viewModel;
        private readonly ICommand _naviagateCommand;

        public LoginCommand(
            UserProvider userProvider,
            UserStore store,
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
            //if (_viewModel is null) return;
            //if (_naviagateCommand is null) return;

            _store.Login(_viewModel.Username, _userProvider);
            if (_store.IsUserLoggedIn)
            {
                _naviagateCommand.Execute(this);
            };
        }
    }
}
