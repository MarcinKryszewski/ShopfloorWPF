using System;
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Plannist.PlannistDashboard.Stores;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Shared.Commands;
using ToastNotifications;
using ToastNotifications.Messages;

namespace Shopfloor.Features.Plannist.Commands
{
    internal sealed class PlannistConfirmCommand : CommandBase
    {
        private readonly SelectedRequestStore _selectedRequest;
        private readonly IServiceProvider _databaseServices;
        private readonly Notifier _notifier;
        public PlannistConfirmCommand(SelectedRequestStore selectedRequest, IServiceProvider databaseServices, Notifier notifier)
        {
            _selectedRequest = selectedRequest;
            _databaseServices = databaseServices;
            _notifier = notifier;
        }
        public override void Execute(object? parameter)
        {
            if (_selectedRequest.Request is null) return;
            int? requestId = _selectedRequest.Request.LastStatus.Id;
            if (requestId is null) return;

            _databaseServices.GetRequiredService<ErrandPartStatusProvider>().Confirm((int)requestId).Wait();
            _selectedRequest.Request.LastStatus.Confirm();
            RequestConfirmed?.Invoke();
            _notifier.ShowInformation("Przekazano do realizacji");
        }
        public event Action? RequestConfirmed;
    }
}