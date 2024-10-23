using System.Windows.Controls;
using System.Windows.Data;
using Shopfloor.Utilities;

namespace Shopfloor.Features.Mechanic.WorkOrderAddNew
{
    public sealed partial class WorkOrderAddNewView : UserControl
    {
        public WorkOrderAddNewView()
        {
            this.InitializeComponent();
        }
        public void DataGrid_TargetUpdated(object sender, DataTransferEventArgs e) => DataGridWidthRefresh.RefreshWidth(sender, e, 1);
    }
}