using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Mechanic.Requests.Stores;
using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandModel.Store;
using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Models.ErrandTypeModel;
using Shopfloor.Models.PartModel;
using Shopfloor.Models.SupplierModel;
using Shopfloor.Models.UserModel;
using Shopfloor.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Shopfloor.Models.ErrandPartModel.Store;

namespace Shopfloor.Features.Mechanic.Requests.RequestsDetails
{
    internal sealed class RequestsDetailsViewModel : ViewModelBase
    {
        private readonly IServiceProvider _mainServices;
        private readonly SelectedRequestStore _selectedRequest;
        public ICommand ReturnCommand { get; }
        public ErrandPart ErrandPart => _selectedRequest.Request!;
        public IEnumerable<ErrandPart> HistoricalData { get; private set; } = [];
        public RequestsDetailsViewModel(IServiceProvider mainServices, IServiceProvider databaseServices)
        {
            _mainServices = mainServices;
            Task.Run(() => LoadData(databaseServices));
            _selectedRequest = _mainServices.GetRequiredService<SelectedRequestStore>();
            //ReturnCommand = new NavigateCommand<RequestsListViewModel>(_mainServices.GetRequiredService<NavigationService<RequestsListViewModel>>());
        }
        private async Task LoadData(IServiceProvider databaseServices)
        {
            SuppliersStore suppliers = databaseServices.GetRequiredService<SuppliersStore>();
            UserStore users = databaseServices.GetRequiredService<UserStore>();
            PartStore parts = databaseServices.GetRequiredService<PartStore>();
            ErrandPartStatusStore errandPartStatuses = databaseServices.GetRequiredService<ErrandPartStatusStore>();
            ErrandStore errands = databaseServices.GetRequiredService<ErrandStore>();
            ErrandTypeStore errandTypes = databaseServices.GetRequiredService<ErrandTypeStore>();
            ErrandPartStore errandPartStore = databaseServices.GetRequiredService<ErrandPartStore>();


            LoadHistoricalData(errandPartStore);
        }
        private void LoadHistoricalData(ErrandPartStore errandParts)
        {
            HistoricalData = errandParts.GetData(true).Where(part => part.PartId == _selectedRequest.Request!.PartId);
        }
    }
}