using Shopfloor.Layout.TopPanel;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Layout.Content
{
    public class ContentViewModel : ViewModelBase
    {
        private readonly TopPanelViewModel _topPanelViewModel;

        public TopPanelViewModel TopPanelViewModel => _topPanelViewModel;

        public ContentViewModel()
        {
            _topPanelViewModel = new TopPanelViewModel();
        }
    }
}
