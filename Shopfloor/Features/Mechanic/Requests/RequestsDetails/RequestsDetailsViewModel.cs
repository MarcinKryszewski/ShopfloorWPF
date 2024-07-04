using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Shopfloor.Features.Mechanic.Requests.Stores;
using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.Mechanic.Requests
{
    internal sealed class RequestsDetailsViewModel : ViewModelBase
    {
        private readonly IDataStore<ErrandPart> _errandPartStore;
        private readonly SelectedRequestStore _selectedRequest;
        public RequestsDetailsViewModel(SelectedRequestStore selectedRequestStore, IDataStore<ErrandPart> errandPartStore, NavigationService navigationService)
        {
            Task.Run(() => LoadData());
            _selectedRequest = selectedRequestStore;
            _errandPartStore = errandPartStore;
            ReturnCommand = new NavigationCommand<RequestsListViewModel>(navigationService).Navigate();
        }
        public ErrandPart ErrandPart => _selectedRequest.Request!;
        public IEnumerable<ErrandPart> HistoricalData { get; private set; } = [];
        public ICommand ReturnCommand { get; }
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