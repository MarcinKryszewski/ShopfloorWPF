using Shopfloor.Models.Activities;
using Shopfloor.Shared;

namespace Shopfloor.Features.Trainings.ActionTraining.Contexts
{
    internal class SelectedActionContext : ObservableObject
    {
        private Activity? _activity;
        public Activity? Activity
        {
            get => _activity;
            set
            {
                _activity = value;
                OnPropertyChanged(nameof(Activity));
            }
        }
    }
}