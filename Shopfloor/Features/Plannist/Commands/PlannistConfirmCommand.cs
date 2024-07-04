using System;
using Shopfloor.Features.Plannist.PlannistDashboard.Stores;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Services.NotificationServices;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.Plannist.Commands
{
    internal sealed class PlannistConfirmCommand : CommandBase
    {
        private readonly ErrandPartStatusProvider _errandPartStatusProvider;
        private readonly INotifier _notifier;
        private readonly SelectedRequestStore _selectedRequest;
        public PlannistConfirmCommand(SelectedRequestStore selectedRequest, INotifier notifier, ErrandPartStatusProvider errandPartStatusProvider)
        {
            _selectedRequest = selectedRequest;
            _notifier = notifier;
            _errandPartStatusProvider = errandPartStatusProvider;
        }
        public event Action? RequestConfirmed;
        public override void Execute(object? parameter)
        {
            if (_selectedRequest.Request is null)
            {
                return;
            }

            int? requestId = _selectedRequest.Request.LastStatus.Id;
            if (requestId is null)
            {
                return;
            }

            _errandPartStatusProvider.Confirm((int)requestId).Wait();
            _selectedRequest.Request.LastStatus.Confirm();
            RequestConfirmed?.Invoke();
            _notifier.ShowInformation("Przekazano do realizacji");
        }
    }
}