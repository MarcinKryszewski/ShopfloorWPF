using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Admin.Parts.Commands;
using Shopfloor.Features.Admin.Parts.Interfaces;
using Shopfloor.Features.Admin.Parts.List;
using Shopfloor.Shared.Commands;
using Shopfloor.Shared.Services;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.Admin.Parts.Edit
{
    public class PartsEditViewModel : ViewModelBase, IPartForm
    {
        public ICommand ReturnCommand { get; }
        public ICommand CleanFormCommand { get; }
        public ICommand EditPartCommand { get; }

        public PartsEditViewModel(IServiceProvider mainServices, IServiceProvider databaseServices)
        {
            ReturnCommand = new NavigateCommand<PartsListViewModel>(mainServices.GetRequiredService<NavigationService<PartsListViewModel>>());
            CleanFormCommand = new PartCleanFormCommand(this);
            EditPartCommand = new PartEditCommand();
        }

        public void CleanForm()
        {

        }
    }
}