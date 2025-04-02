using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Shopfloor.Utilities;

namespace Shopfloor.Features.Trainings.ActionTraining
{
    /// <summary>
    /// Interaction logic for ActionTrainingView.xaml
    /// </summary>
    public partial class ActionTrainingView : UserControl
    {
        public ActionTrainingView()
        {
            InitializeComponent();
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