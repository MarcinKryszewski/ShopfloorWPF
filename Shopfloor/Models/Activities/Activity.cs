using System.Linq;
using Shopfloor.Models.ActivityTypes;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Machines;
using Shopfloor.Models.Workshops;

namespace Shopfloor.Models.Activities
{
    internal class Activity : IModel
    {
        required public int Id { get; init; }
        required public int MachineId { get; init; }
        public Machine? Machine { get; set; }
        required public int WorkshopId { get; init; }
        public Workshop? Workshop { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsPassed { get; set; } = false;
        required public int TypeId { get; init; }
        public ActivityType? Type { get; set; }
        public bool IsLoto { get; set; }
        public bool IsJog { get; set; }
        public bool IsDurningProduction { get; set; }
        public int Occurance { get; set; } = 1;
        public OccuranceUnit OccuranceUnit { get; set; } = OccuranceUnit.M;
        public string OccuranceText => $"{Occurance} {OccuranceUnit}";
        public bool HasInstruction { get; set; } = false;
        public string Additionals => string.Join(
            " / ",
            new[]
            {
                IsLoto ? "LOTO" : null,
                IsJog ? "JOG" : null,
                IsDurningProduction ? "Podczas produkcji" : null,
            }.Where(s => !string.IsNullOrEmpty(s)));
        public void SetValues<T>(IModelCreationModel<T> data)
            where T : IModel
        {
            if (data is not ActivityCreation)
            {
                return;
            }

            ActivityCreation creation = (ActivityCreation)data;

            Machine = creation.Machine;
            Workshop = creation.Workshop;
            Description = creation.Description;
            IsPassed = creation.IsPassed;
            Type = creation.Type;
            IsLoto = creation.IsLoto;
            IsJog = creation.IsJog;
            IsDurningProduction = creation.IsDurningProduction;
            Occurance = creation.Occurance;
            OccuranceUnit = creation.OccuranceUnit;
        }
        public Activity Clone()
        {
            return (Activity)MemberwiseClone();
        }
    }
}