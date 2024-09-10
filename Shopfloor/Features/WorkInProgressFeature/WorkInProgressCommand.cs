using System;
using Shopfloor.Services.NotificationServices;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.WorkInProgressFeature
{
    internal class WorkInProgressCommand : CommandBase
    {
        private readonly INotifier _notification;

        public WorkInProgressCommand(INotifier notification)
        {
            _notification = notification;
        }

        public override void Execute(object? parameter)
        {
            string workInProgressDescription = "Przycisk w trakcie tworzenia";
            _notification.ShowInformation(workInProgressDescription);
        }
    }
}