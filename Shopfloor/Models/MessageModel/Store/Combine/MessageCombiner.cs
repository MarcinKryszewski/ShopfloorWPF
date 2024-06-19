using Shopfloor.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Models.MessageModel.Store.Combine
{
    internal sealed class MessageCombiner : ICombinerManager<Message>
    {
        public bool IsCombined { get; private set; }
        public Task CombineAll(bool shouldForce = false)
        {
            if (IsCombined && !shouldForce) return Task.CompletedTask;
            IsCombined = true;
            return Task.CompletedTask;
        }
        public Task CombineOne(Message item)
        {
            return Task.CompletedTask;

        }
    }
}