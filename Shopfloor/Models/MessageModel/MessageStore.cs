using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;

namespace Shopfloor.Models.MessageModel
{
    internal sealed class MessageStore : IDataStore<Message>
    {
        private readonly IServiceProvider _databaseServices;
        private List<Message> _data = [];
        public MessageStore(IServiceProvider databaseServices)
        {
            _databaseServices = databaseServices;
        }
        public List<Message> GetData => _data;
        public bool IsLoaded { get; private set; }
        public Task Load()
        {
            MessageProvider provider = _databaseServices.GetRequiredService<MessageProvider>();
            _data = new(provider.GetAll().Result);
            IsLoaded = true;
            return Task.CompletedTask;
        }
        public async Task Reload()
        {
            MessageProvider provider = _databaseServices.GetRequiredService<MessageProvider>();
            _data = new(await provider.GetAll());
        }
        public async Task CombineData()
        {
            List<Task> tasks = [];



            if (tasks.Count > 0) await Task.WhenAll(tasks);
        }
    }
}