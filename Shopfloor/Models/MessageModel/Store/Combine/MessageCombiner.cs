using Shopfloor.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Models.MessageModel.Store.Combine
{
    internal sealed class MessageCombiner : ICombinerManager<Message>
    {
        public Task Combine()
        {
            return Task.CompletedTask;
        }
    }
}