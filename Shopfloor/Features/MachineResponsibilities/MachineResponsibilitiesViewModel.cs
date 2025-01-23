using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Data;
using Shopfloor.Roots;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.MachineResponsibilities
{
    internal class MachineResponsibilitiesViewModel : ViewModelBase
    {
        private readonly MachinesRoot _machinesRoot;

        public MachineResponsibilitiesViewModel(MachinesRoot machinesRoot)
        {
            _machinesRoot = machinesRoot;
            Task.Run(_machinesRoot.GetData);
        }
        public ICollectionView Machines => CollectionViewSource.GetDefaultView(_machinesRoot.Data);
    }
}