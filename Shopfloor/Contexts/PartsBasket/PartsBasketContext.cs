using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Shopfloor.Models.WorkOrderParts;
using Shopfloor.Models.WorkOrders;

namespace Shopfloor.Contexts.PartsBasket
{
    internal class PartsBasketContext
    {
        private WorkOrderModel? _workOrder;
        public ObservableCollection<WorkOrderPartCreationModel> Parts { get; } = [];
        public List<WorkOrderPartCreationModel> OriginalPartsList { get; set; } = [];
        public List<WorkOrderPartCreationModel> PartsCanceled { get; } = [];
        public WorkOrderModel? WorkOrder
        {
            get => _workOrder;
            set
            {
                _workOrder = value;
                if (value != null)
                {
                    Parts.ToList().ForEach(x =>
                    {
                        x.WorkOrder = value;
                        x.WorkOrderId = value.Id;
                    });
                }
            }
        }
    }
}