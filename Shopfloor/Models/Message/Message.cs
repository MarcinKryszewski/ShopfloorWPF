using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Shopfloor.Models.MessageModel
{
    internal sealed partial class Message : DataModel
    {
        private MessageDTO _data => new();
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
    }
}