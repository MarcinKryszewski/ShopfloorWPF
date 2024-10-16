using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Shopfloor.Features.Plannist.PlannistDashboard.Commands;
using Shopfloor.Interfaces;
using Shopfloor.Models.PartModel;
using Shopfloor.Shared.ViewModels;
using Shopfloor.Utilities.CustomList;
using Shopfloor.Utilities.CustomList.CustomListCommands;

namespace Shopfloor.Features.Mechanic.PartsStock
{
    internal sealed class PartsStockListViewModel : ViewModelBase
    {
        private readonly List<Part> _dataSource = [];
        public PartsStockListViewModel(IProvider<Part> partProvider)
        {
            DisplayList = new(_dataSource);
            LoadExcel = new LoadExcelDataCommand(this);
            NextPage = new NextPageCommand(DisplayList);
            PrevPage = new PreviousPageCommand(DisplayList);
            UpdateValues = new UpdateDataCommand(this, partProvider);
        }
        public List<Part> DataSource => _dataSource;
        public SearchableModelList DisplayList { get; }
        public ICommand LoadExcel { get; }
        public ICommand NextPage { get; }
        public ICommand PrevPage { get; }
        public ICommand UpdateValues { get; }
        public async Task LoadData(List<Part> parts)
        {
            _dataSource.Clear();
            foreach (Part part in parts)
            {
                _dataSource.Add(part);
            }
            await DisplayList.ReloadSourceData();
        }
    }
}