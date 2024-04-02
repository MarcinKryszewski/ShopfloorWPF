using Shopfloor.Features.Plannist.PlannistDashboard.Stores;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Shared.Commands;
using System;
using ToastNotifications;
using ToastNotifications.Messages;

namespace Shopfloor.Features.Plannist.Commands
{
    internal sealed class PlannistConfirmCommand : CommandBase
    {
        private readonly SelectedRequestStore _selectedRequest;
        private readonly Notifier _notifier;
        private readonly ErrandPartStatusProvider _errandPartStatusProvider;

        public PlannistConfirmCommand(SelectedRequestStore selectedRequest, Notifier notifier, ErrandPartStatusProvider errandPartStatusProvider)
        {
            _selectedRequest = selectedRequest;
            _notifier = notifier;
            _errandPartStatusProvider = errandPartStatusProvider;
        }
        public override void Execute(object? parameter)
        {
            if (_selectedRequest.Request is null) return;
            int? requestId = _selectedRequest.Request.LastStatus.Id;
            if (requestId is null) return;

            _errandPartStatusProvider.Confirm((int)requestId).Wait();
            _selectedRequest.Request.LastStatus.Confirm();
            RequestConfirmed?.Invoke();
            _notifier.ShowInformation("Przekazano do realizacji");
        }
        public event Action? RequestConfirmed;
    }
}