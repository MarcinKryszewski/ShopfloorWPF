using Shopfloor.Models.Activities;
using Shopfloor.Shared;

namespace Shopfloor.Contexts
{
    internal class ActivityContext : ObservableObject
    {
        private bool _isEditable;
        public Activity? Activity { get; set; }
        public bool IsEditable
        {
            get => _isEditable;
            set
            {
                _isEditable = value;
                OnPropertyChanged(nameof(IsEditable));
            }
        }
    }
}