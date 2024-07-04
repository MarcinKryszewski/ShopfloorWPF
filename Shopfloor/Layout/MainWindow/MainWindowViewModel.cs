using Shopfloor.Layout.Content;
using Shopfloor.Layout.SidePanel;

namespace Shopfloor.Layout.MainWindow
{
    internal sealed class MainWindowViewModel
    {
        private readonly ContentViewModel _contentViewModel;
        private readonly SidePanelViewModel _sidePanelViewModel;
        public MainWindowViewModel(SidePanelViewModel sidePanelViewModel, ContentViewModel contentViewModel)
        {
            _sidePanelViewModel = sidePanelViewModel;
            _contentViewModel = contentViewModel;
        }
        public ContentViewModel ContentViewModel => _contentViewModel;
        public SidePanelViewModel SidePanelViewModel => _sidePanelViewModel;
    }
}