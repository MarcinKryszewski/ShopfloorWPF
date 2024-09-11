using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.WorkOrders;
using Shopfloor.Services.NotificationServices;
using Shopfloor.Shared.Commands;
using Shopfloor.UnitOfWorks;

namespace Shopfloor.Features.WorkOrderAddNew.Commands
{
    internal class WorkOrderCreateCommand : CommandBase
    {
        private readonly INotifier _notifier;
        private readonly WorkOrderCreateRoot _unitOfWork;

        public WorkOrderCreateCommand(INotifier notifier, WorkOrderCreateRoot unitOfWork)
        {
            _notifier = notifier;
            _unitOfWork = unitOfWork;
        }

        public override void Execute(object? parameter)
        {
            if (parameter is not WorkOrderDto)
            {
                return;
            }

            WorkOrderDto data = (WorkOrderDto)parameter;
            _ = CreateWorkOrder(data);
        }
        private async Task CreateWorkOrder(WorkOrderDto data)
        {
            string testNotificationText = $@"
            description: {data.Description}
            lineId: {data.LineId}";

            await _unitOfWork.CreateWorkOrder(data);
            _notifier.ShowInformation(testNotificationText);
        }
    }
}