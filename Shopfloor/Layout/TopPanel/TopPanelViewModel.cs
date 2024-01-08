using System;
using System.Drawing;
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Shared.ViewModels;
using Shopfloor.Stores;

namespace Shopfloor.Layout.TopPanel
{
    public class TopPanelViewModel : ViewModelBase
    {
        private readonly string _userImagePath;
        public string UserImagePath => _userImagePath;
        public TopPanelViewModel(IServiceProvider userServices)
        {
            UserStore userStore = userServices.GetRequiredService<UserStore>();
            _userImagePath = userStore.Image;
        }
    }
}
