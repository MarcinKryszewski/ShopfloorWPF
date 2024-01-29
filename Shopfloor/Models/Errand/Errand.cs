using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandsErrandStatuses;
using Shopfloor.Models.ErrandTypeModel;
using Shopfloor.Models.MachineModel;
using Shopfloor.Models.UserModel;
using System;
using System.Collections.Generic;

namespace Shopfloor.Models.ErrandModel
{
    internal sealed partial class Errand
    {
        public const string DefaultPriority = "C";
        private readonly int _createdById;
        private readonly DateTime _createdDate;
        private readonly ErrandDisplay _display;
        private readonly List<ErrandErrandStatus> _errandStatuses = [];
        private readonly int? _id;
        private User? _createdByUser;
        private string _description;
        private ErrandType? _errandType;
        private int _errandTypeId;
        private DateTime? _expectedDate;
        private Machine? _machine;
        private int _machineId;
        private int? _ownerId;
        private User? _ownerUser;
        private string _priority;
        private string? _sapNumber = string.Empty;
        public Errand(int id, DateTime createdDate, int createdById, int machineId, int errandTypeId, string description, string? sapNumber, DateTime? expectedDate, int? ownerId, string? priority = DefaultPriority)
        {
            _id = id;
            _createdDate = createdDate;
            _createdById = createdById;
            _ownerId = ownerId;
            _priority = priority ?? DefaultPriority;
            _machineId = machineId;
            _errandTypeId = errandTypeId;
            _description = description;
            _sapNumber = sapNumber;
            _expectedDate = expectedDate;
            _display = new(this);
        }
        public Errand(DateTime createdDate, int createdById, int machineId, int errandTypeId, string description, string? priority = DefaultPriority)
        {
            _createdDate = createdDate;
            _createdById = createdById;
            _machineId = machineId;
            _errandTypeId = errandTypeId;
            _description = description;
            _priority = priority ?? DefaultPriority;
            _display = new(this);
        }
        public int? CreatedById => _createdById;
        public User? CreatedByUser
        {
            get => _createdByUser;
            set
            {
                if (value == null) return;
                if (value.Id == _id) _createdByUser = value;
            }
        }
        public DateTime CreatedDate => _createdDate;
        public string Description
        {
            get => _description;
            set
            {
                if (value == null) return;
                _description = value;
            }
        }
        public ErrandDisplay Display => _display;
        public List<ErrandErrandStatus> ErrandStatuses => _errandStatuses;
        public int ErrandTypeId => _errandTypeId;
        public DateTime? ExpectedDate
        {
            get => _expectedDate;
            set
            {
                if (value == null) return;
                _expectedDate = value;
            }
        }
        public int? Id => _id;
        public Machine? Machine
        {
            get => _machine;
            set
            {
                if (value?.Id is not null) _machineId = (int)value.Id;
                _machine = value;
            }
        }
        public int MachineId => _machineId;
        public int? OwnerId => _ownerId;
        public string Priority
        {
            get => _priority;
            set => _priority = value;
        }
        public User? Responsible
        {
            get => _ownerUser;
            set
            {
                if (value?.Id is not null) _ownerId = value.Id;
                _ownerUser = value;
            }
        }
        public string SapNumber
        {
            get => _sapNumber = string.Empty;
            set => _sapNumber = value;
        }
        public ErrandType? Type
        {
            get => _errandType;
            set
            {
                if (value?.Id is not null) _errandTypeId = (int)value.Id;
                _errandType = value;
            }
        }
        public void AddStatus(ErrandErrandStatus status)
        {
            _errandStatuses.Add(status);
        }
    }
    internal sealed partial class Errand : ISearchableModel
    {
        public string SearchValue
        {
            get
            {
                return Machine?.Path ?? string.Empty + Description;
            }
        }
    }
    internal sealed partial class Errand : IEquatable<Errand>
    {
        public bool Equals(Errand? other)
        {
            if (other == null) return false;
            if (_id == null && other._id == null)
            {
                return CreatedDate == other.CreatedDate;
            }

            return _id == other.Id;
        }
        /*public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (obj is not Errand) return false;
            return Equals(obj);
    }*/
        public override int GetHashCode()
        {
            if (_id != null) return _id.GetHashCode();
            return CreatedDate.GetHashCode();
        }
    }
}