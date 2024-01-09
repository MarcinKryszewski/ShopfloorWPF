using Shopfloor.Shared.Commands;
using Shopfloor.Stores;
using System;

namespace Shopfloor.Layout.TopPanel.Commands
{
    internal class LogoutCommand : CommandBase
    {
        private readonly UserStore _userStore;

        public LogoutCommand(UserStore userStore)
        {
            _userStore = userStore;
        }
        public override void Execute(object? parameter)
        {
            _userStore.Logout();
            _userStore.ResetError();
        }
    }
}
