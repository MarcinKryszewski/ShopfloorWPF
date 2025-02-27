using Shopfloor.Models.Commons.BaseClasses;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Persons;

namespace Shopfloor.Models.Attendences
{
    internal class AttendenceCreation : ModelValidationBase, IModelCreationModel<Attendence>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsConfirmedByCoach { get; set; }
        public bool IsConfirmedByTrainee { get; set; }
        public Attendence CreateModel(int id)
        {
            return new Attendence()
            {
                Id = id,
                Name = Name,
            };
        }
    }
}