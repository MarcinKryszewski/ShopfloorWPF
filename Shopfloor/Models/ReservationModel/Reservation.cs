using System;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Models.ReservationModel
{
    internal sealed class Reservation : DataModel
    {
        private readonly ReservationDto _data;
        public Reservation()
        {
            _data = new();
            Completed = false;
        }
        required public double Amount
        {
            get => _data.Amount;
            init => _data.Amount = value;
        }
        public bool Completed
        {
            get => _data.Completed;
            set => _data.Completed = value;
        }
        required public DateTime CreateDate
        {
            get => _data.CreateDate;
            init => _data.CreateDate = value;
        }
        public ErrandPart? ErrandPart
        {
            get => _data.ErrandPart;
            set => _data.ErrandPart = value;
        }
        required public int ErrandPartId
        {
            get => _data.ErrandPartId;
            init => _data.ErrandPartId = value;
        }
        required public DateTime ExpirationDate
        {
            get => _data.ExpirationDate;
            set => _data.ExpirationDate = value;
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
    }
}