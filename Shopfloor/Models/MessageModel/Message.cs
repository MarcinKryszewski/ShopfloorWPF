using Shopfloor.Models.UserModel;
using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Models.MessageModel
{
    internal sealed partial class Message : DataModel
    {
        private readonly MessageDto _data;
        public Message()
        {
            _data = new();
        }
        public int? Id
        {
            get => _data.Id;
            set
            {
                if (_data.Id is not null)
                {
                    AddError(nameof(Id), "Id already assigned");
                    return;
                }
                _data.Id = value;
            }
        }
        public User? Receiver
        {
            get => _data.Receiver;
            set => _data.Receiver = value;
        }
        required public int ReceiverId
        {
            get => _data.ReceiverId;
            init => _data.ReceiverId = value;
        }
        public string Text
        {
            get => _data.Text;
            set => _data.Text = value;
        }
        public bool WasRead
        {
            get => _data.Read;
            set => _data.Read = value;
        }
    }
}