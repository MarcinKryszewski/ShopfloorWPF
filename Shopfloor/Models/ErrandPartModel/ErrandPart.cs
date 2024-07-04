using System;
using System.Collections.Generic;
using System.Linq;
using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Models.PartModel;
using Shopfloor.Models.UserModel;
using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Models.ErrandPartModel
{
    internal sealed partial class ErrandPart : DataModel
    {
        private const string _existingIdErrorMassage = "Id already exists";
        private readonly ErrandPartDto _data;
        private readonly List<ErrandPartStatus> _statusList = [];
        public ErrandPart(int? id = 0)
        {
            _data = new()
            {
                Id = id,
            };
        }
        public double? Amount { get => _data.Amount; set => _data.Amount = value; }
        public string AmountText => Amount + ((Part is not null) ? " " + Part.Unit : string.Empty);
        public bool Canceled
        {
            get => _data.Canceled;
            set => _data.Canceled = value;
        }
        public Errand? Errand
        {
            get => _data.Errand;
            set
            {
                if (value is null)
                {
                    return;
                }

                if (value.Id == ErrandId)
                {
                    _data.Errand = value;
                }
            }
        }
        required public int ErrandId
        {
            get => _data.ErrandId;
            set
            {
                string myName = nameof(ErrandId);
                ClearErrors(myName);

                if (_data.ErrandId != 0)
                {
                    AddError(myName, _existingIdErrorMassage);
                    return;
                }

                _data.ErrandId = value;
            }
        }
        public DateTime? ExpectedDeliveryDate
        {
            get => _data.ExpectedDeliveryDate;
            set => _data.ExpectedDeliveryDate = value;
        }
        public string ExpectedDeliveryDateDisplay
        {
            get
            {
                if (_data.ExpectedDeliveryDate is null)
                {
                    return string.Empty;
                }

                return ((DateTime)_data.ExpectedDeliveryDate).ToShortDateString();
            }
        }
        public int? Id
        {
            get => _data.Id;
            set
            {
                string myName = nameof(Id);
                ClearErrors(myName);

                if (_data.Id != 0 && _data.Id is not null)
                {
                    AddError(myName, _existingIdErrorMassage);
                    return;
                }

                _data.Id = value;
            }
        }
        public ErrandPartStatus LastStatus => StatusList.Where(status => status.StatusValue >= 0)
                                                .OrderByDescending(status => status.CreatedDate)
                                                .First();
        public string LastStatusText => LastStatusValue == -1 ? "ERROR" : ErrandPartStatus.Status[LastStatusValue];
        public string LastStatusUpdateDate => StatusList.Count > 0 ? LastStatus.CreatedDate.ToString("dd/MM/yyyy") : "NIGDY";
        public int LastStatusValue => StatusList.Count > 0 ? LastStatus.StatusValue : -1;
        public int OrderedById
        {
            get => _data.OrderedById;
            init => _data.OrderedById = value;
        }
        public User? OrderedByUser
        {
            get => _data.OrderedByUser;
            set
            {
                if (value is null)
                {
                    return;
                }

                if (value.Id == OrderedById)
                {
                    _data.OrderedByUser = value;
                }
            }
        }
        public Part? Part
        {
            get => _data.Part;
            set
            {
                if (value is null)
                {
                    return;
                }

                if (value.Id == PartId)
                {
                    _data.Part = value;
                }
            }
        }
        required public int PartId
        {
            get => _data.PartId;
            init => _data.PartId = value;
        }
        public double PricePerUnit => _data.PricePerUnit;
        public IEnumerable<ErrandPartStatus> StatusListDisplay => StatusList;

        internal List<ErrandPartStatus> StatusList => _statusList;

        public void SetPrice(double price, double? amount = null) => _data.PricePerUnit = amount is null ? price : price / (double)amount;
    }
    internal sealed partial class ErrandPart : IEquatable<ErrandPart>
    {
        public bool Equals(ErrandPart? other)
        {
            if (other == null)
            {
                return false;
            }

            return ErrandId == other.ErrandId && PartId == other.PartId;
        }
        public override bool Equals(object? obj) => obj is ErrandPart objErrandPart && Equals(objErrandPart);
        public override int GetHashCode() => ErrandId.GetHashCode() & PartId.GetHashCode();
    }
    internal sealed partial class ErrandPart : ISearchableModel
    {
        public string SearchValue => LastStatusText;
    }
    internal sealed partial class ErrandPart : ICloneable
    {
        public object Clone()
        {
            ErrandPart clone = new()
            {
                ErrandId = ErrandId,
                PartId = PartId,
                Amount = Amount,
                Canceled = Canceled,
                ExpectedDeliveryDate = ExpectedDeliveryDate,
                Id = Id,
                OrderedById = OrderedById,
                Part = Part,
                OrderedByUser = OrderedByUser,
            };
            return clone;
        }
    }
}