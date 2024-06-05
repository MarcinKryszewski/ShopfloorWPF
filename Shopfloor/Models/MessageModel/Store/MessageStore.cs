using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Models.MessageModel
{
    internal sealed class MessageStore : StoreBase<Message>
    {
        public MessageStore(MessageProvider provider) : base(provider)
        {
        }
    }
}