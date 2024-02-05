using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.ErrandStatusModel;
using Shopfloor.Models.ErrandTypeModel;
using Shopfloor.Models.MachineModel;
using Shopfloor.Models.UserModel;
using Shopfloor.Shared.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using Shopfloor.Features.Mechanic.Errands.ErrandsNew;
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Shared.Commands;
using Shopfloor.Shared.Services;

namespace Shopfloor.Features.Mechanic.Errands.ErrandsList
{
    internal sealed class ErrandsListViewModel : ViewModelBase
    {
        private readonly IServiceProvider _mainServices;
        private readonly IServiceProvider _databaseServices;
        private readonly ObservableCollection<Errand> _errands = [];
        public ICollectionView Errands => CollectionViewSource.GetDefaultView(_errands);
        public ICommand ErrandsAddNavigateCommand { get; }
        public ErrandsListViewModel(IServiceProvider mainServices, IServiceProvider databaseServices)
        {
            _mainServices = mainServices;
            _databaseServices = databaseServices;
            Task.Run(() => LoadData());
            ErrandsAddNavigateCommand = new NavigateCommand<ErrandsNewViewModel>(mainServices.GetRequiredService<NavigationService<ErrandsNewViewModel>>());
            //Task.Run(() => LoadData());
        }
        // TEST DATA
        /*private Task LoadDataTest()
        {
            Errand errand1 = new(1, DateTime.Now.AddDays(-5), 101, 201, 301, "Fix critical issueFix critical issueFix critical issueFix critical issueFix critical issueFix critical issueFix critical issueFix critical issueFix critical issue", "SAP123", DateTime.Now.AddDays(3), 401, "A");
            Errand errand2 = new(2, DateTime.Now.AddDays(-10), 102, 202, 302, "Update software", "SAP456", DateTime.Now.AddDays(5), 402);
            Errand errand3 = new(3, DateTime.Now.AddDays(-2), 103, 203, 303, "Inspect equipment", null, DateTime.Now.AddDays(2), 403, "C");
            Errand errand4 = new(4, DateTime.Now.AddDays(-7), 104, 204, 304, "Routine maintenance", "SAP789", DateTime.Now.AddDays(7), 404);
            Errand errand5 = new(5, DateTime.Now.AddDays(-3), 105, 205, 305, "Test new equipment", "SAP555", DateTime.Now.AddDays(4), 405, "B");

            _errands.Clear();
            _errands.Add(errand1);
            _errands.Add(errand2);
            _errands.Add(errand3);
            _errands.Add(errand4);
            _errands.Add(errand5);

            ErrandStatus errandStatus1 = new(1, "W trakcie");
            ErrandStatus errandStatus2 = new(1, "Zamówiony");
            ErrandStatus errandStatus3 = new(1, "Anulowany");

            ErrandStatus errandErrandStatus1 = new(1, 1, DateTime.Now.AddDays(-1));
            ErrandStatus errandErrandStatus2 = new(1, 2, DateTime.Now.AddDays(-0));
            ErrandStatus errandErrandStatus3 = new(1, 3, DateTime.Now.AddDays(1));

            Machine machine1 = new(1, "Monoblok", null, null, null, true);
            Machine machine2 = new(2, "RB1", null, null, null, true);
            machine1.SetParent(machine2);

            ErrandType errandType = new(1, "DCS", null);

            User user = new(1, "kryszm01", "Marcin", "Kryszewski");

            errand1.Machine = machine1;
            errand1.Type = errandType;
            errand1.AddStatus(errandErrandStatus1);
            errand1.AddStatus(errandErrandStatus2);
            errand1.AddStatus(errandErrandStatus3);
            errand1.Responsible = user;

            return Task.CompletedTask;
        }*/
        private Task LoadData()
        {
            return Task.CompletedTask;
        }
    }
}