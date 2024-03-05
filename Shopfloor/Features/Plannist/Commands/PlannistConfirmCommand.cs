using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Plannist.PlannistDashboard.Stores;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.Plannist.Commands
{
    internal sealed class PlannistConfirmCommand : CommandBase
    {
        private readonly SelectedRequestStore _selectedRequest;
        private readonly IServiceProvider _databaseServices;
        public PlannistConfirmCommand(SelectedRequestStore selectedRequest, IServiceProvider databaseServices)
        {
            _selectedRequest = selectedRequest;
            _databaseServices = databaseServices;
        }
        public override void Execute(object? parameter)
        {
            if (_selectedRequest.Request is null) return;
            int? requestId = _selectedRequest.Request.LastStatus.Id;
            if (requestId is null) return;

            _databaseServices.GetRequiredService<ErrandPartStatusProvider>().Confirm((int)requestId).Wait();
            _selectedRequest.Request.LastStatus.Confirm();
            RequestConfirmed?.Invoke();
        }
        public event Action? RequestConfirmed;
    }
}