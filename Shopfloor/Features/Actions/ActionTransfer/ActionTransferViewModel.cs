using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using Shopfloor.Contexts;
using Shopfloor.Features.Actions.ActionsList;
using Shopfloor.Features.Actions.ActionTransfer.Commands;
using Shopfloor.Features.Actions.ActionTransfer.Utilities;
using Shopfloor.Models.Activities;
using Shopfloor.Models.Workshops;
using Shopfloor.Roots;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Services.NotificationServices;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.Actions.ActionTransfer
{
    internal class ActionTransferViewModel : ViewModelBase
    {
        private readonly ActionTransferRoot _root;
        private Workshop? _selectedWorkshop;
        public ActionTransferViewModel(
            ViewModelBaseDependecies dependecies,
            ActivityContext activityContext,
            ActionTransferRoot root)
        : base(dependecies)
        {
            if (activityContext == null)
            {
                ReturnCommand.Execute(null);
            }
            Activity = activityContext!.Activity!;
            _root = root;
            PeopleToTrain = new ListCollectionView(_root.TrainingList)
            {
                Filter = FilterWorkshop,
            };

            Workshops = new ListCollectionView(_root.Workshops);

            SaveCommand = new ActionTransferCommand(root, activityContext);
            ((ActionTransferCommand)SaveCommand).ExecuteFinished += OnActionTransfer;

            Task.Run(LoadDataAsync);
        }
        public Activity Activity { get; }
        public bool IsEveryoneTrained
        {
            get
            {
                if (_root.TrainingList.Count == 0)
                {
                    return false;
                }
                foreach (ResponsibleTraining item in _root.TrainingList)
                {
                    if (!FilterWorkshop(item))
                    {
                        continue;
                    }
                    if (item.TrainingStatus != TrainingStatus.Trained)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        public ICollectionView PeopleToTrain { get; }
        public ICommand ReturnCommand => new NavigationCommand<ActionsListViewModel>(NavigationService).Navigate();
        public ICommand SaveCommand { get; }
        public Workshop? SelectedWorkshop
        {
            get => _selectedWorkshop;
            set
            {
                _selectedWorkshop = value;
                OnPropertyChanged(nameof(SelectedWorkshop));
                PeopleToTrain.Refresh();
                OnPropertyChanged(nameof(IsEveryoneTrained));
            }
        }
        public ICollectionView Workshops { get; private set; }
        private void OnActionTransfer(object? sender, Notification? notification)
        {
            if (notification != null)
            {
                Notifier.Show(notification);
            }
            OnPropertyChanged(nameof(Activity));
        }
        private bool FilterWorkshop(object obj)
        {
            if (obj is ResponsibleTraining training && SelectedWorkshop is not null)
            {
                return training.Responsible.WorkshopId == SelectedWorkshop.Id;
            }
            return false;
        }
        private async Task LoadDataAsync()
        {
            List<Task> tasks = [];

            await _root.LoadData();

            await Task.WhenAll(tasks);
            PeopleToTrain.Refresh();
            Workshops.Refresh();
            SelectedWorkshop = Workshops.Cast<Workshop>().FirstOrDefault(x => x.Id == Activity.WorkshopId);
        }
    }
}

// X - nieprzeszkolony
// O - w trakcie szkolenia
// V - przeszkolony