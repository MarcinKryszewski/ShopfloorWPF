using Shopfloor.Utilities;
using System.Windows.Controls;
using System.Windows.Data;

namespace Shopfloor.Features.Plannist
{
    /// <summary>
    /// Interaction logic for OrdersView.xaml
    /// </summary>
    public partial class PartsOrdersView : UserControl
    {
        public PartsOrdersView()
        {
            InitializeComponent();
        }
        public void DataGrid_TargetUpdated(object sender, DataTransferEventArgs e) => DataGridWidthRefresh.RefreshWidth(sender, e, 7);
    }
}