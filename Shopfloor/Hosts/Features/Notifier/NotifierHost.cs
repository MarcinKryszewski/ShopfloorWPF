﻿using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Services.NotificationServices;
using static Shopfloor.Services.NotificationServices.NotifierServices;

namespace Shopfloor.Hosts.Features
{
    internal static partial class NotifierServices
    {
        public static void Get(IServiceCollection services)
        {
            NotifierService notifier = NotifierSetup.GetSetup();
            services.AddSingleton<INotifier>(notifier);
        }
    }
}