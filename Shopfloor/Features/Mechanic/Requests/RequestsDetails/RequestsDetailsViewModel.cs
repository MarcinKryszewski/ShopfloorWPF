using Shopfloor.Features.Mechanic.Requests.Stores;
using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartModel.Store;
using Shopfloor.Services.NavigationServices;
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
        private readonly IDataStore<ErrandPart> _errandPartStore;

        public ICommand ReturnCommand { get; }
        public ErrandPart ErrandPart => _selectedRequest.Request!;
        public IEnumerable<ErrandPart> HistoricalData { get; private set; } = [];
        public RequestsDetailsViewModel(SelectedRequestStore selectedRequestStore, IDataStore<ErrandPart> errandPartStore, NavigationService navigationService)
        {
            Task.Run(() => LoadData());
            _selectedRequest = selectedRequestStore;
            _errandPartStore = errandPartStore;
            ReturnCommand = new NavigationCommand<RequestsListViewModel>(navigationService).Navigate();
        }
        private Task LoadData()
        {
            IDataStore<ErrandPart> errandPartStore = _errandPartStore;

            LoadHistoricalData(errandPartStore);
            return Task.CompletedTask;
        }
        private void LoadHistoricalData(IDataStore<ErrandPart> errandParts)
        {
            HistoricalData = errandParts.Data.Where(part => part.PartId == _selectedRequest.Request!.PartId);
        }
    }
}