using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Services.NotificationServices;
using Shopfloor.Shared.ViewModels;
using System;
using System.Windows.Input;
using ToastNotifications;
using static Shopfloor.Services.NotificationServices.NotifierServices.ToastMessageStyles;

namespace Shopfloor.Features.Dashboard
{
    internal sealed class DashboardViewModel : ViewModelBase
    {
        private readonly Notifier _notifier;
        public ICommand NotifierTest { get; }
        public DashboardViewModel(IServiceProvider mainServices)
        {
            _notifier = mainServices.GetRequiredService<Notifier>();
            NotifierTest = new NotifierCommand(_notifier, "TEST", Success);
        }
    }
}