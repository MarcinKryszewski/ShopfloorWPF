using System;
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
            if (parameter is not WorkOrderCreationModel)
            {
                string errorInfo = "Nie udało się stworzyć zadania";
                _notifier.ShowError(errorInfo);
                return;
            }

            // TODO: Validation

            WorkOrderCreationModel data = (WorkOrderCreationModel)parameter;
            _ = CreateWorkOrder(data);
        }
        private async Task CreateWorkOrder(WorkOrderCreationModel data)
        {
            string testNotificationText = $@"
            description: {data.Description}
            lineId: {data.LineId}";

            try
            {
                await _unitOfWork.CreateWorkOrder(data);
                _notifier.ShowInformation(testNotificationText);
            }
            catch (Exception e)
            {
                _notifier.ShowError(e.Message);
            }
        }
    }
}