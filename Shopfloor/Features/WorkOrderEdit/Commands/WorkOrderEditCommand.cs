using System;
using System.Threading.Tasks;
using Shopfloor.Models.WorkOrders;
using Shopfloor.Roots;
using Shopfloor.Services.NotificationServices;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.WorkOrderEdit.Commands
{
    internal class WorkOrderEditCommand : CommandBase
    {
        private readonly INotifier _notifier;
        private readonly WorkOrderEditRoot _root;
        public WorkOrderEditCommand(INotifier notifier, WorkOrderEditRoot root)
        {
            _notifier = notifier;
            _root = root;
        }
        public override void Execute(object? parameter)
        {
            if (parameter is not WorkOrderCreationModel)
            {
                string errorInfo = "Nie udało się edytować zadania";
                _notifier.ShowError(errorInfo);
                return;
            }

            WorkOrderCreationModel data = (WorkOrderCreationModel)parameter;
            _ = EditWorkOrder(data);
        }
        private async Task EditWorkOrder(WorkOrderCreationModel data)
        {
            string testNotificationText = $@"
            description: {data.Description}
            lineId: {data.LineId}";

            try
            {
                await _root.EditWorkOrder(data);
                _notifier.ShowInformation(testNotificationText);
            }
            catch (Exception e)
            {
                _notifier.ShowError(e.Message);
            }
        }
    }
}