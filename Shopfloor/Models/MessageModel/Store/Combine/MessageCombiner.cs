using Shopfloor.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Models.MessageModel.Store.Combine
{
    internal sealed class MessageCombiner : ICombiner<Message>
    {
        public Task Combine(List<Message> data)
        {
            return Task.CompletedTask;
        }
    }
}