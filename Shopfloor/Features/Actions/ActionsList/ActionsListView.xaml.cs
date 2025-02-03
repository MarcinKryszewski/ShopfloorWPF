using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Shopfloor.Utilities;

namespace Shopfloor.Features.Actions.ActionsList
{
    public sealed partial class ActionsListView : UserControl
    {
        public ActionsListView()
        {
            this.InitializeComponent();
        }
        public void DataGrid_TargetUpdated(object sender, DataTransferEventArgs e) => DataGridWidthRefresh.RefreshWidth(sender, e, 4);
        private void DataGrid_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (!e.Handled && sender is ScrollViewer scrollViewer)
            {
                scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - e.Delta);
                e.Handled = true;
            }
        }
    }
}