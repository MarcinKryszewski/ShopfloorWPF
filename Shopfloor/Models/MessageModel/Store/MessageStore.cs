using Shopfloor.Models.MessageModel.Store.Combine;

namespace Shopfloor.Models.MessageModel
{
    internal sealed class MessageStore : StoreBase<Message>
    {
        public MessageStore(MessageProvider provider, MessageCombiner combiner) : base(provider, combiner)
        {

        }
    }
}