using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Shopfloor.Features.Trainings.ActionTraining.Contexts;
using Shopfloor.Shared.Commands;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.Trainings.ActionTraining.SelectPerson
{
    internal class SelectPersonViewModel : ViewModelBase
    {
        private readonly SelectedActionContext _actionContext;
        public SelectPersonViewModel(SelectedActionContext actionContext)
        {
            _actionContext = actionContext;
            ReturnCommand = new RelayCommand(_ => { _actionContext.Activity = null; }, x => true);
        }
        public ICommand ReturnCommand { get; }
    }
}