using System;
using System.Collections.Generic;
using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandStatusModel;
using Shopfloor.Models.ErrandTypeModel;
using Shopfloor.Models.MachineModel;
using Shopfloor.Models.UserModel;
using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Models.ErrandModel
{
    internal sealed partial class Errand : DataModel
    {
        public const string DefaultPriority = "C";
        private readonly ErrandDisplay _display;
        private readonly List<ErrandStatus> _errandStatuses = [];
        private readonly List<ErrandPart> _parts = [];
        private readonly ErrandValidation _validation;
        private User? _createdByUser;
        public Errand(int id = 0)
        {
            _display = new(this);
            _data = new()
            {
                Id = id,
            };
            _validation = new(this);
        }
        public ErrandDisplay Display => _display;
        public ErrandStatus LatestStatus => _errandStatuses[^1];
        public List<ErrandPart> Parts => _parts;
        public List<ErrandStatus> Statuses => _errandStatuses;
        public void AddStatus(ErrandStatus status) => _errandStatuses.Add(status);
        public void Validate() => _validation.Validate();
    }
    internal sealed partial class Errand
    {
        private const string _existingIdErrorMassage = "Id already exists";
        private readonly ErrandDto _data;
        required public int CreatedById
        {
            get => _data.CreatedById;
            init => _data.CreatedById = value;
        }
        public User? CreatedByUser
        {
            get => _createdByUser;
            set
            {
                if (value == null)
                {
                    return;
                }

                if (value.Id == _data.CreatedById)
                {
                    _createdByUser = value;
                }
            }
        }
        public DateTime CreatedDate
        {
            get => _data.CreatedDate;
            init => _data.CreatedDate = value;
        }
        public string Description
        {
            get => _data.Description ?? string.Empty;
            set
            {
                if (value == null)
                {
                    return;
                }

                _data.Description = value;
            }
        }
        public DateTime? ExpectedDate
        {
            get => _data.ExpectedDate;
            set
            {
                if (value == null)
                {
                    return;
                }

                _data.ExpectedDate = value;
            }
        }
        public int? Id
        {
            get => _data.Id;
            set
            {
                string myName = nameof(Id);
                ClearErrors(myName);

                if (value == null || _data.Id == 0)
                {
                    _data.Id = value;
                }

                AddError(myName, _existingIdErrorMassage);
            }
        }
        public Machine? Machine
        {
            get => _data.Machine;
            set
            {
                if (value?.Id is not null)
                {
                    _data.MachineId = (int)value.Id;
                }

                _data.Machine = value;
            }
        }
        public int? MachineId
        {
            get => _data.MachineId;
            init => _data.MachineId = value;
        }
        public int? OwnerId
        {
            get => _data.OwnerId;
            init => _data.OwnerId = value;
        }
        public string? Priority
        {
            get => _data.Priority ?? DefaultPriority;
            set => _data.Priority = value is null ? DefaultPriority : value;
        }
        public User? Responsible
        {
            get => _data.Responsible;
            set
            {
                if (value?.Id is not null)
                {
                    _data.OwnerId = value.Id;
                }

                _data.Responsible = value;
            }
        }
        public string? SapNumber
        {
            get => _data.SapNumber;
            set => _data.SapNumber = value;
        }
        public ErrandType? Type
        {
            get => _data.ErrandType;
            set
            {
                if (value?.Id is not null)
                {
                    _data.ErrandTypeId = (int)value.Id;
                }

                _data.ErrandType = value;
            }
        }
        public int? TypeId
        {
            get => _data.ErrandTypeId;
            init => _data.ErrandTypeId = value;
        }
        public void SetId(int id)
        {
            _data.Id = id;
        }
    }
    internal sealed partial class Errand : ISearchableModel
    {
        public string SearchValue => Machine?.Path ?? string.Empty + Description;
    }
    internal sealed partial class Errand : IEquatable<Errand>
    {
        public bool Equals(Errand? other)
        {
            if (other == null)
            {
                return false;
            }

            if (Id == null && other.Id == null)
            {
                return CreatedDate == other.CreatedDate;
            }

            return Id == other.Id;
        }
        public override bool Equals(object? obj) => obj is Errand objErrand && Equals(objErrand);
        public override int GetHashCode()
        {
            if (Id != null)
            {
                return Id.GetHashCode();
            }

            return CreatedDate.GetHashCode();
        }
    }
    internal sealed partial class Errand : ICloneable
    {
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}