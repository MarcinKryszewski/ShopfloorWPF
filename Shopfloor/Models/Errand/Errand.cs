using System;
using System.ComponentModel;
using Microsoft.VisualBasic.ApplicationServices;
using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandTypeModel;
using Shopfloor.Models.MachineModel;

namespace Shopfloor.Models.Errand
{
    public partial class Errand
    {
        public const string DefaultPriority = "C";
        private int? _id;
        private DateTime _createdDate;
        private int _createdById;
        private User? _createdByUser;
        private int? _ownerId; //
        private User? _ownerUser;
        private string _priority; //
        private int _machineId; //
        private Machine? _machine;
        private int _errandTypeId; //
        private ErrandType? _errandType;
        private string _description; //
        private string? _sapNumber = string.Empty;
        private DateTime? _expectedDate; //

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
        }
        public Errand(DateTime createdDate, int createdById, int machineId, int errandTypeId, string description, string? priority = DefaultPriority)
        {
            _createdDate = createdDate;
            _createdById = createdById;
            _machineId = machineId;
            _errandTypeId = errandTypeId;
            _description = description;
            _priority = priority ?? DefaultPriority;
        }

        public int? Id => _id; //
        public DateTime CreatedDate => _createdDate;
        public int? CreatedById => _createdById;
        public User? CreatedByUser => _createdByUser;
        public int? OwnerId => _ownerId;
        public User? OwnerUser => _ownerUser;
        public string Priority => _priority; //
        public int MachineId => _machineId;
        public Machine? Machine => _machine; //
        public int ErrandTypeId => _errandTypeId;
        public ErrandType? ErrandType => _errandType;
        public string Description => _description = string.Empty; //
        public string SapNumber => _sapNumber = string.Empty;
        public DateTime? ExpectedDate => _expectedDate;  //
    }
    public partial class Errand : ISearchableModel
    {
        public string SearchValue
        {
            get
            {
                return Machine?.Path ?? string.Empty + Description;
            }
        }
    }
    public partial class Errand : IEquatable<Errand>
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
        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (obj is not Errand) return false;
            return Equals(obj);
        }
        public override int GetHashCode()
        {
            if (_id != null) return _id.GetHashCode();
            return CreatedDate.GetHashCode();
        }
    }
}