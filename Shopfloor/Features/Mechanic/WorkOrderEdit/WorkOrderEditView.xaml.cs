using System.Windows.Controls;
using System.Windows.Data;
using Shopfloor.Utilities;

namespace Shopfloor.Features.Mechanic.WorkOrderEdit
{
    /// <summary>
    /// Interaction logic for WorkOrderEditView.xaml
    /// </summary>
    public partial class WorkOrderEditView : UserControl
    {
        public WorkOrderEditView()
        {
            InitializeComponent();
        }
        public void DataGrid_TargetUpdated(object sender, DataTransferEventArgs e) => DataGridWidthRefresh.RefreshWidth(sender, e, 1);
    }
}
