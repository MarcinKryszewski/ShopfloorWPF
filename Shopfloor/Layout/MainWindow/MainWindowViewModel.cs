using System;
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Layout.Content;
using Shopfloor.Layout.SidePanel;

namespace Shopfloor.Layout.MainWindow
{

    public class MainWindowViewModel
    {
        private readonly SidePanelViewModel _sidePanelViewModel;
        private readonly ContentViewModel _contentViewModel;

        public SidePanelViewModel SidePanelViewModel => _sidePanelViewModel;
        public ContentViewModel ContentViewModel => _contentViewModel;

        public MainWindowViewModel(IServiceProvider mainHost)
        {
            _sidePanelViewModel = mainHost.GetRequiredService<SidePanelViewModel>();
            _contentViewModel = new ContentViewModel();
        }
    }
}
