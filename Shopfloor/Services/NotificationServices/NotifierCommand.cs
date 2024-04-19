using Shopfloor.Shared.Commands;
using static Shopfloor.Services.NotificationServices.NotifierSetup;

namespace Shopfloor.Services.NotificationServices
{
    internal sealed class NotifierCommand : CommandBase
    {
        private readonly INotifier _notifier;
        private readonly ToastMessageStyles _style;
        private readonly string _message;

        public NotifierCommand(INotifier notifier, string message, ToastMessageStyles style)
        {
            _notifier = notifier;
            _message = message;
            _style = style;
        }

        public override void Execute(object? parameter)
        {
            switch (_style)
            {
                case ToastMessageStyles.Information:
                    _notifier.ShowInformation(_message);
                    break;
                case ToastMessageStyles.Warning:
                    _notifier.ShowWarning(_message);
                    break;
                case ToastMessageStyles.Error:
                    _notifier.ShowError(_message);
                    break;
                case ToastMessageStyles.Success:
                    _notifier.ShowSuccess(_message);
                    break;
                default:
                    break;
            }
        }
    }
}