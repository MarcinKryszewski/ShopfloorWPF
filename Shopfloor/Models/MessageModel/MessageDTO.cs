using Microsoft.VisualBasic.ApplicationServices;

namespace Shopfloor.Models.MessageModel
{
    internal sealed class MessageDTO
    {
        public int? Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public bool Read { get; set; }
        public int ReceiverId { get; set; }
        public User? Receiver { get; set; }
    }
}