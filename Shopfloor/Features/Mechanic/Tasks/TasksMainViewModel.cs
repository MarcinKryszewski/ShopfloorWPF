using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shopfloor.Features.Mechanic.Tasks.Hosts;
using Shopfloor.Features.Mechanic.Tasks.TasksList;
using Shopfloor.Shared.Services;
using Shopfloor.Shared.Stores;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.Mechanic.Tasks
{
    public class TasksMainViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly IHost _tasksServices;
        public ViewModelBase? Content => _navigationStore.CurrentViewModel;
        public TasksMainViewModel(IServiceProvider databaseServices)
        {
            _tasksServices = TasksHost.GetHost(databaseServices);
            _tasksServices.Start();

            _navigationStore = _tasksServices.Services.GetRequiredService<NavigationStore>();
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            NavigationService<TasksListViewModel> navigationService = _tasksServices.Services.GetRequiredService<NavigationService<TasksListViewModel>>();
            navigationService.Navigate();
        }
        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(Content));
        }
    }
}