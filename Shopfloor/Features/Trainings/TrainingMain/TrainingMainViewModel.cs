using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Shopfloor.Contexts;
using Shopfloor.Features.Trainings.TrainingCreate;
using Shopfloor.Features.Trainings.TrainingDetails;
using Shopfloor.Features.Trainings.TrainingEdit;
using Shopfloor.Models.Trainings;
using Shopfloor.Roots;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.Trainings.TrainingMain
{
    internal class TrainingMainViewModel : ViewModelBase
    {
        private static readonly Lock _syncLock = new();
        private readonly TrainingsRoot _root;
        private readonly TrainingContext _context;

        public TrainingMainViewModel(
            ViewModelBaseDependecies dependecies,
            TrainingsRoot root,
            TrainingContext context)
        : base(dependecies)
        {
            _root = root;
            _context = context;
            _root.DataChanged += OnDataChanged;

            _context.Training = null;

            DetailsCommand = new NavigationCommand<TrainingDetailsViewModel>(NavigationService).Navigate();
            EditCommand = new NavigationCommand<TrainingEditViewModel>(NavigationService).Navigate();
            CreateCommand = new NavigationCommand<TrainingCreateViewModel>(NavigationService).Navigate();

            Task.Run(LoadDataAsync);
        }
        public ICommand DetailsCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand CreateCommand { get; }
        public ICollectionView Trainings => CollectionViewSource.GetDefaultView(_root.Data.AsObservable);
        public Training? Training
        {
            get => _context.Training;
            set
            {
                _context.Training = value;
                OnPropertyChanged(nameof(Training));
            }
        }
        public void OnDataChanged(object? sender, EventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                lock (_syncLock)
                {
                    OnPropertyChanged(nameof(Trainings));
                    Trainings.Refresh();
                }
            });
        }
        private async Task LoadDataAsync()
        {
            List<Task> tasks = [];

            tasks.Add(LoadTrainingsAsync());

            await Task.WhenAll(tasks);
        }
        private async Task LoadTrainingsAsync()
        {
            await _root.GetData();
        }
    }
}