using System;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using Shopfloor.Contexts;
using Shopfloor.Features.Actions.ActionTransfer.Utilities;
using Shopfloor.Features.Trainings.ListTrainee;
using Shopfloor.Features.Trainings.ListTrainer;
using Shopfloor.Features.Trainings.TrainingEdit;
using Shopfloor.Models.Persons;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.ViewModels;
using Shopfloor.Utilities.Collections;

namespace Shopfloor.Features.Trainings.TrainingDetails
{
    internal class TrainingDetailsViewModel : ViewModelBase
    {
        private string _title = "Line - Machine - Skill";
        private Person? _trainer;
        private DateTime _trainingDate = new(2025, 5, 22);
        private ConcurrentObservableCollection<Student> _students = [];
        public TrainingDetailsViewModel(
            ViewModelBaseDependecies dependecies,
            TrainingContext trainingContext
        )
        : base(dependecies)
        {
            EditNavigateCommand = new NavigationCommand<TrainingEditViewModel>(NavigationService).Navigate();
            if (trainingContext.TrainingList is null or ListTraineeViewModel)
            {
                ReturnNavigateCommand = new NavigationCommand<ListTraineeViewModel>(NavigationService).Navigate();
                return;
            }
            ReturnNavigateCommand = new NavigationCommand<ListTrainerViewModel>(NavigationService).Navigate();
        }
        public string Title => _title;
        public DateTime TrainingDate => _trainingDate;
        public string TrainerName => _trainer?.FullName ?? string.Empty;
        public ICollectionView Students => CollectionViewSource.GetDefaultView(_students.AsObservable);
        public ICommand EditNavigateCommand { get; }
        public ICommand ReturnNavigateCommand { get; }
        private class Student
        {
            required public Person Person { get; set; }
            public DateTime? TrainingDate { get; set; }
            public TrainingStatus TrainingStatus { get; set; } = TrainingStatus.Untrained;
        }
    }
}