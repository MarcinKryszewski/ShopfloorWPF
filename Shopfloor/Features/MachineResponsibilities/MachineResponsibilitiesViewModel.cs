using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Data;
using Shopfloor.Models.Lines;
using Shopfloor.Models.Machines;
using Shopfloor.Roots;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.MachineResponsibilities
{
    internal class MachineResponsibilitiesViewModel : ViewModelBase
    {
        private readonly MachinesRoot _machinesRoot;
        private readonly IEnumerable<Line> _lines;
        // private readonly DataRoot _data;
        public MachineResponsibilitiesViewModel(
            MachinesRoot machinesRoot,
            DataRoot data)
        {
            _machinesRoot = machinesRoot;
            // _data = data;

            // Machines.GroupDescriptions.Clear();
            Machines.GroupDescriptions.Add(new PropertyGroupDescription(nameof(Machine.LineId)));

            // Task.Run(_machinesRoot.GetData);
            Task.Run(_machinesRoot.GetData).Wait();
            _lines = data.GetLine().Result;
            Machines.Refresh();
        }
        public ICollectionView Machines => CollectionViewSource.GetDefaultView(_machinesRoot.Data);
        public ICollectionView Lines => CollectionViewSource.GetDefaultView(_lines);
    }
}