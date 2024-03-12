using System.Windows.Controls;
using System.Windows.Data;
using Shopfloor.Utilities;

namespace Shopfloor.Features.Manager.OrdersToApprove
{
    public sealed partial class OrdersToApproveView : UserControl
    {
        public OrdersToApproveView()
        {
            this.InitializeComponent();
        }
        public void DataGrid_TargetUpdated(object sender, DataTransferEventArgs e) => DataGridWidthRefresh.RefreshWidth(sender, e, 7);
    }
}