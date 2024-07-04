using System.Windows.Controls;
using System.Windows.Data;
using Shopfloor.Utilities;

namespace Shopfloor.Features.Plannist
{
    public sealed partial class PlannistPartsListView : UserControl
    {
        public PlannistPartsListView()
        {
            this.InitializeComponent();
        }
        public void DataGrid_TargetUpdated(object sender, DataTransferEventArgs e) => DataGridWidthRefresh.RefreshWidth(sender, e, 7);
    }
}