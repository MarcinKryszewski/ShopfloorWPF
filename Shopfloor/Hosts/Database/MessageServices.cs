using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;
using Shopfloor.Models.MessageModel;
using Shopfloor.Models.MessageModel.Store.Combine;

namespace Shopfloor.Hosts.Database
{
    internal static class MessageServices
    {
        public static void Get(IServiceCollection services)
        {
            services.AddSingleton<IProvider<Message>, MessageProvider>();
            services.AddSingleton<IDataStore<Message>, MessageStore>();
            services.AddSingleton<ICombinerManager<Message>, MessageCombiner>();
        }
    }
}