﻿using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Layout.Content;
using Shopfloor.Layout.SidePanel;
using System;

namespace Shopfloor.Layout.MainWindow
{
    internal sealed class MainWindowViewModel
    {
        private readonly SidePanelViewModel _sidePanelViewModel;
        private readonly ContentViewModel _contentViewModel;

        public SidePanelViewModel SidePanelViewModel => _sidePanelViewModel;
        public ContentViewModel ContentViewModel => _contentViewModel;

        public MainWindowViewModel(IServiceProvider mainServices)
        {
            _sidePanelViewModel = mainServices.GetRequiredService<SidePanelViewModel>();
            _contentViewModel = mainServices.GetRequiredService<ContentViewModel>();
        }
    }
}