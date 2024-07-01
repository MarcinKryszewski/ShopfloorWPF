using Shopfloor.Interfaces;
using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Models.MessageModel
{
    internal sealed class MessageStore : StoreBase<Message>
    {
        public MessageStore(IProvider<Message> provider) : base(provider)
        {
        }
    }
}