using System.Windows.Controls;
using System.Windows.Data;
using Shopfloor.Utilities;

namespace Shopfloor.Features.Mechanic.WorkOrderDetails
{
    public sealed partial class WorkOrderDetailsView : UserControl
    {
        public WorkOrderDetailsView()
        {
            this.InitializeComponent();
        }
        public void DataGrid_TargetUpdated(object sender, DataTransferEventArgs e) => DataGridWidthRefresh.RefreshWidth(sender, e, 1);
    }
}