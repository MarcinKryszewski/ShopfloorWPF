using Shopfloor.Shared.Commands;
using Shopfloor.Stores;
using System.Windows.Input;

namespace Shopfloor.Layout.TopPanel.Commands
{
    internal class LogoutCommand : CommandBase
    {
        private readonly ICurrentUserStore _userStore;
        private readonly ICommand _returnCommand;
        public LogoutCommand(ICurrentUserStore userStore, ICommand returnCommand)
        {
            _userStore = userStore;
            _returnCommand = returnCommand;
        }
        public override void Execute(object? parameter)
        {
            _userStore.Logout();
            _returnCommand.Execute(true);
        }
    }
}