using System.Windows.Input;
using Shopfloor.Shared.Commands;
using Shopfloor.Stores;

namespace Shopfloor.Layout.TopPanel.Commands
{
    internal class LogoutCommand : CommandBase
    {
        private readonly ICommand _returnCommand;
        private readonly ICurrentUserStore _userStore;
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