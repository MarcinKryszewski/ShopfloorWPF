using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.SomeFeature;
using Shopfloor.Shared.Commands;
using Shopfloor.Shared.Services;

namespace Shopfloor.Features.Dashboard.Commands
{
    public class TestNavigateCommand : CommandBase
    {
        private readonly IServiceProvider _services;

        public TestNavigateCommand(IServiceProvider mainServices)
        {
            _services = mainServices;
        }

        public override void Execute(object parameter)
        {
            NavigationService<SomeFeatureViewModel> navigationService = _services.GetRequiredService<NavigationService<SomeFeatureViewModel>>();
            navigationService.Navigate();
        }
    }
}