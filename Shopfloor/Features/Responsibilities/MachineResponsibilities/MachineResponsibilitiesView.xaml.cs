using System.Windows.Controls;
using System.Windows.Input;

namespace Shopfloor.Features.MachineResponsibilities
{
    public sealed partial class MachineResponsibilitiesView : UserControl
    {
        public MachineResponsibilitiesView()
        {
            this.InitializeComponent();
        }
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