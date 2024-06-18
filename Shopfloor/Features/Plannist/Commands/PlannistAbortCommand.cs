using System;
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Plannist.PlannistDashboard.Stores;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.Plannist.Commands
{
    internal sealed class PlannistAbortCommand : CommandBase
    {
        private readonly SelectedRequestStore _selectedRequest;
        private readonly IServiceProvider _databaseServices;
        public PlannistAbortCommand(SelectedRequestStore selectedRequest, IServiceProvider databaseServices)
        {
            _selectedRequest = selectedRequest;
            _databaseServices = databaseServices;
        }
        public override void Execute(object? parameter)
        {
            if (_selectedRequest.Request is null) return;
            int? requestId = _selectedRequest.Request.LastStatus.Id;
            if (requestId is null) return;

            _databaseServices.GetRequiredService<ErrandPartStatusProvider>().Abort((int)requestId).Wait();
            _selectedRequest.Request.LastStatus.Abort();
            RequestAborted?.Invoke();
        }
        public event Action? RequestAborted;
    }
}