using System;
using Shopfloor.Features.Plannist.PlannistDashboard.Stores;
using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.Plannist.Commands
{
    internal sealed class PlannistAbortCommand : CommandBase
    {
        private readonly ErrandPartStatusProvider _errandPartStatusProvider;
        private readonly SelectedRequestStore _selectedRequest;
        public PlannistAbortCommand(SelectedRequestStore selectedRequest, IProvider<ErrandPartStatus> errandPartStatusProvider)
        {
            _selectedRequest = selectedRequest;
            _errandPartStatusProvider = (ErrandPartStatusProvider)errandPartStatusProvider; //temporary fix
        }
        public event Action? RequestAborted;
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

            _errandPartStatusProvider.Abort((int)requestId).Wait();
            _selectedRequest.Request.LastStatus.Abort();
            RequestAborted?.Invoke();
        }
    }
}