using Shopfloor.Features.Mechanic.Requests.Stores;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartModel.Store;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.Commands;
using Shopfloor.Shared.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Shopfloor.Features.Mechanic.Requests
{
    internal sealed class RequestsDetailsViewModel : ViewModelBase
    {
        private readonly SelectedRequestStore _selectedRequest;
        private readonly ErrandPartStore _errandPartStore;

        public ICommand ReturnCommand { get; }
        public ErrandPart ErrandPart => _selectedRequest.Request!;
        public IEnumerable<ErrandPart> HistoricalData { get; private set; } = [];
        public RequestsDetailsViewModel(SelectedRequestStore selectedRequestStore, ErrandPartStore errandPartStore, NavigationService navigationService)
        {
            Task.Run(() => LoadData());
            _selectedRequest = selectedRequestStore;
            _errandPartStore = errandPartStore;
            ReturnCommand = new RelayCommand(o => { navigationService.NavigateTo<RequestsListViewModel>(); }, o => true);
        }
        private Task LoadData()
        {
            ErrandPartStore errandPartStore = _errandPartStore;

            LoadHistoricalData(errandPartStore);
            return Task.CompletedTask;
        }
        private void LoadHistoricalData(ErrandPartStore errandParts)
        {
            HistoricalData = errandParts.Data.Where(part => part.PartId == _selectedRequest.Request!.PartId);
        }
    }
}