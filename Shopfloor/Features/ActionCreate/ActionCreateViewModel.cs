using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using Shopfloor.Features.ActionsList;
using Shopfloor.Models.Activities;
using Shopfloor.Models.ActivityTypes;
using Shopfloor.Models.Lines;
using Shopfloor.Models.Machines;
using Shopfloor.Models.Workshops;
using Shopfloor.Roots;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.ActionCreate
{
    internal class ActionCreateViewModel : ViewModelBase
    {
        public ActionCreateViewModel(
            ViewModelBaseDependecies dependecies,
            ActivitiesDataRoot dataRoot)
        : base(dependecies)
        {
            ReturnCommand = new NavigationCommand<ActionsListViewModel>(NavigationService).Navigate();
            SaveCommand = new NavigationCommand<ActionsListViewModel>(NavigationService).Navigate(); //TODO save
            Task.Run(() => LoadDataAsync(dataRoot));
        }
        public ActivityCreation Activity { get; } = new();
        public Line? Line { get; set; }
        public ICollectionView Lines { get; private set; } = new ListCollectionView(new List<Line>());
        public ICollectionView Machines { get; private set; } = new ListCollectionView(new List<Machine>());
        public ICollectionView Occurencies { get; private set; } = new ListCollectionView(new List<Occurance>());
        public ICommand ReturnCommand { get; }
        public ICommand SaveCommand { get; }
        public ICollectionView Types { get; private set; } = new ListCollectionView(new List<ActivityType>());
        public ICollectionView Workshops { get; private set; } = new ListCollectionView(new List<Workshop>());
        private async Task LoadDataAsync(ActivitiesDataRoot dataRoot)
        {
            List<Task> tasks = [];

            tasks.Add(LoadLinesAsync(dataRoot));
            tasks.Add(LoadTypesAsync(dataRoot));
            tasks.Add(LoadWorkshopsAsync(dataRoot));
            tasks.Add(LoadMachinesAsync(dataRoot));
            tasks.Add(LoadOccurenciesAsync(dataRoot));

            await Task.WhenAll(tasks);
        }
        private async Task LoadLinesAsync(ActivitiesDataRoot dataRoot)
        {
            List<Line> data = await dataRoot.GetLines();
            Lines = new ListCollectionView(data);
            OnPropertyChanged(nameof(Lines));
        }
        private async Task LoadMachinesAsync(ActivitiesDataRoot dataRoot)
        {
            List<Machine> data = await dataRoot.GetMachines();
            Machines = new ListCollectionView(data);
            OnPropertyChanged(nameof(Types));
        }
        private async Task LoadOccurenciesAsync(ActivitiesDataRoot dataRoot)
        {
            List<Occurance> data = await dataRoot.GetOccurencies();
            Occurencies = new ListCollectionView(data);
            OnPropertyChanged(nameof(Types));
        }
        private async Task LoadTypesAsync(ActivitiesDataRoot dataRoot)
        {
            List<ActivityType> data = await dataRoot.GetTypes();
            Types = new ListCollectionView(data);
            OnPropertyChanged(nameof(Types));
        }
        private async Task LoadWorkshopsAsync(ActivitiesDataRoot dataRoot)
        {
            List<Workshop> data = await dataRoot.GetWorkshops();
            Workshops = new ListCollectionView(data);
            OnPropertyChanged(nameof(Types));
        }
    }
}