using Shopfloor.Services.NotificationServices;

namespace Shopfloor.Shared.Dummies
{
    internal class NotifierDummy : INotifier
    {
        public void Show(Notification notification)
        {
        }

        public void Show(string message, NotifierType type = NotifierType.Success)
        {
        }

        public void ShowError(string message)
        {
        }

        public void ShowInformation(string message)
        {
        }

        public void ShowSuccess(string message)
        {
        }

        public void ShowWarning(string message)
        {
        }
    }
}