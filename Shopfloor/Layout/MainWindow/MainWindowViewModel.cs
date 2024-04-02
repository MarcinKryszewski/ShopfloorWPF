using Shopfloor.Layout.Content;
using Shopfloor.Layout.SidePanel;

namespace Shopfloor.Layout.MainWindow
{
    internal sealed class MainWindowViewModel
    {
        private readonly SidePanelViewModel _sidePanelViewModel;
        private readonly ContentViewModel _contentViewModel;

        public SidePanelViewModel SidePanelViewModel => _sidePanelViewModel;
        public ContentViewModel ContentViewModel => _contentViewModel;

        public MainWindowViewModel(SidePanelViewModel sidePanelViewModel, ContentViewModel contentViewModel)
        {
            _sidePanelViewModel = sidePanelViewModel;
            _contentViewModel = contentViewModel;
        }
    }
}