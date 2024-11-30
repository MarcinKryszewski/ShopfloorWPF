using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Shopfloor.Utilities;

namespace Shopfloor.Features.ActionsList
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
            var scrollViewer = OuterScrollViewer;

            if (scrollViewer != null)
            {
                scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - (e.Delta / 3.0));
                e.Handled = true;
            }
        }
    }
}