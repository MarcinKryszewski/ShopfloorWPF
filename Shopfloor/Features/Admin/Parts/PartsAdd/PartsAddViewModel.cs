using System;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Admin.Parts.Commands;
using Shopfloor.Features.Admin.Parts.Interfaces;
using Shopfloor.Features.Admin.Parts.List;
using Shopfloor.Shared.Commands;
using Shopfloor.Shared.Services;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.Admin.Parts.Add
{
    public class PartsAddViewModel : ViewModelBase, IPartForm
    {
        public ICommand ReturnCommand { get; }
        public ICommand CleanFormCommand { get; }
        public ICommand AddPartCommand { get; }

        public PartsAddViewModel(IServiceProvider mainServices, IServiceProvider databaseServices)
        {
            ReturnCommand = new NavigateCommand<PartsListViewModel>(mainServices.GetRequiredService<NavigationService<PartsListViewModel>>());
            CleanFormCommand = new PartCleanFormCommand(this);
            AddPartCommand = new PartAddCommand();
        }

        public void CleanForm()
        {

        }
    }
}