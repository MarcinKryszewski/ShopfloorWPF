using Shopfloor.Models.UserModel;

namespace Shopfloor.Models.MessageModel
{
    internal sealed class MessageDto
    {
        public int? Id { get; set; }
        public bool Read { get; set; }
        public User? Receiver { get; set; }
        public int ReceiverId { get; set; }
        public string Text { get; set; } = string.Empty;
    }
}