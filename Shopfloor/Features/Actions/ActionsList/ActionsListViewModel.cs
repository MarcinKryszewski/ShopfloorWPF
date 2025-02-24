using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Shopfloor.Contexts;
using Shopfloor.Features.Actions.ActionCreate;
using Shopfloor.Features.Actions.ActionDetails;
using Shopfloor.Features.Actions.ActionEdit;
using Shopfloor.Features.Actions.ActionsList.Commands;
using Shopfloor.Features.Actions.ActionsList.Utilities;
using Shopfloor.Features.Actions.ActionTransfer;
using Shopfloor.Features.Trainings.ActionTraining;
using Shopfloor.Models.Activities;
using Shopfloor.Roots;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.Actions.ActionsList
{
    internal class ActionsListViewModel : ViewModelBase
    {
        private static readonly Lock _syncLock = new();
        private readonly ActivityContext _activityContext;
        private readonly ActivitiesRoot _root;
        public ActionsListViewModel(
            ActivitiesRoot root,
            ActionsFilter filter,
            ActivityContext activityContext,
            ViewModelBaseDependecies dependecies)
        : base(dependecies)
        {
            _root = root;
            FilterData = filter;
            _activityContext = activityContext;
            Activities.Filter = Filter;
            _root.DataChanged += OnDataChanged;
            FilterData.FiltersChanged += OnFiltersChanged;

            DetailsCommand = new NavigationCommand<ActionDetailsViewModel>(NavigationService).Navigate();
            EditCommand = new NavigationCommand<ActionEditViewModel>(NavigationService).Navigate();
            CreateActionCommand = new NavigationCommand<ActionCreateViewModel>(NavigationService).Navigate();
            CancelCommand = new CancelCommand(root);
            TrainingsCommand = new NavigationCommand<ActionTrainingViewModel>(NavigationService).Navigate();
            TransferActionCommand = new NavigationCommand<ActionTransferViewModel>(NavigationService).Navigate();

            Task.Run(LoadDataAsync);
        }
        public ICollectionView Activities => CollectionViewSource.GetDefaultView(_root.Data.AsObservable);
        public Activity? Activity
        {
            get => _activityContext.Activity;
            set => _activityContext.Activity = value;
        }
        public ICommand CancelCommand { get; }
        public ICommand CreateActionCommand { get; }
        public ICommand DetailsCommand { get; }
        public ICommand EditCommand { get; }
        public ActionsFilter FilterData { get; }
        public ICommand TrainingsCommand { get; }
        public ICommand TransferActionCommand { get; }
        public void OnDataChanged(object? sender, EventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                lock (_syncLock)
                {
                    OnPropertyChanged(nameof(Activities));
                    Activities.Refresh();
                }
            });
        }
        public void OnFiltersChanged(object? sender, EventArgs e)
        {
            Activities.Filter = Filter;
            Activities.Refresh();
        }
        private bool Filter(object obj)
        {
            if (obj is Activity activity)
            {
                bool workshop = string.IsNullOrEmpty(FilterData.Workshop) || activity.Workshop!.Name.Contains(FilterData.Workshop, StringComparison.InvariantCultureIgnoreCase);
                bool line = string.IsNullOrEmpty(FilterData.Line) || activity.Machine!.Line!.Name.Contains(FilterData.Line, StringComparison.InvariantCultureIgnoreCase);
                bool machine = string.IsNullOrEmpty(FilterData.Machine) || activity.Machine!.Name.Contains(FilterData.Machine, StringComparison.InvariantCultureIgnoreCase);
                bool type = string.IsNullOrEmpty(FilterData.Type) || activity.Type!.Name.Contains(FilterData.Type, StringComparison.InvariantCultureIgnoreCase);
                bool isLoto = !FilterData.IsLoto || activity.IsLoto;
                bool isJog = !FilterData.IsJog || activity.IsJog;
                bool isProduction = !FilterData.IsProduction || activity.IsDurningProduction;
                bool hasInstruction = FilterData.HasInstruction == null || (bool)FilterData.HasInstruction == activity.HasInstruction;

                return workshop && line && machine && type && isLoto && isJog && isProduction && hasInstruction;
            }
            return false;
        }
        private async Task LoadActivitiesAsync()
        {
            await _root.GetData();
        }
        private async Task LoadDataAsync()
        {
            List<Task> tasks = [];

            tasks.Add(LoadActivitiesAsync());

            await Task.WhenAll(tasks);
        }
    }
}