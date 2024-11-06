using Shopfloor.Models.ActivityTypes;
using Shopfloor.Models.Commons.BaseClasses;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Machines;
using Shopfloor.Models.Workshops;

namespace Shopfloor.Models.Activities
{
    internal class ActivityCreation : ModelValidationBase, IModelCreationModel<Activity>
    {
        required public int Id { get; set; }
        required public int MachineId { get; set; }
        public Machine? Machine { get; set; }
        required public int WorkshopId { get; set; }
        public Workshop? Workshop { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsPassed { get; set; } = false;
        required public int TypeId { get; set; }
        public ActivityType? Type { get; set; }
        public bool IsLoto { get; set; }
        public bool IsJog { get; set; }
        public bool IsDurningProduction { get; set; }
        public int Occurance { get; set; } = 1;
        public OccuranceUnit OccuranceUnit { get; set; } = OccuranceUnit.M;
        public Activity CreateModel(int id)
        {
            return new Activity()
            {
                Id = id,
                MachineId = MachineId,
                Machine = Machine,
                WorkshopId = WorkshopId,
                Workshop = Workshop,
                Description = Description,
                IsPassed = IsPassed,
                TypeId = TypeId,
                Type = Type,
                IsLoto = IsLoto,
                IsJog = IsJog,
                IsDurningProduction = IsDurningProduction,
                Occurance = Occurance,
                OccuranceUnit = OccuranceUnit,
            };
        }
    }
}