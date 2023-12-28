using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Shopfloor.Features.Dashboard.Commands;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.Dashboard
{
    public class DashboardViewModel : ViewModelBase
    {
        public ICommand Test { get; }
        public DashboardViewModel(IServiceProvider mainServices)
        {
            Test = new TestNavigateCommand(mainServices);
        }
    }
}