using System.Windows.Controls;
using System.Windows.Data;

namespace Shopfloor.Utilities
{
    public class DataGridWidthRefresh
    {
        public static void RefreshWidth(object sender, DataTransferEventArgs e, int starColumn)
        {
            if (sender is not DataGrid) return;
            DataGrid obj = (DataGrid)sender;
            obj.Columns[starColumn].Width = 0;
            obj.UpdateLayout();
            obj.Columns[starColumn].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
        }
    }
}